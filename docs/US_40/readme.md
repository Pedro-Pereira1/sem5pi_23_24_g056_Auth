# US 40 - Know why my data is being collected

## 1. Context

* This is the first time this task is being developed

## 2. Requirements

**US 20 -** As a potential system user(ex. student, teacher), I want to register as a user of the system

## 3. Analysis

**Regarding this requirement we understand that:**

As a user, I want to know why my data is being collected.

## 4. Design

For this use case we will implement a link on the registration window and on the sidebar which will take you to
a page were you can consult that information

## 5. Implementation

```html
<div class="sub-title">
    <h2>Why is my data collected and how is it going to be used</h2>
</div>

<div class="paragraph">
    <p>RobDroneGo is committed to only processing personal data strictly necessary for the operation of our solution. The personal data we will collect for each process includes:</p>
    <p>Client Program RobDroneGo:</p>
    <p>- Identification data: name.</p>
    <p>- Contact data: mobile number, email, taxpayer number.</p>
    <p>These data are collected with the purpose of identifying each of our users and communicating with them in the event of any irregularity with their user account.</p>
    <p>Video surveillance and image capture:</p>
    <p>- Our robots and drones offer the possibility to record video and capture images inside the campus to ensure its security.</p>
    <p>Object request:</p>
    <p>- Location data: current location.</p>
    <p>These data are collected to be able to calculate the path that any of our robots have to follow in order to deliver your order.</p>

</div>
```

## 6. Observations

No additional observations.
