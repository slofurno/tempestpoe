var fs = require('fs');

var mapNames = ["Crypt",
"The Cowards Trial",
"Desert",
"Dunes",
"Dungeon",
"Grotto",
"Pit",
"Tropical Island",
"Untainted Paradise",
"Arcade",
"Cemetery",
"Channel",
"Mountain Ledge",
"Maelstrom of Chaos",
"Sewer",
"Thicket",
"Wharf",
"The Apex of Sacrifice",
"Ghetto",
"Mud Geyser",
"Museum",
"Quarry",
"Reef",
"Mao Kun",
"Spider Lair",
"Vaal Pyramid",
"Vaults of Atziri",
"Arena",
"Overgrown Shrine",
"Actons Nightmare",
"Promenade",
"Hall of Grandmasters",
"Shore",
"Spider Forest",
"Tunnel",
"Bog",
"Coves",
"Graveyard",
"Pier",
"Underground Sea",
"Villa",
"Arachnid Nest",
"Catacomb",
"Colonnade",
"Dry Woods",
"Poorjoys Asylum",
"Strand",
"Temple",
"Whakawairua Tuahu",
"Jungle Valley",
"Labyrinth",
"Mine",
"Obas Cursed Trove",
"Torture Chamber",
"Waste Pool",
"Canyon",
"Cells",
"Dark Forest",
"Dry Peninsula",
"Orchard",
"Arid Lake",
"Gorge",
"Residence",
"Underground River",
"Bazaar",
"Death and Taxes",
"Necropolis",
"Plateau",
"Volcano",
"Academy",
"Crematorium",
"Precinct",
"Springs",
"Arsenal",
"Overgrown Ruin",
"The Alluring Abyss",
"Courtyard",
"Excavation",
"Wasteland",
"Waterways",
"Maze",
"Olmecs Sanctum",
"Palace",
"Shrine",
"Vaal Temple",
"Abyss",
"Colosseum"];

var maps = {};

mapNames.forEach(function(name){
  var key = name.toLowerCase().replace(" ","_");
  maps[key]=name;

});

var json = JSON.stringify(maps);
console.log(json);