﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

/*
 * cObstacleObject.cs
 * Copyright 2017 Christoph Waibel <chwaibel@student.ethz.ch>
 * 
 * This work is licensed under the GNU GPL license version 3.
*/

namespace GHSolar
{
    internal class cObstacleObject
    {
        internal Mesh mesh;
        internal List<double> albedos;  //8760 values for diffuse
        internal List<double> specCoeff; //8760 values for specular
        internal int reflType;
        internal Vector3d[] normals;
        internal Vector3d[] normalsRev;
        internal Point3d[] faceCen;

        internal double tolerance;
        internal string name;

        /// <summary>
        /// Create an obstacle object, used for solar calculations.
        /// </summary>
        /// <param name="_mesh">Mesh object. Face normals are important.</param>
        /// <param name="_albedos">8760 albedo values.</param>
        /// <param name="_reflType">Reflection type. 0 = diffuse, 1 = specular (no refraction currently considered), 2 = specular and diffuse (snow), all other numbers = blind (no inter-reflections)</param>
        /// <param name="_tolerance">Tolerance, used to offset point from actual face center point, to avoid self obstruction.</param>
        /// <param name="_name">Name of the obstacle. E.g. use to indicate an analysis surface.</param>
        /// <param name="mt">Multi-threading.</param>
        internal cObstacleObject(Mesh _mesh, List<double> _albedos, List<double> _specCoeff, int _reflType, double _tolerance, string _name, bool mt)
        {
            mesh = _mesh;
            albedos = new List<double>(_albedos);
            specCoeff = new List<double>(_specCoeff);
            reflType = _reflType;
            name = _name;
            tolerance = _tolerance;

            mesh.FaceNormals.ComputeFaceNormals();

            normals = new Vector3d[mesh.Faces.Count];
            normalsRev = new Vector3d[mesh.Faces.Count];
            faceCen = new Point3d[mesh.Faces.Count];
            if (!mt)
            {
                for (int k = 0; k < mesh.Faces.Count; k++)
                {
                    normals[k] = mesh.FaceNormals[k];
                    normalsRev[k] = Vector3d.Negate(normals[k]);
                    Point3d cen0 = mesh.Faces.GetFaceCenter(k);
                    faceCen[k] = new Point3d(Point3d.Add(cen0, Vector3d.Multiply(Vector3d.Divide(normals[k], normals[k].Length), tolerance)));
                }
            }
            else
            {
                Parallel.For(0, mesh.Faces.Count, k =>
                {
                    normals[k] = mesh.FaceNormals[k];
                    normalsRev[k] = Vector3d.Negate(normals[k]);
                    Point3d cen0 = mesh.Faces.GetFaceCenter(k);
                    faceCen[k] = new Point3d(Point3d.Add(cen0, Vector3d.Multiply(Vector3d.Divide(normals[k], normals[k].Length), tolerance)));
                });
            }


        }
    }
}