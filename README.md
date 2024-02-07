# Drone Service Form - C# Sem 2 Assessment 3
____________________________________________________

Description:

WPF application for managing and tracking drone service information/clients. Stores Client's name, drone model, issue, service number, service cost, and service queue type (express or regular).
The program uses two queues of Drone class type (where the client information is stored), one for the regular service queue and one for the express service queue. 
These can be dequeued when completed where they will sit in a completed list until client collection. The application uses regex to prevent incorrect user inputs and appropriate error trapping.
The UI was designed to be scalable, changing dynamically to the size of the window and locking at a minimum size to maintain usability. UI is also tab-indexed, so program can be used independently 
of the mouse if the user prefers.
