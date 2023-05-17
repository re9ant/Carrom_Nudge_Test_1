# Carrom_Nudge_Test
 Carrom Game made for nudge test
 
 YOU NEED TO SHOOT WHITE COINS
 Max Score : 6
 
 Initially I setup the scene added sprites, scaled them, added colliders and triggers for the coin collectors
 later I started working on the striker script and I figured I need to use the negative of mouse axis to get the direction to launch the striker with
 a force which is calculated on the distance between the strikers position and the Modified Mouse Positon (negative mouse position) and caliculated the percentage
 and multiplied it with a variable named maxForce which is the maximum force 
 (example : 0.75(percentage of distance and max distance)  * 200(Max Force) = 175(Result Force))
 
 Then I worked on the physics2d materials and adjusted some values of the striker's rigidbody and I started to work on the coin collectors I used triggers to
 detect coins color or team based of their tags "White" or "Black" or "Red" the coins disable when the go in the hole and the correspoding team gets 
 (Player or Computer) gets their score
 
 Later I worked on computer's turn or AI where i simply got a "Black" Taged coin from a overlap circle cast and I got the direction from the strikers position
 to the black coin position and i launched the striker with a force which is calculated by the distance and clamped the value from (min force)-2, 2(maxForce) 
 (could've used 1 but 2 felt better) 
 
 Later I worked on the UI and Added win / lose screen with a restart and quit application button
 
#End
 
