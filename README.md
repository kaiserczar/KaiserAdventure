# KaiserAdventure
Playing around with Monogame. May expand over time.

What do I actually want to do with this?

TODO:
	
	Implement Camera properly
	Basic central character
		Can use art from that one Love walking test
		Character class - player character, or all characters?
			If all characters, include external controllers that can be fit in with user input, networking, AI, etc.
		Animations of various kinds - walking already have pieces.
	Rendering Order
		How to walk behind props, then walk in front of them?
			Y axis sorting based rendering?
			IS THIS EVEN 2.5D OR JUST 2D??
	Collisions
		Collidable class? Normally I'd figure abstract, but having invisible colliders sounds nice also.
		Maybe just do circle colliders? Makes life very easy.
			If not, do just one type, circle or rectangle. Mix and match gets annoying.
		This is probably the extent of physics that I want.
	Research networking - haven't done anything with that yet.
	Level Editor
		Figure out how to integrate Monogame into Windows Forms? - see bookmark
		Lots to do - lower priority for now.
		Probably mixture of tiles and props.
		Map save format?
			Map transfer from server to client?

Done:
	Basic understanding of Monogame (doesn't feel that much different from Love)
	Implement gamestates - go back and re-add the previous states, perhaps?
	Some weird color effect thing. Definitely gonna use this for my display name if I get around to multiplayer.
		Expand to allow more manual definition of numbers?
