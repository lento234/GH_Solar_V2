# GH_Solar_V2
Solar irradiation model plug-in for Rhinoceros Grasshopper. Version 2 with better physics than [version 1](https://github.com/christophwaibel/GH_Solar_V1).

Please refer to this publication for citation: [Waibel et al. (2017)](http://www.sciencedirect.com/science/article/pii/S0038092X17309349)

To Do:
- [x] Perez diffuse sky model
- [x] skydome with equal patches
- [x] snow cover
- [x] trees
- [x] annual interpolation trees
- [x] specular inter-reflections max 2 bounces
- [x] annual interpolation specular inter-reflections
- [x] diffuse inter-reflections max 1 bounce
- [x] annual interpolation diffuse inter-reflections
- [ ] specular inter-reflections refraction coefficients
- [x] multi-threading annual
- [ ] multi-threading hourly
- [ ] Input points and normals, instead of analysis surface mesh... more control on where to place sensor points
- [ ] precise calculation of equinox & solstice dates (SunVector.cs, int [] GetEquinoxSolstice(...))
- [ ] replace rhino libraries for geometry operations with open source libraries (https://doc.cgal.org/ ?). solar.dll should have no rhino dependency. ghsolar.gha should be the rhino wrapper
- [ ] multi-threading ray-mesh intersection (rhino library not thread-safe)


Bugs:
- [ ] Memory build up over time... get's slower over time... 
- [ ] mesh surfaces far away from the origin become crumpled when used as analysis surface 
- [ ] values underestimated for geometries close to origin (0,0,0)
- [ ] Out of Memory in Rhino... write results into .txt file, reather than into Rhino directly
- [ ] rhino geometry intersection class not thread safe
- [ ] only works properly in meter as workspace unit in Rhino
