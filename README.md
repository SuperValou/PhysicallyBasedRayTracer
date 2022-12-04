# Physically-Based Ray Tracer
**This is a learning project.** 

The main objective of this repository is to implement a small C# version of the physically-based ray tracer described in [Physically Based Rendering – From Theory to Implementation](https://www.pbr-book.org/). This book describes "both the mathematical theory behind a modern photorealistic rendering system as well as its practical implementation".

Obviously, this repository does not aim to match the quality or feature set available in the [official pbrt implementation](https://github.com/mmp/pbrt-v3). This project will be considered done once the following image can be rendered:

![book](https://pbrt.org/scenes-v3_images/book.jpg)

# Book reading progress

(First reading of the book)

- [x] 1 Introduction
  - [x] 1.1 Literate Programming
  - [x] 1.2 Photorealistic Rendering and the Ray-Tracing Algorithm
  - [x] 1.3 pbrt: System Overview
  - [x] 1.4 Parallelization of pbrt
  - [x] 1.5 How to Proceed through This Book
  - [x] 1.6 Using and Understanding the Code
  - [x] 1.7 A Brief History of Physically Based Rendering
  
- [x] 2 Geometry and Transformations
  - [x] 2.1 Coordinate Systems
  - [x] 2.2 Vectors
  - [x] 2.3 Points
  - [x] 2.4 Normals
  - [x] 2.5 Rays
  - [x] 2.6 Bounding Boxes
  - [x] 2.7 Transformations
  - [x] 2.8 Applying Transformations
  - [x] 2.9 Animating Transformations
  - [x] 2.10 Interactions
- [ ] 3 Shapes
  - [x] 3.1 Basic Shape Interface
  - [x] 3.2 Spheres
  - [ ] 3.3 Cylinders
  - [ ] 3.4 Disks
  - [ ] 3.5 Other Quadrics
  - [x] 3.6 Triangle Meshes
  - [ ] 3.7 Curves
  - [ ] 3.8 Subdivision Surfaces
  - [x] 3.9 Managing Rounding Error
- [ ] 4 Primitives and Intersection Acceleration
  - [x] 4.1 Primitive Interface and Geometric Primitives
  - [x] 4.2 Aggregates
  - [x] 4.3 Bounding Volume Hierarchies
  - [ ] 4.4 Kd-Tree Accelerator

- [x] 5 Color and Radiometry
  - [x] 5.1 Spectral Representation
  - [x] 5.2 The SampledSpectrum Class
  - [x] 5.3 RGBSpectrum Implementation
  - [x] 5.4 Radiometry
  - [x] 5.5 Working with Radiometric Integrals
  - [x] 5.6 Surface Reflection
- [x] 6 Camera Models
  - [x] 6.1 Camera Model
  - [x] 6.2 Projective Camera Models
  - [x] 6.3 Environment Camera
  - [x] 6.4 Realistic Cameras
- [ ] 7 Sampling and Reconstruction
  - [ ] 7.1 Sampling Theory
  - [ ] 7.2 Sampling Interface
  - [ ] 7.3 Stratified Sampling
  - [ ] 7.4 The Halton Sampler
  - [ ] 7.5 (0, 2)-Sequence Sampler
  - [ ] 7.6 Maximized Minimal Distance Sampler
  - [ ] 7.7 Sobol’ Sampler
  - [ ] 7.8 Image Reconstruction
  - [x] 7.9 Film and the Imaging Pipeline

- [ ] 8 Reflection Models
  - [x] 8.1 Basic Interface
  - [ ] 8.2 Specular Reflection and Transmission
  - [ ] 8.3 Lambertian Reflection
  - [ ] 8.4 Microfacet Models
  - [ ] 8.5 Fresnel Incidence Effects
  - [ ] 8.6 Fourier Basis BSDFs
- [ ] 9 Materials
  - [ ] 9.1 BSDFs
  - [ ] 9.2 Material Interface and Implementations
  - [ ] 9.3 Bump Mapping
- [ ] 10 Texture
  - [ ] 10.1 Sampling and Antialiasing
  - [ ] 10.2 Texture Coordinate Generation
  - [ ] 10.3 Texture Interface and Basic Textures
  - [ ] 10.4 Image Texture
  - [ ] 10.5 Solid and Procedural Texturing
  - [ ] 10.6 Noise
- [ ] 11 Volume Scattering
  - [ ] 11.1 Volume Scattering Processes
  - [ ] 11.2 Phase Functions
  - [ ] 11.3 Media
  - [ ] 11.4 The BSSRDF
- [ ] 12 Light Sources
  - [ ] 12.1 Light Emission
  - [ ] 12.2 Light Interface
  - [ ] 12.3 Point Lights
  - [ ] 12.4 Distant Lights
  - [ ] 12.5 Area Lights
  - [ ] 12.6 Infinite Area Lights

- [ ] 13 Monte Carlo Integration
  - [ ] 13.1 Background and Probability Review
  - [ ] 13.2 The Monte Carlo Estimator
  - [ ] 13.3 Sampling Random Variables
  - [ ] 13.4 Metropolis Sampling
  - [ ] 13.5 Transforming between Distributions
  - [ ] 13.6 2D Sampling with Multidimensional Transformations
  - [ ] 13.7 Russian Roulette and Splitting
  - [ ] 13.8 Careful Sample Placement
  - [ ] 13.9 Bias
  - [ ] 13.10 Importance Sampling
- [ ] 14 Light Transport I: Surface Reflection
  - [ ] 14.1 Sampling Reflection Functions
  - [ ] 14.2 Sampling Light Sources
  - [ ] 14.3 Direct Lighting
  - [ ] 14.4 The Light Transport Equation
  - [ ] 14.5 Path Tracing
- [ ] 15 Light Transport II: Volume Rendering
  - [ ] 15.1 The Equation of Transfer
  - [ ] 15.2 Sampling Volume Scattering
  - [ ] 15.3 Volumetric Light Transport
  - [ ] 15.4 Sampling Subsurface Reflection Functions
  - [ ] 15.5 Subsurface Scattering Using the Diffusion Equation
- [ ] 16 Light Transport III: Bidirectional Methods
  - [ ] 16.1 The Path-Space Measurement Equation
  - [ ] 16.2 Stochastic Progressive Photon Mapping
  - [ ] 16.3 Bidirectional Path Tracing
  - [ ] 16.4 Metropolis Light Transport


# Limitations
To keep this project doable on a realistic time frame and avoid overambitious expectations, here is a (still growing) list of skipped content to improve iteration time:
- Anti-aliasing isn't supported
	- The RayDifferential class is not implemented
	- Cameras do not generate RayDifferentials
	- [Partial derivatives of normal vectors](https://www.pbr-book.org/3ed-2018/Shapes/Spheres#PartialDerivativesofNormalVectors) are not computed
- Volumetric scattering isn't supported
	- [Medium](https://www.pbr-book.org/3ed-2018/Volume_Scattering/Media#) is not implemented
	- [Rays](https://pbr-book.org/3ed-2018/Geometry_and_Transformations/Rays#) and [cameras](https://www.pbr-book.org/3ed-2018/Camera_Models/Camera_Model#) don't hold any reference to a medium
	- [MediumInteraction](https://pbr-book.org/3ed-2018/Volume_Scattering/Media#MediumInteractions) is not implemented ([SurfaceInteraction](https://pbr-book.org/3ed-2018/Geometry_and_Transformations/Interactions#SurfaceInteraction) is the only interaction available)
- [Animations](https://www.pbr-book.org/3ed-2018/Geometry_and_Transformations/Animating_Transformations#) are entirely discarded
	- [Cameras](https://www.pbr-book.org/3ed-2018/Camera_Models/Camera_Model#) can't have motion blur
	- [TransformedPrimitives](https://www.pbr-book.org/3ed-2018/Primitives_and_Intersection_Acceleration/Primitive_Interface_and_Geometric_Primitives#TransformedPrimitive:ObjectInstancingandAnimatedPrimitives) are discarded
- Spectral power distributions (SPD) are only represented by the [Spectrum class](https://www.pbr-book.org/3ed-2018/Color_and_Radiometry/Spectral_Representation#TheSpectrumType) implementing RGB sampling directly
	- The RGBSpectrum and CoefficientSpectrum types don't exist, as they are merged into the Spectrum type
	- [SampledSpectrum](https://www.pbr-book.org/3ed-2018/Color_and_Radiometry/The_SampledSpectrum_Class#) is not implemented
- Only perspective cameras are supported
	- [Orthographic Camera](https://www.pbr-book.org/3ed-2018/Camera_Models/Projective_Camera_Models#OrthographicCamera) is not implemented
	- [EnvironmentCamera](https://www.pbr-book.org/3ed-2018/Camera_Models/Environment_Camera#) is not implemented
	- [Realistic cameras](https://www.pbr-book.org/3ed-2018/Camera_Models/Realistic_Cameras#) are entirely discarded
	- [Camera sampling](https://pbr-book.org/3ed-2018/Camera_Models/Camera_Model#) is reduced to a Vector2 holding the film coordinates being sampled
- [Sampling](https://pbr-book.org/3ed-2018/Sampling_and_Reconstruction/Sampling_Theory#) is currently dumbed down to sampling directly at the exact pixel position
- [Shape intersection](https://www.pbr-book.org/3ed-2018/Shapes/Basic_Shape_Interface#IntersectionTests) tests do not support cutting away some of the shape surfaces using a texture
- Shapes are simplified
	- Sphere are always full (they can't be partial sweeps)
- [Error analysis](https://www.pbr-book.org/3ed-2018/Shapes/Managing_Rounding_Error#) on float arithmetic is ignored
- The only Acceleration Structures implemented is a Kd-Tree
	- [Bounding Volume Hierarchies](https://www.pbr-book.org/3ed-2018/Primitives_and_Intersection_Acceleration/Bounding_Volume_Hierarchies#) are skipped
