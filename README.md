# DevBook

<br>

## Table of contents

- [Web](#web)
  - [API](#api)
    - [RESTful API](#restful-api)
- [Github](#github)
  - [Formatting the README.md](#formatting-the-README-md)
- [Frontend website performance optimization](#frontend-website-performance-optimization)
  - [App Lifecycles](#App-Lifecycles)
  - [Rendering a website](#render-a-website)
  - [Optimizing the CRP](#optimizing-the-CRP)
  - [Javascript](#Javascript)
    - [JIT](#JIT)
    - [Animation optimization](#Animation-optimization)
- [TODO](#todo)

<br>

## Web

### API
- Application Program Interface
- Contact provided by one piece of software to another
- Structured request and response
- (You're sitting at a table in a restaurant, the kitchen is the foreign software, and the waiter is the API)

#### RESTful API
- Representational State Transfer
- **Architecture style** for designing networked applications
- Relies on a **stateless**, **client-server** protocol, almost always **HTTP**
- Treats server objects as resources that can be created or destroyed
- Can be used by virtually any programming language

**HTTP methods**
- **GET**: Retrieve data from a specified resource (You could use GET instead of POST but the data you send (parameters) can be seen by everyone, so it's not really secure.)
- **POST**: Submit data to be processed to a specified resource
- **PUT**: Update a specified resource. (Usually need to send an id) Data is sent along with the request, just like with POST. You can's PUT with a form, you need to use ajax or some other framework solution.
- **DELETE**: Delete a specified resource. (Also needs an id)
- HEAD: (rare) Same as GET but does not return a body.
- OPTIONS: (rare) Returns the supported HTTP methods of the server.
- PATCH: (rare) Update partial resources.

**Authentication**

Some API's require authentication use their service. This could be free or paid. OAUTH is a common authentication.<br>
Some examples using curl to request the URLs and set parameters:
```
curl -H "Authorization: token OAUTH-TOKEN" https://api.github.com  // Sending the token inside the header
curl https://api.github.com/?access_token=OAUTH-TOKEN  // Send the token in the url
curl 'https://api.github.com/users/whatever?client_id=xxx&client_secret=yyy'  // Send not the token, but the generated client id and secret
```
<br>

## Github

### Formatting the README.md
**Table of contents links** `- [Heading title whatever](#heading-title)` The #heading-title must be the same as the heading. (Uppercase becomes lowercase, space becomes "-".)


<br>

## Frontend website performance optimization

### App Lifecycles

**LIAR/RAIL**: Load, Idle, Animation, Response.<br>
- _Load_ - 1s. Download and render your critical resources here.<br>
- _Idle_ - 50ms. The user needs some time to take in the page. No interactions while this is happening, the page is idle.<br>
- _Response_ - The user does something, and the site needs to respond in <= **100ms**.<br>
- _Animation_ - 16ms. **10-12ms** with the browser overhead.

1000s / 60 fps = 16 ms => 10-12 ms width browser overhead.
Try to fit all the work - JavaScript->Style->Layout->Paint->Composite - into 10-12 ms to meet 60 fps.<br>

<br>

**FLIP**: First, Last, Invert, Play<br>

<br>

<p align="center">Table of time allowances for different tasks</p>
<table align="center">
  <thead>
    <tr>
      <th></th>
      <th>Response</th>
      <th>Animation</th>
      <th>Idle</th>
      <th>Load</th>
    </tr>
    <tr>
      <th>Threshold</th>
      <th>100ms</th>
      <th>10ms</th>
      <th>50ms chunks</th>
      <th>1000ms</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>Asset load / parse</td>
      <td>Avoid</td>
      <td>Avoid</td>
      <td>Unknown</td>
      <td>400ms</td>
    </tr>
    <tr>
      <td>JS: Parse</td>
      <td>Avoid</td>
      <td>Avoid</td>
      <td>Unknown</td>
      <td>30ms</td>
    </tr>
    <tr>
      <td>JS: Execute</td>
      <td>15ms</td>
      <td>3ms</td>
      <td>Unknown</td>
      <td>60ms</td>
    </tr>
    <tr>
      <td>JS: GC</td>
      <td>Avoid</td>
      <td>Avoid</td>
      <td>Unknown</td>
      <td>20ms</td>
    </tr>
    <tr>
      <td>Blink: Style Calcs</td>
      <td>10ms</td>
      <td>1ms</td>
      <td>Unknown</td>
      <td>25ms</td>
    </tr>
    <tr>
      <td>Blink: Layout</td>
      <td>15ms</td>
      <td>3ms</td>
      <td>Unknown</td>
      <td>90ms</td>
    </tr>
    <tr>
      <td>Blink: Layer Management</td>
      <td>10ms</td>
      <td>2ms</td>
      <td>Unknown</td>
      <td>10ms</td>
    </tr>
    <tr>
      <td>Blink: Paint</td>
      <td>5ms</td>
      <td>Avoid</td>
      <td>Unknown</td>
      <td>20ms</td>
    </tr>
    <tr>
      <td>Compositor: Rasterize</td>
      <td>5ms</td>
      <td>Avoid</td>
      <td>Unknown</td>
      <td>100ms</td>
    </tr>
    <tr>
      <td>Compositor: Image Decode</td>
      <td>30ms</td>
      <td>Avoid</td>
      <td>Unknown</td>
      <td>180ms</td>
    </tr>
    <tr>
      <td>Compositor: Image Resize</td>
      <td>Avoid</td>
      <td>Avoid</td>
      <td>Unknown</td>
      <td>55ms</td>
    </tr>
    <tr>
      <td>Composite</td>
      <td>10ms</td>
      <td>2ms</td>
      <td>Unknown</td>
      <td>10ms</td>
    </tr>
  </tbody>
</table>

### Rendering a website
Making a frame:
1.  GET / HTTP / 1.1 request to a server
2.  Server responds with an HTML
3.  Browser does some look-ahead parsing and gives us nodes (html, head, link, body, section, h1, script, etc.). In Chrome devtools, it shows as "Parse HTML"
4.  DOM gets created
5.  meanwhile, CSS gets involved
6.  DOM + CSS <- Chrome devtools: "**Recalculate style**"
7.  The **Render Tree** gets created (DOM + CSS). It's similar to the DOM tree, but CSS is applied to it. E.g.: _display: none;_ removes a node and _selector:after { content: "xyz" }_ adds a node.
8.  Chrome devtools: "**Layout**" - translate the render tree to boxes
9.  Rasterizer process: Vectors (the boxes, shapes) to pixels. (drawRoundedRectangle, restore, drawPath, save, clipRoundedRectangle, drawBitmap, etc.). Chrome devtools: "**Paint**".
10.  Chrome devtools: "**Image Decode + Resize**" (it may need to be resized)
11.  Chrome devtools: "**Composite Layers**". Websites have multiple layers. The process also includes loading work done from the CPU to the GPU. The GPU is instructed to put the render up the screen.

### Optimizing the CRP

**Critical Rendering Path**

Critical resources, Critical Bytes, CRP length

**Preload scanner** 

Több script esetén hasznos. (script1, script2) Ha nem lenne, script1-nél render blocking lépne fel és csak a DOM felépítése után lépne tovább a folyamat a script2-re. A preload scanner előre szimatol hogy van-e más kritikus script ami iránt elindíthatja a requestet.
```HTML
<html>
  <head>
    <meta name="viewport" content="width=device-width,initial-scale=1.0">
    <link href="style.css" rel="stylesheet">
    <script src="script1.js"></script>
    <script src="script2.js"></script>
  ...
```

### Javascript

#### JIT
Javascript code you write isn't actually the code that runs. Modern JS engines recompile it through a Just In Time compiler to make it faster.<br>
Example

```Javascript
for (var i = 0; i < 1e5; i++) sum += v.foo(i);
return sum;
```

Translated to Chrome V8 javascript engine
```
B0
v0  BlockEntry
s72 Constant 1 [1, 1]
t15 Constant 0x5a8080b1 <true> boolean
t5  Constant 0x5a808091 <undefined>
t1  Context
t2  Parameter 0
t3  Parameter 1
v6  Simulate id=2 var[4] = t5, var[3] = t5, var[2] = t1, var[1...
v7  Goto B1

B1
v8  BlockEntry
...
```

Because of this, you don't know if any **micro-optimization** you do will actually make the code faster. JIT might "overwrite" your code. (Of course, optimization is still desired.)

```Javascript
for (var i = 0; i < len; i++) ...
OR
while (++i < len) ...
```

#### Animation optimization
**requestAnimationFrame**
The browser's takes care of when and how the animation should be run.<br>
All browser supports requestAnimationFrame, except IE 9. (Use polyfill for IE9, https://gist.github.com/paulirish/1579671)<br>
How to use it:
```Javascript
function animate() {
  // Do something
  requestAnimationFrame(nextAnimation);
}
requestAnimationFrame(animate);
```
<br>

## TODO
- Code metrics
- Unit/component/integration tests
- SOLID
- GRASP
- end-to-end automated acceptance tests?
- FLIP nek utánanézni, mi is az

