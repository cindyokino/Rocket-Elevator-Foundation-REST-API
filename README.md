# Rocket-Elevator-Foundation-REST-API

CodeBoxx Bootcamp Week 9 - Consolidation of Achievements

### Deliverables:
REST API with 3 new contact points:
● GET: Returns all fields of all Service Request records that do not have a start date and are in "Pending" status.
● PUT: Change the status of the intervention request to "InProgress" and add a start date and time (Timestamp).
● PUT: Change the status of the request for action to "Completed" and add an end date and time (Timestamp).

### Examples:
https://rocketelevatorsrestapicindy.azurewebsites.net/api/Interventions
https://rocketelevatorsrestapicindy.azurewebsites.net/api/Interventions/5/start-progress
https://rocketelevatorsrestapicindy.azurewebsites.net/api/Interventions/1/complete-progress

### Links:
Rails application: https://cindyokino.com/

Rails repository: https://github.com/cindyokino/Rocket-Elevator-Foundation

REST API URL: https://rocketelevatorsrestapicindy.azurewebsites.net/

Postman collection for the 3 requests: https://www.getpostman.com/collections/1f6ff9e4a17b83f4588c

Postman collection for the other requests (week 8): https://www.getpostman.com/collections/c383b8245107831e5654

### Explanations:
- Created a controller `InterventionsController.cs`
- Created a model `Intervention.cs`
- Modified the file `InformationContext.cs` to set the relations ant relate the entity with its respective table in the database.

=======================================================================================

Here's some information about the REST api for Rocket Elevators.

- The entities are the Models folder, with the attributes that are needed for our cas. For example, for the ELevator class : 

```C#
        public int id { get; }
        public string status { get; set; }
        public string serial_number { get; set; }
        public string model { get; set; }
        public string type_building { get; set; }
        public DateTime date_last_inspection { get; set; }

        public virtual int column_id { get; set; }
        public  Column Column { get; }
```

The relations between them are found in InformationContext. For example :

```C#
                modelBuilder.Entity<Lead>()
                .HasKey(x => x.id);

                modelBuilder.Entity<Building>()
                .HasMany(x => x.Batteries)
                .WithOne( y => y.Building)
                .HasForeignKey(z => z.building_id);
                
                
                modelBuilder.Entity<Battery>()
                .HasMany(x => x.Columns)
                .WithOne(y => y.Battery)
                .HasForeignKey(z => z.battery_id)
```
-Each requests to an endpoint are well-specified in the given controller. For example :

```C#
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> Getelevators()
        {
           

            DateTime current =  DateTime.Now.AddMonths(-12);

            var queryElevators = from elev in _context.elevators
                                 where elev.type_building == "Commercial" || elev.date_last_inspection < current
                                 select elev;

            var distinctElevators = (from elev in queryElevators
                                    select elev).Distinct();


            return await distinctElevators.ToListAsync();
        }
```
-All Requests for this API can be found here : https://www.getpostman.com/collections/f90923129965a1d47f0d

-Added to the initial requests, we added two more requests : QuoteThisYear, which return all Quotes made in the last year that are over 200,000$ (because monieeees!),
and ElevatorInspection, which return all elevators for which the last date of inspection date from over a year ago (Might need to be checked by an employee!).

-As mentionned, we implemented PATCH request instead of PUT or POST. The reason for that is that such request, which are relatively new, having been implemented around 2010, 
were added to the list of HTTP requests for the exact use that we want here : modify only selected attributes. See for ref :
https://rapidapi.com/blog/put-vs-patch/?utm_source=google&utm_medium=cpc&utm_campaign=DSA&gclid=Cj0KCQiA-rj9BRCAARIsANB_4AAAF1YmUPIsg3eX3Ae89ZV3AQ-v9xtTYW1lDO4FCCPYxNPTbOAd51QaAsNMEALw_wcB



