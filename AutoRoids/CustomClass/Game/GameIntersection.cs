//using Autodesk.AutoCAD.Geometry;
//using AutoRoids;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutoRoids
//{
//    internal class GameIntersection
//    {
//        internal List<Point2d> lstIntersection;

//        internal List<int> lstRockRow;
//        internal List<int> lstBorderRow;

//        internal List<List<Point2d>> lstLstBorderIntersection;
//        internal List<List<Point2d>> lstLstRockIntersection;
//        internal List<List<Point2d>> lstLstNewRock;

//        internal List<GameLine> lstBorderLine;
//        internal List<GameLine> lstRockLine;

//        internal List<Boolean> lstIsHidden;

//        internal List<List<Point2d>> lstBorderGroup;
//        internal List<List<Point2d>> lstRockGroup;

//        internal Boolean bolIsBorder;

//        internal GameIntersection(Boolean bolIsBorder)
//        {
//            this.lstIntersection = new List<Point2d>();

//            this.lstRockRow = new List<int>();
//            this.lstBorderRow = new List<int>();

//            this.lstLstBorderIntersection = new List<List<Point2d>>();
//            this.lstLstRockIntersection = new List<List<Point2d>>();
//            this.lstLstNewRock = new List<List<Point2d>>();

//            this.lstBorderLine = new List<GameLine>();
//            this.lstRockLine = new List<GameLine>();

//            this.lstIsHidden = new List<bool>();

//            this.lstBorderGroup = new List<List<Point2d>>();
//            this.lstRockGroup = new List<List<Point2d>>();

//            this.bolIsBorder = bolIsBorder;
//        }
//    }
//}