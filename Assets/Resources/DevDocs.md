# DevDocs
## TODO

### szakdoga
#### Bevezetés végére elérhetőségi linkek: Kód, build, demó videó

##### Miért választottam a Unity-t?
##### Unreal bemutatása (2-3 oldal)
##### Godot bemutatása (2-3 oldal)
##### Unity bemutatása (2-3 oldal), plusz a használt technológiák bemutatása (tilemap extras, new input system, input fixture, stb)
#### Rendszerterv
#### Saját projekt fejlesztése
ebben a fejezetben írod le a tényleges projektedet (map generálás, a* pathfinding, stb.)

#### Tesztelés

#### Összegzés
##### Tervből mi valósult meg?
##### Merre tovább?
##### Mit csinálnék máshogy?

#### Ábrák jegyzéke
minden ábrát ide fel kell sorolni, mint egy irodalomjegzék
ha használtam lopott képet, annak adnom kell forrást (ha netről lopott, elég megadni mikor tekintettük meg legutoljára)

#### Irodalomjegyzék
Irodalomjegyzék
##### Citáld ezeket
cartoon network-öt
flash-t
CN flash játkokat is
c# goto-t
az hogy unity az market leader
nuclear throne
rogue like
godot engine
MIT licensz
#### Nyilatkozat
nem fejezet, a PDF végén olvasd el a piros szöveget

### Kérdések 
em van en dash?
milyen idezőjelet használjak?
jelenleg referálok játékok nevére közvetlenül. mekkora baj, hogyha citálva bent maradnak?
    "Cyberpunk Nuclear Throne"
Game maker sokkal közelebb ál az én játékomhoz, mint az Unreal. Melyikről írjak, írhatok-e mindkettőről?
Gondolom ne azt írjam, hogy amiatt választotam a unity-t, mivel az volt megadva, hogy abban dolgozzak, igaz?
Csak egy elég béna forrást találok arról, hogy mikor adták ki godot-nak az első verzióját, mekkora baj, ha őt használom?
Citáljam a godot eredeti készítőit?
Pont előtt, vagy után citáljak?

#### Unity Testing Framework
##### map
inventory works
escape menu works

player projectile-re aggro-rnak az enemy-k
lehessen map-en váltani kiválasztott node-ot
nem működik a weapon switching
 


Szuper chest menő spawn
item rework

tud úgy generálódni utolsó előtti sorban egy node, hogy csatlakozzon a bosshoz, és kereszbe legyenek a vonalai
tud úgy generálódni alacsony column számú map második sorában node, hogy ne tudd elérni sehonnan 

tripla pisztoly csak akkor tűnjön el, hogyha végzett a user a használatával
big lag spike

# Ápr 15 után ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

#### Pixel art


##### UI

Health renderer
Gold renderer
Scrap to heal renderer - Egy korsó, amiben annál több sör van, minél több hp-ra heal-elünk
Escape Menu
Continue Button
Exit Game button
weapon switcher

##### Encounter
Encounter Decor lerakás

##### Main Menu
Main Menu title


EZEKHEZ KELLENE EGY AVOIDMELEESTRATEGY, HOGY BE TUDD RAKNI
Ranger Gobbo standing
Ranger gobbo running



sima enemy node az elite
EAI és PS halál kódja tele van ismételt dolgokkal
az item description kódot olvasd alaposan át, mivel azt 100% deepseek írta
fegyvereknek a pozíciója ne legyen lerp-elve, csak a forgása
enemy animációnál a bool neve running, playernél isWalking, emiatt jelenleg ugyanazt a logikát külön kezeled
ne a renderer, és az attacker kezelje, a multi-fegyvert
### Új dolgok



#### Enemy variánsok
NuclearThroneBanditMovementStrategy - vagy egy helyben áll, vagy mindentől függetlenül, cél nélkül választ egy közeli pontot, és oda sétál
avoid melee range movement strategy nem mükszik

burst fire enemy
enemy, aki mepgróbál eléd lőni (gungeon veteran)

knockback, rework

több enemy-n tudjon át menni egy bullet
OnEnemyContact ScripO?
OnWallContact

### Minden ami UI
Pixel perfect camera setup
dagger bulletek le pattannak az enemy-ről

ne lehessen át futni enemy-ken 

#### itemek

remove effect függvény
vagy egy ARemovableEffect class
vagy egy bool, hogy removable
vaagy egy InvertEffectIfApplicable virtual függvény
 
stat randomizáló wrapper
follower-ek
encounter végén legyen 2 opciód, hogy melyik itemet választod
snecko eye
ricochet
gimpy
splitting tears
experimental treatment
frontal shield
lézer fegyver

soy milk 
polyphemus
nuclear throne euphoria
chain lightning
1 up 
damage reflection
non stacking damage over time jelenleg nem mükszik

#### Egyéb





#### boss/elite encounter
mini boss enemy




#### UI
escape/ pause menu
main menu
az escape menü ne használjon ilyen sok színt (előlső deszka shading = középső/ hátsó deszka alap szín) 


##### játék kinézet
A UI deszkákból össze eszkábált
Ne legyenek full fekete outline-ok
vibrant color palette
cutesy pixel art
hogyha el jutsz oda, akkor heavy gibbing, gore etc


#### Fa rule tile set
csináld meg az összes tile-t
setupold a szabályokat

### Bugok 
#### SOS
AProjWeapon.Attack() metódusra nem hat ki a forwards offset, mivel a direction paraméter normalized, és a függvényen belül sem használjuk az offsetet
enemy-ket meg lehet ütni falon keresztül is fegyverrel is, hogyha elég nagy a range-e (megoldható azzal hogy van egy raycast player és projectile spawn pont közepe közt)
Run towards player movement strategy-s enemy akkor is neki áll mozogni, hogyha csak player projectile-t lát, mivel ugyanazon a layeren van mint a player (legyen a projectile, és a player 2 külön tag)
Valamiért Már megint be tudok akadni a falba. a bal falon gyakrabban , mint az alsón, valahogy

