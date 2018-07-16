using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Cognitive.Face.Droid;
using Android.Graphics;
using System.Collections.Generic;


using System.IO;
using GoogleGson;
using Newtonsoft.Json;
using Java.Util;
using System;
using XamarinFaces.Model;
using XamarinFaces.Helper;
using Android.Content;
using Android.Provider;
using Android.Runtime;

namespace XamarinFaces
{
    [Activity(Label = "Reconocer Cara", MainLauncher = true)]
    public class MainActivity : Activity
    {
        // Establecer el end point y la clave proporcionada por Cognitive Services para Face:
        private FaceServiceRestClient ServicioFace = new FaceServiceRestClient("https://westcentralus.api.cognitive.microsoft.com/face/v1.0", "88aeb72629714eae9ef4090255b08c61");
        // Grupo Persona creado
        private string grupoPersonaId = "hollywoodstar";
        ImageView imageView;
        Bitmap imagenBitMap;
        Button btnDetectar, btnIdentificar, btnCamara;
        List<CaraModel> carasDetectadas = new List<CaraModel>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            btnDetectar = FindViewById<Button>(Resource.Id.btnDetectar);
            btnIdentificar = FindViewById<Button>(Resource.Id.btnIdentificar);
            btnCamara = FindViewById<Button>(Resource.Id.btnCamara);
            btnCamara.Click += btnCamara_Click;

            btnDetectar.Click += delegate
            {
                byte[] datosBitMap;
                using (var stream = new MemoryStream())
                {
                    imagenBitMap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                    datosBitMap = stream.ToArray();
                }
                var entrada = new MemoryStream(datosBitMap);
                new DetectTask(this).Execute(entrada);

            };

            btnIdentificar.Click += delegate
            {
                string[] carasId = new string[carasDetectadas.Count];
                for(int i = 0; i<carasDetectadas.Count; i++)
                {
                    carasId[i] = carasDetectadas[i].faceId;
                }
                new IdentificationTask(this, grupoPersonaId).Execute(carasId);

            };
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            //Cargar imagen de cámara
            base.OnActivityResult(requestCode, resultCode, data);
            imagenBitMap = (Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(imagenBitMap);
        }
        private void btnCamara_Click(object sender, EventArgs e)
        {
            //Inicia cámara
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
        class DetectTask : AsyncTask<Stream, string, string>
        {
            private MainActivity mainActivity;

            public DetectTask(MainActivity mainActivity)
            {
                this.mainActivity = mainActivity;
            }

            protected override string RunInBackground(params Stream[] @params)
            {
                
                var resultado = mainActivity.ServicioFace.Detect(@params[0],true,false,null);
                if(resultado == null)
                {
                    return null;
                }
                Gson gson = new Gson();
                var stringresultado = gson.ToJson(resultado);
                return stringresultado;
            }

            protected override void OnPreExecute()
            {
                
            }
            protected override void OnPostExecute(string resultado)
            {
                var caras = JsonConvert.DeserializeObject<List<CaraModel>>(resultado);
                mainActivity.carasDetectadas = caras;
                if(mainActivity.carasDetectadas.Count != 0)
                {
                    mainActivity.btnIdentificar.Enabled = true;
                }
            }
            protected override void OnProgressUpdate(params string[] values)
            {
            }
        }
        
        class IdentificationTask:AsyncTask<string,string,string>
        {
            private MainActivity mainActivity;
            private string grupoPersonaId;

            public IdentificationTask(MainActivity mainActivity, string grupoPersonaId)
            {
                this.mainActivity = mainActivity;
                this.grupoPersonaId = grupoPersonaId;
            }

            protected override string RunInBackground(params string[] @params)
            {
                try
                {
                    UUID[] listaUuid = new UUID[(@params.Length)];
                    for(int i = 0; i < @params.Length; i++)
                    {
                        listaUuid[i] = UUID.FromString(@params[i]);
                    }
                    var resultado = mainActivity.ServicioFace.Identity(grupoPersonaId, listaUuid, 1);

                    Gson gson = new Gson();
                    var resultadoString = gson.ToJson(resultado);
                    return resultadoString;
                }
                catch(Exception ex)
                {
                    return null;
                }
            }

            protected override void OnPreExecute()
            {
            }
            protected override void OnProgressUpdate(params string[] values)
            {
            }
            protected override void OnPostExecute(string resultado)
            {
                var listaCaras = JsonConvert.DeserializeObject<List<ResultadoModel>>(resultado);
                foreach(var identify in listaCaras)
                {
                    if(identify.candidates.Count != 0)
                    {
                        var candidate = identify.candidates[0];
                        var personId = candidate.personId;
                        new PersonDetectionTask(mainActivity, grupoPersonaId).Execute(personId);
                    }
                }
            }
        }

        class PersonDetectionTask:AsyncTask<string, string,string>
        {
            private MainActivity mainActivity;
            private string grupoPersonaId;

            public PersonDetectionTask(MainActivity mainActivity, string grupoPersonaId)
            {
                this.mainActivity = mainActivity;
                this.grupoPersonaId = grupoPersonaId;
            }

            protected override string RunInBackground(params string[] @params)
            {
                try
                {
                    UUID uuid = UUID.FromString(@params[0]);
                    var persona = mainActivity.ServicioFace.GetPerson(grupoPersonaId, uuid);
                    Gson gson = new Gson();
                    var resultado = gson.ToJson(persona);
                    return resultado;
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
            protected override void OnPreExecute()
            {
            }
            protected override void OnProgressUpdate(params string[] values)
            {
            }
            protected override void OnPostExecute(string resultado)
            {
                try
                {
                    var persona = JsonConvert.DeserializeObject<PersonaModel>(resultado);
                    mainActivity.imageView.SetImageBitmap(Dibujar.DibujarRectangulo(mainActivity.imagenBitMap, mainActivity.carasDetectadas, persona.name));

                }
                catch
                {

                }
            }
        }
    }
}

