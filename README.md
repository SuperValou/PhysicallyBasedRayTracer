# Physically-Based Ray Tracer
**This is a learning project.** The main objective of this repository is to implement a small C# version of the physically-based ray tracer described in [Physically Based Rendering – From Theory to Implementation](https://www.pbr-book.org/). This book describes "both the mathematical theory behind a modern photorealistic rendering system as well as its practical implementation".

Obviously, this repository does not aim to match the quality or feature set available in the [official pbrt implementation](https://github.com/mmp/pbrt-v3). This project will be considered done once the following image can be rendered:

![book](https://pbrt.org/scenes-v3_images/book.jpg)

# Book reading progress

- [x] 1 Introduction
  - [x] 1.1 Literate Programming
  - [x] 1.2 Photorealistic Rendering and the Ray-Tracing Algorithm
  - [x] 1.3 pbrt: System Overview
  - [x] 1.4 Parallelization of pbrt
  - [x] 1.5 How to Proceed through This Book
  - [x] 1.6 Using and Understanding the Code
  - [x] 1.7 A Brief History of Physically Based Rendering
  
- [ ] 2 Geometry and Transformations
  - [x] 2.1 Coordinate Systems
  - [x] 2.2 Vectors
  - [x] 2.3 Points
  - [ ] 2.4 Normals
  - [ ] 2.5 Rays
  - [ ] 2.6 Bounding Boxes
  - [ ] 2.7 Transformations
  - [ ] 2.8 Applying Transformations
  - [ ] 2.9 Animating Transformations
  - [ ] 2.10 Interactions
- [ ] 3 Shapes
  - [ ] 3.1 Basic Shape Interface
  - [ ] 3.2 Spheres
  - [ ] 3.3 Cylinders
  - [ ] 3.4 Disks
  - [ ] 3.5 Other Quadrics
  - [ ] 3.6 Triangle Meshes
  - [ ] 3.7 Curves
  - [ ] 3.8 Subdivision Surfaces
  - [ ] 3.9 Managing Rounding Error
- [ ] 4 Primitives and Intersection Acceleration
  - [ ] 4.1 Primitive Interface and Geometric Primitives
  - [ ] 4.2 Aggregates
  - [ ] 4.3 Bounding Volume Hierarchies
  - [ ] 4.4 Kd-Tree Accelerator

- [ ] 5 Color and Radiometry
  - [ ] 5.1 Spectral Representation
  - [ ] 5.2 The SampledSpectrum Class
  - [ ] 5.3 RGBSpectrum Implementation
  - [ ] 5.4 Radiometry
  - [ ] 5.5 Working with Radiometric Integrals
  - [ ] 5.6 Surface Reflection
- [ ] 6 Camera Models
  - [ ] 6.1 Camera Model
  - [ ] 6.2 Projective Camera Models
  - [ ] 6.3 Environment Camera
  - [ ] 6.4 Realistic Cameras
- [ ] 7 Sampling and Reconstruction
  - [ ] 7.1 Sampling Theory
  - [ ] 7.2 Sampling Interface
  - [ ] 7.3 Stratified Sampling
  - [ ] 7.4 The Halton Sampler
  - [ ] 7.5 (0, 2)-Sequence Sampler
  - [ ] 7.6 Maximized Minimal Distance Sampler
  - [ ] 7.7 Sobol’ Sampler
  - [ ] 7.8 Image Reconstruction
  - [ ] 7.9 Film and the Imaging Pipeline

- [ ] 8 Reflection Models
  - [ ] 8.1 Basic Interface
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
