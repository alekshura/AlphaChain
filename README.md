# AlphaChain
In the early 2003-2004 during preparing my PhD thesis about theoretical calculations of lifetimes of Super Heavy nuclei  
I've decided to handle `Fortran` output data for each nuclei using .NET.

The aim of the calculation was to choose a "path" of consequent alpha decays for the `Darmstatium 269` and `Ds 271`.
In simple "not scientific" words to check all the posibilites for the `271Ds` (on a picture the "road is an arrows" of the decay):

![271Ds alpha decay](/assets/Ds271.png)
<p align="center"><b>Calculated single-particle spectra of nuclei belonging to the α-decay chain</b></p>

## Alpha-decay chain
To calculate this chain I had to campare the probabilites of each nuclei to emit gamma or aplpha:
- gamma - it is arrow to the bottom of nuclei
- aplha - it is an arrow to the left bottom

And that is a path of decay :)

## Code
Actually code - it is a good example of all possible coding anti-patterns used by a beginning programmer.
In my lexicon a good expression for it - `intuitive programming`. 
Finally, I've decided to show it here beacause of two things: 

- the code "brought the result" and still works - yes it is difficult to read, maintain or understand, but it works
- such "dummy" code can be an motivation or expiration to beginning programmers keep going developing their skills and believe your own!

