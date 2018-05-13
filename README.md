# Parking_WebAPI
This application provides an opportunity to work with REST API and manage parking working.
On startup you can see list of requests
## Requests
### GET
- Show car by id
 `/api/parking/cars/{id}`
- Show free spots
`/api/parking/freespots`
- Show occupied spots
`/api/parking/occupiedspots`
- Show transaction history for the last minute
`/api/parking/last_minute_transactions`
- Show transaction history for the last minute for certain car
`/api/parking/last_minute_transactions/{id}`
- Show Transcations.log
`/api/parking/log`
- Show parking balances
`/api/parking/balance`
### POST
- Add car
`/api/parking/add_car/{type}&{balance:int}`
Where type = 1 Passenger | type = 2 Truck | type = 3 Bus | type = 4 Motorcycle
### PUT
- Recharge car balance
`/api/parking/recharge_balance/{id}&{balance:int}`
### DELETE
- Remove car
 `/api/parking/remove_car/{id}`
