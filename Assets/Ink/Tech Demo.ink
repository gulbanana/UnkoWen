INCLUDE functions.ink

#disable brown
#disable gold
#disable russet
#disable blue
Once upon a time...
    -> dogs
    
== another_dog
-> dogs

== dogs
There {dogs!=4: were}{dogs==4: was} {print_num(5-dogs)} {dogs!=4:dogs}{dogs==4:dog}.
* (brown) [Doctor Brown{gold and russet and blue:.}] -> another_dog #enable brown
* (gold) [{russet and blue and not brown: and }Madame Goldsmith{russet and blue:.}] -> another_dog #enable gold
* (russet) [{blue and not (brown and gold): and }Colonel Russet{blue:.}] -> another_dog #enable russet
* (blue) [{not (brown and gold and russet): and }Jamison Blue.] -> another_dog #enable blue
* -> 
 
- But the story continued.

* This time, there were no dogs.

- They lived happily ever after.
    -> END