#### Nem sürgős
y level handler jelenleg ki van kapcsolva, mivel gyorsabb lesz tőle a vízszintes mozgás, mint a függőleges. vagy javítsd meg, vagy állj át layer alapú sortingra
Jelenleg 4x próbálsz minden node-ot a bosshoz kötni (3x egy for cikulsból, 1x pedig a fallback miatt)
nézd át mindenhol jó random-ot használsz-e

### Refactor: amint meg vagy az encounter/map résszel, de bossok, és item rendszer előtt
#### Rendesen fusson az enemy AI
movement strategy job system
strategy-kbe egy flyweight pattern
Navmesh Prebake, elég hogyha az frissül, amit érint egy AActor
a knockback még mindíg nem jó érzésű

#### kód kinézet
Olvasd végig a föggvány neveket a Map.cs fileban, és nevezd át, amit át kell
szedd ki a degreesToNodeVectors- dict-et (enumba, vagy egy hard coded dict-be)
"_változóNév" legyen az összes property-s field
törölj ki minden már nem használt script-et
basically mindent át tudsz alakítani lokális függvényekre
tegyél rendet az scripts/encounter/gameplay mappádban

#### kód fenttarthatóság
írd át úgy a SightChecker-t, hogy vissza adja azt is, hogy player-t vagy wall-t ütött meg elsőnek
találj ki valamit arra, hogy ne stringeket kelljen használni a scene transition-nél
serializeField

#### Kód sebesség
playernél legyen sima update alapú dolog is, ne csak fixedupdate
ctrl shift f: "TODO"
Még egy kód review :DD
### Majd később
támadáskor adódjon össze a támadó, és a projectile sebessége
Shop encounters
Mystery node az vagy egy random másik node-ot, vagy egy saját "pick one" dialógust/encounter-t dobjon be 
Layer Sorting
### Talán
player movement, és a fegyver kezelés legyen event alapú
### Long Term
Menü
Save/load
Egyéb UI
Hangok
Zene
sok sok particle effect
## Notes to self
Ne örökölj monobehaviour-ből, csak a saját verziódból
Ne hívd meg az Update metódust, abban van a y level handler, hívd meg helyette a AdditionalUpdate metódust
AW.rotationOffset alapértelemett értéket akkor veszi fel, hogyha a sprite minecraft kard orientációban van
Sprite importnál legyen a pixels per unit 16, és legyen a pivot point center
SightChecker targetPosition nem tartalmazza az offset-elést
AOV3 gyerekeinek 4 (ha 3 gyereke van) metódust kell felülírniuk: V2-ből, V3-ból, és a 2 másik gyerekből

## Kata beszélgetés 03 15
április 15 az első techdemo leadási ideje
ápr 8 már legyen kész minden
Tutorial 
 - felkelsz egy hajón, és átveszed rajta a hatalmat
 - utána ship combat tutorial
 - utána hub tutorial 

## 03 21 
Első a combat

## 04 19 (remélhetőleg legalább is)
(ezt nem aznap írod)
ami ténylegesen megmaradt, az az, hogy te kövi hétre (ápr 26) már kész akarsz lenni a jailbreak első verziójával
(ezt kövi héten (ápr 26) írod)
lmao nvm

## 10 05 Hatvani szökőkút lonely meeting :(
Jelenleg az a terv, hogy a map slay the spire, vagy isaac szobák stílusú lesz
Minél gyorsabban végzel a mappel, annál több goldot kapsz
Elite eventeknél kapsz vagy bónusz itemeket, vagy nagyobb erő szintűeket (legyen e elite stage item tier?)
Külön slotjaid lesznek, különböző típusú upgrade-eknek, minden slotba 1 upgrade fér (Egyenlőre: Weapon, Passive. Jelenlegi terv szerint: primary weapon, secondary weapon, utility, offense, defense (a passzívokból maybe több mint 1?))
Minden item upgrade-nek van egy power level-je (think isaac tiers)
Határidőre el kéne oda jutnod, hogy meg legyen az első "Floor" (Tehát: minimum 1 boss, minimum 2 elite, minimum 2 kis encounter, 2 item slot, mindkettőbe legalább 2 item, map generálás, kijelzés)
De mielőtt ennek neki tudnál állni, legyen meg 1 basic encounter, úgy, hogy az már kinézeten, és balance-on kívül shippable legyen

## 11 09 kincsővel az ennieben, közös lonely meeting
Egy szintnek akkor legyen vége, hogyha map végi raktárat ki tudod nyitni. az ajtajához kulcsot egy random enemy dob. minél gyorsabban végzel annál jobb dolgokat kapsz.
Nagyon gondolt át meg e akarod tartani a slot rendszert. Most már hogy így aludtál rá egy párat, így már inkább jobban hajlasz RoR2, vagy Warframe kártyák szerű rendszerhez.
Do i wanna make a big number game? 
Hogyha százalékos/RoR2 itemeket akarok, akkor igen, nagy számos játéknak kell lennie
Yeah i kinda fw the warframe mod system tbh

## 11 10 literálisan másnap, stinky busz állomás
Amit kapsz goldot encounterek végén, azzal tudod feljeszteni a kapott modjaidat, nem csak lecserélgetni tudod őket
Boltban kéne hogy tudj scrap-elni (maybe legyen "sell price bónusz" bounty bizonyos itemekre?)

## 01 16 csak ho le legyen írva
Scrapyard sivatagban, (dzsungelben, furrykkel), vagy pedig maradjon a standard kalózos affair?