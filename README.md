# DevBook

## Table of contents

- [Web](#web)
  - [API](#api)
   - [RESTful API](#restful-api)
- [Github](#github)
- [Frontend website performance optimization](#frontend-website-performance-optimization)
  - [Render tree](#render-tree)
  - [Optimizing the CRP](#optimizing-the-CRP)
- [TODO](#todo)

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

## Github

### Formatting the README.md
**Table of contents links** `- [Heading title whatever](#heading-title)` The #heading-title must be the same as the heading. (Uppercase becomes lowercase, space becomes "-".)

## Frontend website performance optimization

### Render tree
Making a frame.
1.  GET / HTTP / 1.1 request to a server
2.  Server responds with an HTML
3.  Browser does some look-ahead parsing and gives us nodes (html, head, link, body, section, h1, script, etc.). In Chrome devtools, it shows as "Parse HTML"
4.  DOM gets created
5.  meanwhile, CSS gets involved
6.  DOM + CSS <- Chrome devtools: "Recalculate style"
7.  The **Render Tree** gets created (DOM + CSS). It's similar to the DOM tree, but CSS is applied to it. E.g.: _display: none;_ removes a node and _selector:after { content: "xyz" }_ adds a node.

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

## TODO
- Code metrics
- Unit/component/integration tests
- SOLID
- GRASP
- end-to-end automated acceptance tests?

