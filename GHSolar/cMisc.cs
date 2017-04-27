﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Rhino.Geometry;

/*
 * cMisc.cs
 * Copyright 2017 Christoph Waibel <chwaibel@student.ethz.ch>
 * 
 * This work is licensed under the GNU GPL license version 3.
*/

namespace GHSolar
{
    /// <summary>
    /// 
    /// </summary>
    internal static class cMisc
    {

        /// <summary>
        /// Calculate area of mesh face.
        /// source: http://james-ramsden.com/area-of-a-mesh-face-in-c-in-grasshopper/
        /// </summary>
        /// <param name="meshfaceindex"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        internal static double getMeshFaceArea(int meshfaceindex, Mesh m)
        {
            //get points into a nice, concise format
            Point3d[] pts = new Point3d[4];
            pts[0] = m.Vertices[m.Faces[meshfaceindex].A];
            pts[1] = m.Vertices[m.Faces[meshfaceindex].B];
            pts[2] = m.Vertices[m.Faces[meshfaceindex].C];
            if (m.Faces[meshfaceindex].IsQuad) pts[3] = m.Vertices[m.Faces[meshfaceindex].D];

            //calculate areas of triangles
            double a = pts[0].DistanceTo(pts[1]);
            double b = pts[1].DistanceTo(pts[2]);
            double c = pts[2].DistanceTo(pts[0]);
            double p = 0.5 * (a + b + c);
            double area1 = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            //if quad, calc area of second triangle
            double area2 = 0;
            if (m.Faces[meshfaceindex].IsQuad)
            {
                a = pts[0].DistanceTo(pts[2]);
                b = pts[2].DistanceTo(pts[3]);
                c = pts[3].DistanceTo(pts[0]);
                p = 0.5 * (a + b + c);
                area2 = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            }

            return area1 + area2;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="colourSheme"></param>
        /// <param name="quantity"></param>
        /// <param name="top"></param>
        /// <param name="low"></param>
        /// <returns></returns>
        internal static Color GetRGB(int colourSheme, double quantity, double top, double low)
        {
            double RR = 0.0;
            double GG = 0.0;
            double BB = 0.0;


            quantity = (quantity - low) / (top - low);
            double third = 1.0 / 5.0;

            switch (colourSheme)
            {
                case 0:
                    if (quantity > third && quantity <= 2.0 * third)
                    {
                        RR = (quantity - third) * (255.0 / third);
                        GG = 0.0;
                        BB = 255 - ((quantity - third) * (255.0 / third));
                    }
                    else if (quantity > 2.0 * third)
                    {
                        RR = 255.0;
                        GG = (quantity - 2.0 * third) * (255.0 / third);
                        BB = 0.0;
                    }
                    else
                    {
                        RR = 0.0;
                        GG = 0.0;
                        BB = 255.0;
                    }
                    break;
                case 1:
                    third = 1.0 / 3.0;
                    if (quantity > third && quantity <= 2.0 * third)
                    {
                        RR = (quantity - third) * (255.0 / third);
                        GG = 255.0;
                        BB = 255.0 - ((quantity - third) * (255.0 / third));
                    }
                    else if (quantity > 2.0 * third)
                    {
                        RR = 255.0;
                        GG = 255.0 - ((quantity - 2.0 * third) * (255.0 / third));
                        BB = 0.0;
                    }
                    else
                    {
                        RR = 0.0;
                        GG = quantity * (255.0 / third);
                        BB = 255.0;
                    }
                    break;
                case 2:
                    RR = quantity * (255.0 / 1);
                    GG = quantity * (255.0 / 1);
                    BB = quantity * (255.0 / 1);
                    break;
            }

            if (RR > 255) RR = 255;
            else if (RR < 0) RR = 0;
            if (GG > 255) GG = 255;
            else if (GG < 0) GG = 0;
            if (BB > 255) BB = 255;
            else if (BB < 0) BB = 0;
            return Color.FromArgb((int)RR, (int)GG, (int)BB);

        }
    
    }
}