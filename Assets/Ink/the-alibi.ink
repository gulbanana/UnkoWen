The Alibi #format title

A man is dead, and I know who did it. But I have no proof. #enable brown

+ Doctor Bigeyes Brown[.]<> #label brown Dr. BROWN #art brown dogs/brown-greyscale

- was shot at 4 PM. The killing hour. They say he was a good boy - who isn't, these days? 

+ Somebody thought otherwise, and they had a gun. #enable red #enable red.suspect

- It had to be the secretary, Russet. Brown stole and buried his bone. Intolerable to a certain type. 
It was easy enough to find the murder weapon - an old service firearm, unregistered, the kind your grandfather left in the attic after the War. Russet has that kind of grandfather.

+ He also has an alibi. 

- From 3 to 5, Russet was playing in the park... with the most trustworthy woman in the world. He claims they spent every moment in each other's company - fetch, chasey, the whole nine yards. And his companion backs him up. #enable red.alibi
+ Madame Goldsmith is a good boy.
+ Madame Goldsmith is a very good boy.

- <> She'd never lie, not with lives on the line. #enable gold #enable gold.witness
-> who_first

== who_first ==
My only chance now is to talk to people and ask them questions. {But who?|Who else?} #plot-choices
+ [click.red] -> interview_red -> who_first
+ [click.brown] -> interview_brown -> who_first
+ [click.gold] -> interview_gold -> who_next

== who_next ==
I still have questions. #plot-choices
+ [click.red] -> accuse_red
+ [click.brown] -> interview_brown -> who_next
+ [click.gold] -> interview_gold -> who_next

== interview_brown ==
He's dead, Jim. 
    ->->
    
== interview_red ==
#format begin-interview
Subject: Rupert RUSSET #format title
"I didn't do it."
+ "Ok."
#format end-interview
- ->->
    
== interview_gold ==
#format begin-interview
Subject: Gloria GOLDSMITH #format title
"Actually, he did it." #label red.alibi No alibi
+ "Oh."
#format end-interview
- ->->
    
== accuse_red ==
#format begin-interview
Subject: Rupert RUSSET #format title
"Actually, I did it."
#format end-interview
\~fin
    -> END