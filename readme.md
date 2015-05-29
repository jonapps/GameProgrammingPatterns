# ShotEmUp
## Asteroid-Klon/Neuauflage
von Jonathan Wiemers und Jonas Gerdes
### Besondere Features
 - Screenstack
  - jeder Screen auf dem Stack legt fest, ob der Screen darunter gerendert und/oder aktualisiert wird
 - animierte Sprites
  - mit einem Spritesheet leicht können durch Animationsobjekte, die u.a. die Indizes der Frames halten, mehrere Animationen realisiert werden
 - Daten zu Sprites/Entites (Framegröße, Kollisionsform etc) werden aus JSON-Dateien ausgelesen
 - Gamecontroller Tasten/Achsen können in einer input.json konfiguriert werden
 - Umfangreiches UI, viele Grafiken und Animationen
 - Verschiedene Waffen, begrenzte Monition
 - Sounds (auch den selben mehrfach gleichzeitig)
 - WaveManager und Waves für die Generierung der Asteroids und Astronauten -> dadurch einfache Erweiterung

### Verbesserungsbedürftigt/Noch zu machen

- teilweise hohes Coupling
- Wellen, Waffenstärke, Punkteverteilung etc. aus Datei aus lesen
- Belegung der Tasten/Achsen nicht nur aus Datei auslesen, sondern über einen Screen einstellbar machen
- Highscore / Wellen freischaltbar machen
- Gegner, die schießen
- Ladescreen, auf den der Assetloader Daten lädt