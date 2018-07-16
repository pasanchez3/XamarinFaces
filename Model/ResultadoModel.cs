using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XamarinFaces.Model
{
    class ResultadoModel
    {
        public List<Candidate> candidates { get; set; }
        public string faceId { get; set; }
    }
    public class Candidate
    {
        public double confidence { get; set; }
        public string personId { get; set; }
    }
}