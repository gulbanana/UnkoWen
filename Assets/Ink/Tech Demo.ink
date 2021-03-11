INCLUDE functions.ink
VAR dogs = 0

Once upon a time...
    -> list_dogs
    
// looks like we don't count knots without this
=== add_dog ===
-> list_dogs

=== list_dogs ===
~ dogs = 4 - add_dog

There {plural(dogs, "was", "were")} {print_num(dogs)} {plural(dogs, "dog", "dogs")}.

* (brown) [Doctor Brown{gold and red and blue:.}] -> add_dog #enable brown
* (gold) [{red and blue and not brown: and }Madame Goldsmith{red and blue:.}] -> add_dog #enable gold
* (red) [{blue and not (brown and gold): and }Colonel Russet{blue:.}] -> add_dog #enable red
* (blue) [{not (brown and gold and red): and }Jamison Blue.] -> add_dog #enable blue
* -> pet_dog
 
=== pet_dog ===
Which of these dogs would be clicked? #hide-choices

* [click.brown] A good brown boy. 
* [click.gold] A boy as good as gold.
* [click.red] A red good boy.
* [click.blue] A blue boy and a good boy.

- They lived happily ever after.
    -> END
