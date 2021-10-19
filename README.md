# DevBook

<br>

## Table of contents

- [Command lines](#command-lines)
  - [Npm commands](#npm-commands)
  - [Angular CLI commands](#angular-cli-commands)
- [C sharp](#c-sharp)
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
- [Software development](#software-development)
  - [SOLID](#solid)
- [Testing](#testing)
  - [Unit testing](#unit-testing)
  - [Component testing](#component-testing)
  - [Integration testing](#integration-testing)
- [TODO](#todo)

<br>

## Command lines

### Npm commands

```npm install``` This command installs a package, and any packages that it depends on. If the package has a package-lock or shrinkwrap file, the installation of dependencies will be driven by that, with an npm-shrinkwrap.json taking precedence if both files exist. [Source](https://docs.npmjs.com/cli/v6/commands/npm-install) <br>
```npm ci``` Downloads node_modules according to the package-lock.json. <br>
```npm run <command>``` Run the command defined in packages.json / "scripts"

### Angular CLI commands

```ng build``` Builds and listens.

<br>

## C sharp

### Struct vs Class

Structs are light versions of classes. Structs are value types and can be used to create objects that behave like built-in types.<br>
<br>
Structs share many features with classes but with the following limitations as compared to classes.

- Struct cannot have a default constructor (a constructor without parameters) or a destructor.
- Structs are value types and are copied on assignment.
- Structs are value types while classes are reference types.
- Structs can be instantiated without using a new operator ```Coords p; p.x = 3; p.y = 4;```
- A struct cannot inherit from another struct or class, and it cannot be the base of a class. All structs inherit directly from System.ValueType, which inherits from System.Object.
- Struct cannot be a base class. So, Struct types cannot abstract and are always implicitly sealed.
- Abstract and sealed modifiers are not allowed and struct member cannot be protected or protected internals.
- Function members in a struct cannot be abstract or virtual, and the override modifier is allowed only to the override methods inherited from System.ValueType.
- Struct does not allow the instance field declarations to include variable initializers. But, static fields of a struct are allowed to include variable initializers.
- A struct can implement interfaces.
- A struct can be used as a nullable type and can be assigned a null value.

#### When to use struct or classes?

<table>
	<tr>
		<td>Struct</td>
		<td>Class</td>
	</tr>
	<tr>
		<td>Structs are value types, allocated either on the stack or inline in containing types.</td>
		<td>Classes are reference types, allocated on the heap and garbage-collected.</td>
	</tr>
	<tr>
		<td>Allocations and de-allocations of value types are in general cheaper than allocations and de-allocations of reference types.</td>
		<td>Assignments of large reference types are cheaper than assignments of large value types.</td>
	</tr>
	<tr>
		<td>In structs, each variable contains its own copy of the data (except in the case of the ref and out parameter variables), and an operation on one variable does not affect another variable.</td>
		<td>In classes, two variables can contain the reference of the same object and any operation on one variable can affect another variable.</td>
	</tr>
</table>

In this way, struct should be used only when you are sure that,
- It logically represents a single value, like primitive types (int, double, etc.).
- It is immutable. TODO what is this
- It should not be boxed and un-boxed frequently. TODO what is this
		
```C#
struct Location   
{  
    publicint x, y;  
    publicLocation(int x, int y)  
    {  
        this.x = x;  
        this.y = y;  
    }  
}  
Location a = new Location(20, 20);  
Location b = a;
a.x = 100;  
System.Console.WriteLine(a.x); // 100
System.Console.WriteLine(b.x); // 20
```


<br>

## Backend structures
### Reference projects
#### As 'git references'
You can include projects in your solution by referencing git repositories. These __modules__ are referenced in the ```.gitmodules``` file.

```
[submodule "submodules/obc.core"]
	path = submodules/obc.core
	url = git@github.com:CompanyName/obc.core.git
[submodule "submodules/obc.authentication"]
	path = submodules/obc.authentication
	url = git@github.com:CompanyName/obc.authentication.git
[submodule "submodules/obc.utils.singlesignon"]
	path = submodules/obc.utils.signon
	url = git@github.com:CompanyName/obc.utils.signon.git
...
```
#### As simply including them
organize to folders. Solution file in VS looks different than folder in windows explorer. Projects are outside of the solution's folder's root. TODO investigate how its done.
#### As references
.net framework solution (?)
#### As nuget packages
.net core solution

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

## Third party softwares

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

## Documenting

<br>

### UML
Unified Modeling Language.<br/>

#### UML types
- UML Diagrams
  - Structure
    - Class diagram
    - Component diagram
    - Object diagram
    - Composite structure diagram
    - Package diagram
    - Deployment diagram
  - Behavior
    - Use case diagram
    - Activity diagram
    - State machine diagram
    - Interaction

Some special cases:
- Interaction overview diagram: Activity + Interaction
- Composite structure
- Timing diagrams

<br>

## Software development

### SOLID

#### Single-Responsibility Principle
"A class should have one and only one reason to change, meaning that a class should have only one job."

#### Open-Closed Principle
"Objects or entities should be open for extension but closed for modification."<br>
This means that a class should be extendable without modifying the class itself.

#### Liskov Substitution Principle
"Every subclass or derived class should be substitutable for their base or parent class."<br>
"If class A is a subtype of class B, we should be able to replace B with A without disrupting the behavior of our program."

#### Interface Segregation Principle
"A client should never be forced to implement an interface that it doesn’t use, or clients shouldn’t be forced to depend on methods they do not use."<br>
"Larger interfaces should be split into smaller ones. By doing so, we can ensure that implementing classes only need to be concerned about the methods that are of interest to them."<br>

Before SOLID:
```
interface ShapeInterface {
    public function area();
    public function volume();
}
```

<br>

After SOLID:
```
interface ShapeInterface {
    public function area();
}

interface ThreeDimensionalShapeInterface {
    public function volume();
}

class Cuboid implements ShapeInterface, ThreeDimensionalShapeInterface {
    public function area() {
        // calculate the surface area of the cuboid
    }
    public function volume() {
        // calculate the volume of the cuboid
    }
}
```

#### Dependency Inversion Principle
"Entities must depend on abstractions, not on concretions. It states that the high-level module must not depend on the low-level module, but they should depend on abstractions."<br>
"The principle of dependency inversion refers to the decoupling of software modules. This way, instead of high-level modules depending on low-level modules, both will depend on abstractions."

```
class MySQLConnection {
    public function connect() {
        // handle the database connection
        return 'Database connection';
    }
}

class PasswordReminder {
    private $dbConnection;
    public function __construct(MySQLConnection $dbConnection) {
        $this->dbConnection = $dbConnection;
    }
}
```

First, the ```MySQLConnection``` is the low-level module while the ```PasswordReminder``` is high level, but according to the definition of D in SOLID, which states to Depend on abstraction, not on concretions. This snippet above violates this principle as the ```PasswordReminder``` class is being forced to depend on the ```MySQLConnection``` class. To fix this:

```
interface DBConnectionInterface {
    public function connect();
}
```

```
class MySQLConnection implements DBConnectionInterface {
    public function connect() {
        // handle the database connection
        return 'Database connection';
    }
}

class PasswordReminder {
    private $dbConnection;
    public function __construct(DBConnectionInterface $dbConnection) {
        $this->dbConnection = $dbConnection;
    }
}
```

<br>

## Testing

<br>

### Unit testing

...TODO types of testing libraries in VS and implementation examples
<br>

### Component testing

<br>

### Integration testing

<br>

## TODO
- Code metrics
- Unit/component/integration tests
- SOLID
- GRASP
- end-to-end automated acceptance tests?
- FLIP nek utánanézni, mi is az
- Webworkers (JS)
- Memeory management (JS)
- Cloudfront, what is it, how to set it up/use it

