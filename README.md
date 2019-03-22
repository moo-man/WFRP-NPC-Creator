# WFRP-NPC-Creator

This program serves as practice for implementing MVVM architecture, while also providing the utility to easily create NPCs in Warhammer Fantasty Roleplay. 

This is done simply by specifying the NPC Species, adding Careers, and setting the advancement progress of those careers. Based on the Career and how advanced they are in that career, it will allocate the advances *needed* for that advancement. Adding a Career with Advancement Level of Complete will ensure that the Character has enough Advances to Complete the career, adding Advances *if needed*. If they already have enough Advances, it will not add more.

### Creating an NPC

**Example:** The Docker's Guild in the city of Ubersreik runs a tight ring when it comes to ensuring all docking fees are paid, otherwise, cargo isn't moving. We want to make a stat block for these ruffians who call themselves stevedores. We'll say they have completed Tier 1 and Tier 2 of the Stevedore Career, so we add Dockhand and Stevedore, with Advancement Level of Complete. Additionally, to reflect their nature, they probably have some experience in the Racketeer career, so we add Thug (Tier 1), set to Complete, and Racketeer (Tier 2), set to Experienced. As a result, we get the following statblock. 

![Ubersreik Stevedore](https://i.imgur.com/B2UvG86.png)

Any aspect of the character can be redetermined, Species talents, skills, and Career characteristics, talents, skills, can all be rerolled individually. 
