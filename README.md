# DevBook

## Frontend website performance optimization
### Optimizing the CRP
**Preload scanner** Több script esetén hasznos. (script1, script2) Ha nem lenne, script1-nél render blocking lépne fel és csak a DOM felépítése után lépne tovább a folyamat a script2-re. A preload scanner előre szimatol hogy van-e más script ami iránt elindíthatja a requestet.
```HTML
<html>
  <head>
    <meta name="viewport" content="width=device-width,initial-scale=1.0">
    <link href="style.css" rel="stylesheet">
    <script src="script1.js"></script>
    <script src="script2.js"></script>
  ...
```
