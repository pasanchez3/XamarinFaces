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
    public class CaraModel
    {
        public string faceId { get; set; }
        public FaceAttributes faceAttributes { get; set; }
        public FaceRectangle faceRectangle { get; set; }

    }
    public class FaceRectangle
    {
        public int height { get; set; }
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
    }
    public class FaceAttributes
    {
        public double age { get; set; }
        public FacialHair facialHair { get; set; }
        public string gender { get; set; }
        public headPose headPose { get; set; }
        public double smile { get; set; }

    }
    public class FacialHair
    {
        public double beard { get; set; }
        public double moustache { get; set; }
        public double sideburns { get; set; }
    }
    public class headPose
    {
        public double pitch { get; set; }
        public double roll { get; set; }
        public double yaw { get; set; }
    }

}