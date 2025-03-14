[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/KH12NQCm)
# ES-A2

# Kickstarting your Infrastructure Simulator journey

In this year's laboratory classes, we will embark on an exciting journey to develop a single project that integrates multiple components. We will apply software design principles and design patterns to ensure a robust system by the end of the year.

The project will be an Infrastructure Simulator, which will emulate the growth of usage and the impact on the various servers of the infrastructure. The goal at the end is to have an infrastructure that supports 200,000 users. 

A project like this will help you practice the concepts from the T and TP classes and introduce some concepts in system scalability. Get ready to dive into coding!

## Introduction

Starting from the beginning! We will need to create a project to support our InfrastructureSimulator. We will introduce you to Blazor, a web framework by Microsoft that lets you build interactive applications using C# and .NET.

Blazor supports both client-side execution with WebAssembly (Blazor WebAssembly) and server-side rendering with SignalR (Blazor Server), enabling a flexible development approach.

## Part 1: Create Your Server-Side Blazor Project

### Clone this repository
Get the GitHub repository SSH link on top of this page and execute the following command, on the root folder of the repo (where you can see the readme file):

```bash
git clone github-link
```

### Create the Project:
Open your terminal and run the following command to create a new Blazor Server project named InfraSim:

```bash
dotnet new blazorserver -o InfraSim
```

### Run the Project:
Navigate into your project folder and run:

```bash
cd InfraSim
dotnet run
```

Your terminal will display a URL (e.g., `https://localhost:xxxx`). Open it in your browser to see your app in action.

### Explore the Project Structure:
- **wwwroot**: Holds static content (images, JavaScript, etc.).
- **Pages**: Contains Razor components that demonstrate how Blazor works.
- **Shared**: Contains shared components, such as navigation

### Commit Your Changes:
🏁 **Tip**: Commit your project setup with a message like “Initial Blazor project”.

```bash
git add .
git commit -m “Initial blazor project” 

```

## Part 2: Add a Counter with a Button

Let’s include a number and a button on the page, as well as the logic to increment from 1 to 100 when the user presses the button.

Code inside razor components are created inside a code block:

```csharp
@code {

}
```

### Update the Index Page:
Open `Index.razor` in the `Pages` folder and add:
- A numeric variable (e.g., a Counter).
- HTML to display the variable.
- A button element wired to an event handler that will trigger incrementation.

**Tip**:
- Use the `@` symbol to transition from static HTML to C# code.
- Add an event handler for the button (e.g., `@onclick="StartIncrementing"`).
- Inside StartIncrementing, you’ll need to build your logic to increment the counter.


### 🏁  Commit Your Changes


## Part 3: Make the Incrementation Asynchronous

### Problem to Solve:
You might notice the counter does not visibly update on the page. This is because the loop completes before the UI can re-render.

To increment the number and display it on a page, you need to make it asynchronous and introduce a delay. This allows the page to render between its cycles before returning to the cycle.

To achieve this, we must begin working with async methods. To transform it into async, you’ll need to add the keyword async to the method declaration, and the return type must be a Task, which represents the ongoing execution of the method.


**Tip**:
- Convert the incrementing method into an asynchronous method:
```csharp
protected async Task StartIncrementing()
```
- Introduce a delay (using something like `Task.Delay`) within your loop:
```csharp
await Task.Delay(100);
```
- Invoke a UI refresh method (for example, `StateHasChanged`) after each delay to show the updates.

### 🏁  Commit Your Changes


## Part 4: Refactor for Better Structure

Following the Single Responsibility Principle (SRP), the logic for updating the counter should be separated from your Razor component.

### Create a New Class:
Create a class (e.g., `UserCounter`) in a suitable folder (like `Services`) to encapsulate the counter logic.

<div style="text-align: center;">
  <img src="./Images/UserCounter.png" alt="UserCounter">
</div>

**Tip**:
- The class should hold a counter value, a callback action to notify changes, and an asynchronous method to perform the incrementation.

- The `OnCounterChanged` will be a callback for the razor page to ensure we can inform the page the counter has changed, and enforce the re-render of the component.

- On the incremental loop, you can execute the callback calling Invoke method:
```csharp
OnCounterChanged?.Invoke();
```

- In your Razor component, instantiate the class and subscribe to its callback in the component’s lifecycle method (like `OnInitialized`).

- Unsubscribe from the event in the component’s `Dispose` method to avoid memory leaks. 

```csharp
public void Dispose()
{
    UserCounter.OnCounterChanged -= MethodName; // Avoid memory leaks
}

```

### 🏁  Commit Your Changes

## Part 5: Isn't the goal to reach 200,000 users?

### Objective:
Modify your application so the counter increments from 1 to 200,000 instead of 1 to 100. You won't want to wait a long time to reach 200,000, so find a way to get to the end of the cycle in less than a minute.



### 🏁  Commit Your Changes

## Final Reminder

**⚠️ Don't Forget:** Once you have completed all parts of the assignment, push your code to your remote repository.

Happy coding and enjoy exploring Blazor! Use these guidelines and tips to build your solution. Remember, the goal is to experiment, raise questions and learn by doing.

