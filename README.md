# GMTKJam2024
A repository containing the code for the GMTK Jam fo 2024

# Feed The Beast : 

Nourrir une entité,qui grossit avec la nourriture. La bête dois être nourrit de manière fréquente, sinon la bête dévorre le joueur, c'est GAME OVER.

Plus le joueur mange = stocke (pour donner plus tard) plus il grandit, lui permettant de manger les bêtes les plus petite que lui, mais limitant ces mouvement.
Donc la boucle de gameplay consiste à aller chercher des resources à ramener, pour nourrir la bête.

Même améliorations au sein de la boucle de jeu et de la méta progression.
La méta progression permet de débloquer de base des améliorations plus rapidement en réduisant leurs prix voir les rendants gratuits,
La méta progression sert simplement à accélèrer la progression dans les débuts.

Le jeux est vu de haut et en 3D (Genre vu hotline Miami)

-> idées : 
 Ressource de différent type :
 - Resources statique
 - Resource qui bouge de façon linéaire 
 - Resource qui bouge avec des mouvement qu'on aura défini
 - Des monstres à manger. 

 
Il n'y a pas de mal à réutiliser des assets de boss qui deviennent des monstres de base après.

 Combat à distance ou mêlée. A vérifier 
 Système de combat complexe -> Attaque au corps à corps, Dash qui Slam, dodge roll. (Support Manette a prendre en compte)

-> Règles : 
 Contrainte timer, par exemple 20 secondes au début,.
 On ne peut pas bouffer ce qui est plus gros que nous. On ne peut que les tuer, pour ensuite les dévorers, (Est-ce-que dévorer prends du temps ?).
 Taille -> Limite les chemins possibles, offre ou limite des zones pour la nourriture. 

-> Principes :
 Resources fixes => précision, pas trop puzzle, exploration, méta progression
 Resources enemies => tailles, combat, patterns, stun

 Nourrire la bête :
 Pour nourrire la bête, le joueur devra se trouver dans une zone spécifique, plus la bête est nourrie en grande quantité d'un coups, plus de ressource d'amélioration est obtenue.
 Si le timer s'écoule, la bête mange le joueur quand celui-ci se présente, (Ou meurt un des deux)

 ->Amélioration : 
 - Nombre de Dash
 - Porté d'attaque
 - Dégats
 - Taille de base du joueur ? (A réfléchir, si tel est le cas, possibilité d'ossiler entre la taille min et max)
 - Taille des projectiles du joueurs (si projectille)
 - Vitesse de déplacement
 - Vitesse de miam des monstres ? 
 - Facteur de grossisement


 -> Carte :
 Carte Fixe fait maim avec différentes zones avec des difficultées + ou moins élevée. 
 La bête à nourrir est toujours au centre de la carte

 -> Zone : 
- Zone de base, paisible avec de faible source de nourriture

