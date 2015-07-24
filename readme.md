# AWSM
Ein minimalistisches Tower Defence

*von Jonathan Wiemers und Jonas Gerdes*
## Features

 - EventBus/Stream  
	- Zeitlich verzögerbare Events
	- Alle Listener eines Eventtypens können auf einen Schlag entfernt werden
	  - spezielle persitente Listener sind davon nicht betroffen
 - Screenstack 
	- Stapeln von Screens ermöglich "Durchsicht"
	  - Jeder Screen definiert, ob Screens unter ihm geupdatet und/oder gezeichnet werden
	- Shader können auf kompletten Screen angewandt werden
- Skalierbar
  - Keine Verzerrung - UI passt sich an, sichtbarer Kartenausschnitt größer
- Sehr Datadriven
  - Level werden komplett aus Datein gelesen (Gegner, Türme, Wellen, Farben des UIs, Musik usw)
  - Einfaches hinzufügen neuer Level durch hinzufügen eines Ornders mit den entsprechenden Daten
 - Minimalistische Grafike (alle Grafiken selbst gemacht), dazu viele Animationen
 - Nette Musik (Musik komplett selbst gemacht - Sounds nicht)
 - Isometrische Ansicht
 - Zentrales Verwalten aller Assets in einem AssetManager, werden in einem Ladeschirm geladen

## Spielprinzip
Der Spieler hat eine Auswahl von __Türmen__, die er für __Energie__ kaufen und bauen kann. Dies Türme können in einen gewissen Radius mit einer gewissen Frequenz schießen. Mit ihnen muss der Spieler verhindern, dass die __Gegner__ er schaffen, auf ihrem definierten Weg das Ende des Levels zu erreichen. Sollte dies eine gewisse Anzahl an Gegner gelingen, verliert der Spieler und muss/kann das Level erneut spielen. Abgeschossene Gegner verlieren Energie, die der Spieler bekommt und somit neue Türme bauen kann. Die Gegner erscheinen in Wellen, die der Spieler nacheinander auslösen kann. Sind alle Wellen vorüber, hat der Spieler gewonnen.
## Steuerung
##### Überall
 - Escape zum Beenden des Spiels
##### Im Menü
 - Pfeiltasten oder W-A-S-D zu Auswahl
 - Bestätigen mit ENTER
##### Im Spiel
 - Pfeiltasten oder W-A-S-D oder Maus an den Rand bewegen: Verschieben der Ansicht
 - 1, 2, 3, ...: Auswahl eines Turmes
 - Klicken: Ausgewählten Turm an Mausposition bauen
 - N oder ENTER: Nächste Welle starten (Wenn vorherige vorbei ist)
 - Q: Alle Türme abwählen
 - M: Zurück ins Hauptmenü
##### "Cheats"
 - G: Sofortiges Gewinnen des Levels
 - L: Sofortiges Verlieren des Levels

## Hinweise/Schwachstellen/Verbesserungen/zukünftige Features
 - Zur Zeit ist nur das erste Level einigermaßen ausbalanciert. Die anderen beiden sind noch zu einfach.
 - Bei sehr vielen Gegnern und Türmen fängt das Spiel merklich an zu ruckeln. Ein Insertsort für alle Entities, das Entfernen der Fluganimation der Partikel und weitere Tweaks haben leider keine Abhilfe geschaffen
 - Hin und wieder kann es vorkommen (vielleicht ist es auch schon gefixt...), dass man zwei Türme auf ein Tile baut und dadurch negative Engerie bekommen kann.
 - Mehr Level - Freischalten von neuen Leveln durch meistern von vorherigen
 - Menüsteuerung und Towerauswahl mit Maus