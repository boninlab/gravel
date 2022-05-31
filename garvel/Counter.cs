using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp;

namespace garvel
{
    public class Counter : GVimage
    {
        public int currCellCnt = 0;
        OpenCvSharp.Point[][] contours;
        HierarchyIndex[] hierarchy;
        public List<double> Contours;
        ArrayList cell_centers;

        public static string jsSet = // 기본값
            "{" +
                "\"pre-process\": {" +
                    "\"customSize\": false," +
                    "\"customWidth\": 0," +
                    "\"customHeight\": 0," +
                    "\"brightness\": 100," +
                    "\"rotate\": 0," +
                    "\"filter\": 0," +
                    "\"filterValue\": 0," +
                    "\"border\": 0," +
                "}," +
                "\"process\": {" +
                    "\"threshold\": 65," +
                    "\"approxi\": 0," +
                    "\"retrieval\": 0," +
                    "\"areaMin\": 10," +
                    "\"areaMax\": 500," +
                "}," +
                "\"post-process\": {" +
                    "\"countArea\": {" +
                        "\"R\": 0," +
                        "\"G\": 255," +
                        "\"B\": 0," +
                        "\"thickness\": 1," +
                    "}," +
                    "\"exclusionArea\": {" +
                        "\"R\": 255," +
                        "\"G\": 0," +
                        "\"B\": 0," +
                        "\"thickness\": 1," +
                    "}," +
                    "\"label\": {" +
                        "\"enable\": false," +
                        "\"R\": 255," +
                        "\"G\": 255," +
                        "\"B\": 0," +
                        "\"scale\": 25," +
                        "\"lineType\": 0," +
                        "\"decimal\": 1," +
                    "}," +
                    "\"bound\": {" +
                        "\"enable\": false," +
                        "\"R\": 0," +
                        "\"G\": 255," +
                        "\"B\": 255," +
                    "}," +
                "}," +
                "\"export\": {" +
                    "\"original\": true," +
                    "\"marked\": true," +
                    "\"csv\": true," +
                "}," +
            "}";
        public static JObject mainSet = JObject.Parse(jsSet);
        public JToken preSet = mainSet["pre-process"];
        public JToken procSet = mainSet["process"];
        public JToken postSet = mainSet["post-process"];
        public JToken exportSet = mainSet["export"];

        public static Image RotateImage(Image img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.RotateTransform(rotationAngle); // 회전시 bound 크기를 구함
            Bitmap bmp2 = new Bitmap((int)gfx.VisibleClipBounds.Width, (int)gfx.VisibleClipBounds.Height);
            Graphics gfx2 = Graphics.FromImage(bmp2);
            gfx2.TranslateTransform((float)bmp2.Width / 2, (float)bmp2.Height / 2);
            gfx2.RotateTransform(rotationAngle);
            gfx2.TranslateTransform(-(float)bmp2.Width / 2, -(float)bmp2.Height / 2);
            gfx2.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx2.DrawImage(img, new System.Drawing.Point((bmp2.Width - bmp.Width)/2, (bmp2.Height - bmp.Height)/2));
            gfx.Dispose();
            gfx2.Dispose();

            return bmp2;
        }
        private JObject createCountJs(JObject jsmm)
        {
            List<int> mmkind = new List<int>(new int[] { 10, 20, 50 });

            double lo = comp2mm(mmX, mmY).Item1;

            foreach (int mm in mmkind)
            {
                string k = mm.ToString();
                jsmm.Add(k, new JObject());
                for (int i = 0; i < Contours.Count; i++)
                {
                    double start = 0;
                    double end = 0;
                    for (int s = 0; end < lo; s++)
                    {
                        start = s * mm;
                        end = (s + 1) * mm;
                        string key = start.ToString() + "~" + end.ToString();
                        if (jsmm[k][key] == null)
                            jsmm[k][key] = 0;
                        if (start < Contours[i] && Contours[i] <= end)
                            jsmm[k][key] = (int)jsmm[k][key] + 1;
                    }
                }
            }
            return jsmm;
        }

