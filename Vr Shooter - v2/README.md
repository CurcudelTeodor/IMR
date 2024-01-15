# VR Shooter

### Week 7

* [x] Am facut scena de joc cu multa staruinta
* [x] Am adaugat 9 arme modelate frumos (AK-74u - 3 optiuni, AKM, Steyr AUG, M4, Revolver, Glock 9mm, Shotgun, UZI, AWP)
* [x] Video demonstrativ 
* [x] Poze cu scena

### Week 8

* [x] Am completat scena de joc (harta)
* [x] Am adaugat 4 arme modelate (M4A1, Glock, Pistol with silencer, MAC-10)
* [x] Am creat setup-ul initial pentru VR cu multa staruinta (de aceea se cheama v2)
* [x] Am adaugat un model de inamic cu animatii
* [x] Video demonstrativ 
* [x] Poze cu scena

### Week 9

* [x] Am creat o scena noua in care am adaugat modele noi pentru maini 
* [x] Am adaugat animatii pentru maini
* [x] Am implementat metodele de grab si pinch (cu tastatura -> cam greu de lucrat asa)
* [x] Am adugat un exemplu de interactable cu care interacationeaza mainile (il pot apuca)
* [x] Am adaugat box colliders pentru a servi drept exemplu
* [x] Video demonstrativ 
* [x] Poze cu mainile, actiunile mainilor, script-uri

### Week 10

* [x] Armele au grab pointuri si le putem ridica si arunca.
* [x] Am implementat Weapon System cu Scriptable Objects
* [x] Am sters fisiere inutile
* [x] Am vrea sa testam functionalitatile de grab si de shoot pt arme
* [x] Poze

### Week 11

* [x] Shooting for pistol_mac si M4A1 with different bullet speeds! (pistol_mac G grab, F shoot)
* [x] Scriptable Objects for pistol_mac and M4A1
* [x] Teleportation with G
* [x] Demo video

### Week 12

* [x] Sounds for guns incorporated in scriptable objects
* [x] Fire speed configurable from scriptable objects
* [x] Added realistic sounds for pistol_mac and M4A1
* [x] Added some kind of continous/burst shooting - same for all guns for now
* [x] Added a little spread for bullets so that they don't all land in the same spot
* [x] Moved the environement a little bit up on Y axis to deal with the clipping through floor teleportation (sometimes we teleported below the plane)
* [x] Removed version 1 of the project
* [x] Demo video
* [ ] Failed fixing teleportation ray bend
* [ ] Failed adding recoil ~5h-6h :((

### Week 13

* [x] Added recoil for guns
* [x] Fixed falling camera and too close to the ground 
* [x] Fixed gun not staying in the left hand while moving
* [x] Fixed issue when a sound was heard when starting the game
* [x] Fixed teleportation ray bend
* [x] Added NavMesh for the enemy 
    * The monster now tries to catch the player using AI
* [x] Added enemy system - enemy now dies after taking several shots, Hit animation plays, Death animation plays 
    * Modified the Animator
* [x] Implemented the health system for monsters 
  * Used sprites found online - quality is not great though
  * Edited anchors such that the heart icon stays fixed on the left, Border and fill stay in the center
  * Used a gradient to change the color of the fill depending on how much health the monster has. We can use either Fixed or Blend options for the gradient.
  * The Health Bar stays above the monster (Changed Canvas Render Mode from Screen Space - Overlay to World Space)
  * Added a billboard script to the canvas to make the health bar always face the Main Camera (player)
* [x] Screenshots
* [x] Demo videos

