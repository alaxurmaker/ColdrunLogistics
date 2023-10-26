# ColdrunLogistics
Exerice project for the Coldrun recruitment process.


Implemented with the below requirements:

  The goal is to create the first REST API module of the ERP application. The first requirement is to manage Trucks data. 
  It is assumed that there will be more API endpoints to manage different resources (e.g. employee, factory, customer).
  
  Each Truck:
  - must have a unique alphanumeric code
  - must have a name
  - must have a status included in the following set: "Out Of Service", "Loading", "To Job", "At Job", "Returning"
      - "Out Of Service" status can be set regardless of the current status of the Truck
      - each status can be set if the current status of the Truck is "Out of service"
      - the remaining statuses can only be changed in the following order: Loading -> To Job -> At Job -> Returning
      - when Truck has "Returning" status it can start "Loading" again.
  - may have a description
  - User must be able to perform CRUD operations on the trucks including the ability to query a filtered and sorted list of Trucks.