        private JObject creCntJs(JObject jsmm)
        {
            List<int> mmkind = new List<int>(new int[] { 10, 20, 50 });
            foreach (int mm in mmkind)
            {
                string k = mm.ToString();
                jsmm.Add(k, new JObject());
                foreach(Point2f center in cell_centers)
                {
                    double top = 0;
                    double bottom = 0;
                    for (int s = 0; bottom < mmY; s++)
                    {
                        top = s * mm;
                        bottom = (s + 1) * mm;
                        string key = ((int)(top+(mm/2))).ToString();

                        if (jsmm[k][key] == null)
                            jsmm[k][key] = 0;

                        double centerY = center.Y * pixelHeight;

                        if (top < centerY && centerY <= bottom)
                            jsmm[k][key] = (int)jsmm[k][key] + 1;
                    }
                }

            }

            return jsmm;
        }
        private void createCsv(string savePath, string nowt)
        {
            string strFilePath = savePath + "\\results_"+ nowt + ".csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();

            string[][] inaOutput = new string[][]{
            new string[]{"Image info"},
            new string[]{"Name","Size(pixel)","Size(mm)","DPI"},
            new string[]{imgName,width.ToString()+"x"+height.ToString(), string.Format("{0:F2}", mmX) +"x"+ string.Format("{0:F2}", mmY), Convert.ToInt32(dpiX).ToString()+"x"+ Convert.ToInt32(dpiY).ToString()},
            new string[]{ },
            new string[]{"Total count"},
            new string[]{currCellCnt.ToString()},
            };
            for (int i = 0; i < inaOutput.Length; i++)
                sbOutput.AppendLine(string.Join(strSeperator, inaOutput[i]));

            JObject jsmm = new JObject();
            jsmm = creCntJs(jsmm); // 단위별 개수 Js 생성

            foreach (var obj in jsmm) 
            { 
                sbOutput.AppendLine(string.Join(strSeperator, " "));
                sbOutput.AppendLine(string.Join(strSeperator, "("+obj.Key+"mm)"));
                foreach (var subObj in jsmm[obj.Key])
                {
                    string[] sstr = subObj.ToString().Split(new string[] { ": " }, StringSplitOptions.None);
                    sbOutput.AppendLine(string.Join(strSeperator, sstr));
                }
            }
            File.WriteAllText(strFilePath, sbOutput.ToString());
        }

