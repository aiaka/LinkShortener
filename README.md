Link shortener test project 


Implemented:

POST - Save the URL to the database, return the short link

GET - redirect to the saved url using the short link



Design details:

Used MamoryCache for optimization getting tokens

BackgroundService to update Cache



ToDo list:

- add logging
- add unit tests
- add integration tests
- add QR code implementation


Future improvements:

The cache could be external for scalability

