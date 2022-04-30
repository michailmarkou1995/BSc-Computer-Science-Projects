DriveAR Codelab for Unity AR Foundation
============

```This project contains Prototype code and not Design Patterns```

This repository contains sample assets that accompany the [Create an AR game using Unity's AR Foundation Codelab](https://codelabs.developers.google.com/arcore-unity-ar-foundation/).

Steps (if you follow the tutorial here is the complete so there is no need for steps):
1) Create 3D URP project or AR project from Unity template (2021.3.1f1)
2) Depending what you choose then install from Package manager AR, XR foundation or/and Universal pipeline
3) Create a new asset -> Rendering -> URP Asset (with Universal Renderer) after click hit enter
4) Edit -> Project Settings -> Graphics .. and assign the new URP asset (this also autmatically will attach to AR Camera as renderer)
5) From the new Third URP asset Created go to New Universal Render Pipeline Asset_Renderer and attach a component to it "AR Background Renderer"
6) Then switch platform to Android
7) Import any .unitypackage (if imported from before convert material to URP from edit -> Rendering -> Materials menu)

on build first make a keystore as well before testing

License
-------

Copyright 2021 Google, Inc.

Licensed to the Apache Software Foundation (ASF) under one or more contributor
license agreements.  See the NOTICE file distributed with this work for
additional information regarding copyright ownership.  The ASF licenses this
file to you under the Apache License, Version 2.0 (the "License"); you may not
use this file except in compliance with the License.  You may obtain a copy of
the License at

  http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the
License for the specific language governing permissions and limitations under
the License.