        public bool makeResults(string savePath)
        {
            DateTime dt = DateTime.Now;
            string nowPath = "\\core image results\\" + dt.ToString("yyyy-MM-dd");
            savePath += nowPath;
            string nowt = dt.ToString("HHmms");

            if (!Directory.Exists(savePath))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                catch
                {
                    MessageBox.Show("Save path Error.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
            }

            if ((bool)exportSet["original"])
                oriImg.SaveImage(savePath + @"\original_"+ nowt + ".jpg");

            if((bool)exportSet["marked"])
                RotateImage(markedImg,(int)preSet["rotate"]).Save(savePath + @"\marked_"+ nowt + ".jpg");

            if((bool)exportSet["csv"])
                createCsv(savePath, nowt);

            return true;
        }

        public void changeMainset(string jsString = null)
        {
            if (jsString == null)
            {
                mainSet = JObject.Parse(jsSet);
                preSet = mainSet["pre-process"];
                procSet = mainSet["process"];
                postSet = mainSet["post-process"];
                exportSet = mainSet["export"];
            }
            else
            {
                mainSet = JObject.Parse(jsString);
                preSet = mainSet["pre-process"];
                procSet = mainSet["process"];
                postSet = mainSet["post-process"];
                exportSet = mainSet["export"];
            }
        }
        public JObject getMainJS()
        {
            mainSet["pre-process"] = preSet;
            mainSet["process"] = procSet;
            mainSet["post-process"] = postSet;
            mainSet["export"] = exportSet;
            return mainSet;
        }

        private Mat applyFilter(Mat ori)
        {
            int borderNum = (int)preSet["border"];
            int filterNum = (int)preSet["filter"];
            int value = (int)preSet["filterValue"];
            value += (value - 1); // 홀수
            OpenCvSharp.BorderTypes border;

            switch (borderNum)
            {
                case 1: border = BorderTypes.Constant;
                    break;
                case 2: border = BorderTypes.Replicate;
                    break;
                case 3: border = BorderTypes.Reflect;
                    break;
                case 4: border = BorderTypes.Isolated;
                    break;
                default: border = BorderTypes.Reflect101;
                    break;
            }

            switch (filterNum)
            {
                case 1: Cv2.GaussianBlur(ori, ori, new OpenCvSharp.Size(value, value), 0, 0, border);
                    break;
                case 2: Cv2.Blur(ori, ori, new OpenCvSharp.Size(value, value), new OpenCvSharp.Point(-1, -1), border);
                    break;
                case 3: Cv2.BoxFilter(ori, ori, MatType.CV_8UC3, new OpenCvSharp.Size(value, value), new OpenCvSharp.Point(-1, -1), true, border);
                    break;
                case 4: Cv2.MedianBlur(ori, ori, value);
                    break;
                case 5:
                    {
                        Mat tmpmat = new Mat();
                        Cv2.BilateralFilter(ori, tmpmat, value, 3, 3, border);
                        ori = tmpmat.Clone();
                        break;
                    }
                default: Cv2.GaussianBlur(ori, ori, new OpenCvSharp.Size(value, value), 0, 0, border);
                    break;
            }
            
            return ori;
        }
        private void findContours(Mat bin)
        {
            OpenCvSharp.RetrievalModes retrieval;
            OpenCvSharp.ContourApproximationModes approxi;

            switch ((int)procSet["retrieval"])
            {
                case 0: retrieval = RetrievalModes.Tree;
                    break;
                case 1: retrieval = RetrievalModes.External;
                    break;
                case 2: retrieval = RetrievalModes.List;
                    break;
                case 3: retrieval = RetrievalModes.CComp;
                    break;
                default: retrieval = RetrievalModes.Tree;
                    break;
            }
            switch ((int)procSet["approxi"])
            {
                case 0: approxi = ContourApproximationModes.ApproxTC89KCOS;
                    break;
                case 1: approxi = ContourApproximationModes.ApproxNone;
                    break;
                case 2: approxi = ContourApproximationModes.ApproxSimple;
                    break;
                case 3: approxi = ContourApproximationModes.ApproxTC89L1;
                    break;
                default: approxi = ContourApproximationModes.ApproxTC89KCOS;
                    break;
            }
            Cv2.FindContours(bin, out contours, out hierarchy, retrieval, approxi);
        }
        private Tuple<double, double> comp2mm(double width, double height)
        {
            double lo = width;
            double sh;

            if (lo < height)
            {
                sh = lo;
                lo = height;
            }
            else
                sh = height;


            return new Tuple<double, double>(lo,sh);
        }
        public Bitmap proc()
        {
            if ((bool)preSet["customSize"])
            {
                mmX = (double)preSet["customWidth"];
                mmY = (double)preSet["customHeight"];
                setImgInfo(true);
            }
            else
                setImgInfo();

            Mat ori = oriImg.Clone();
            int bri = (int)preSet["brightness"]-100;
            ori.ConvertTo(ori, -1, 1, bri);
            
            if ((int)preSet["filterValue"] > 0 && (int)preSet["filter"] > 0) ori = applyFilter(ori);
            Mat bin = ori.Clone();
            Mat dst = ori.Clone();

            List<OpenCvSharp.Point[]> cnt_contours = new List<OpenCvSharp.Point[]>();
            List<OpenCvSharp.Point[]> ex_contours = new List<OpenCvSharp.Point[]>();
            
            Cv2.CvtColor(ori, bin, ColorConversionCodes.BGR2GRAY);
            //bin = rotateMat(bin, -(int)preSet["rotate"]);
            Cv2.Threshold(bin, bin, (int)procSet["threshold"], 255, ThresholdTypes.Binary);
            findContours(bin);

            int cnt = 0;
            cell_centers = new ArrayList(0);
            foreach (OpenCvSharp.Point[] p in contours)
            {
                RotatedRect rect = Cv2.MinAreaRect(p);
                //double length = Cv2.ContourArea(p, true) * (pixelSize);
                (double lo, double sh) = comp2mm((rect.Size.Width*pixelWidth),(rect.Size.Height*pixelHeight));
                    
                if ((int)procSet["areaMin"] <= lo && lo <= (int)procSet["areaMax"])
                {
                    cnt_contours.Add(p);
                    cnt++;
                    cell_centers.Add(rect.Center);
                }
                else
                {
                    ex_contours.Add(p);
                }
                    
            }
            currCellCnt = cnt;

            JToken exArea = postSet["exclusionArea"];
            JToken cntArea = postSet["countArea"];
            JToken lset = postSet["label"];
            JToken bset = postSet["bound"];
            bool label = (bool)lset["enable"];
            bool bound = (bool)bset["enable"];

            if ((int)exArea["thickness"] != 0)
                Cv2.DrawContours(dst, ex_contours, -1, new Scalar((int)exArea["B"], (int)exArea["G"], (int)exArea["R"]), (int)exArea["thickness"]);
            if ((int)cntArea["thickness"] != 0)
                Cv2.DrawContours(dst, cnt_contours, -1, new Scalar((int)cntArea["B"], (int)cntArea["G"], (int)cntArea["R"]), (int)cntArea["thickness"]);
            
            // label | bound
            if (label || bound) 
            {
                LineTypes lineType = LineTypes.AntiAlias;
                if (label)
                {
                    switch ((int)lset["lineType"])
                    {
                        case 1:
                            lineType = LineTypes.Link4;
                            break;
                        case 2:
                            lineType = LineTypes.Link8;
                            break;
                    }
                }
                
                int i = 0;
                foreach (OpenCvSharp.Point[] p in cnt_contours)
                {
                    Point2f center;
                    float radius;
                    
                    Cv2.MinEnclosingCircle(p, out center, out radius);
                    Rect boundingRect = Cv2.BoundingRect(p);
                    RotatedRect rect = Cv2.MinAreaRect(p);


                    if (bound)
                    {
                        for (int j = 0; j < 4; j++) 
                        { 
                            Cv2.Line(dst, new OpenCvSharp.Point(rect.Points()[j].X, rect.Points()[j].Y), 
                                new OpenCvSharp.Point(rect.Points()[(j + 1) % 4].X, rect.Points()[(j + 1) % 4].Y),
                                new Scalar((int)bset["B"], (int)bset["G"], (int)bset["R"]), 1, LineTypes.AntiAlias); 
                        }
                        //Cv2.Ellipse(dst, rotatedRect, new Scalar((int)bset["B"], (int)bset["G"], (int)bset["R"]), 1);
                        //Cv2.Rectangle(dst, boundingRect, new Scalar((int)bset["B"], (int)bset["G"], (int)bset["R"]), 1);
                        //Cv2.Circle(dst, (int)center.X,(int)center.Y, (int)radius, new Scalar((int)bset["B"], (int)bset["G"], (int)bset["R"]), 1);
                    }

                    if (label)
                    {
                        (double mmlong, double mmshort) = comp2mm((rect.Size.Width * pixelWidth), (rect.Size.Height * pixelHeight));
                        string istr = string.Format("{0:F"+lset["decimal"]+"}", mmlong) + "x" + string.Format("{0:F" + lset["decimal"] + "}", mmshort);
                        Cv2.PutText(dst, istr.ToString(), new OpenCvSharp.Point((int)center.X, (boundingRect.Bottom + ((double)lset["scale"]/10))),
                        HersheyFonts.HersheySimplex, ((double)lset["scale"] / 100), new Scalar((int)lset["B"], (int)lset["G"], (int)lset["R"]), 1, lineType);
                    }
                    i++;
                }
            }

            return Mat2bm(dst);

        }
    }
}
