# .NET-CORE-ADO.NET-library

an wrapper for MySql ado.net

why do i use ado.net and not entity framework ?

well, i needed speed for my URL'S tracking platform -> http://j.davidmunsa.com/demo

this platform needs to handle a LOT of INSERRT'S UPDATES'S (somthing like 1000 requests per second)

and its need to be FAST...

as u might know EF is build on top of ADO.NET so it can't be faster

all of my tests showed that ado.net is faster by > 20% than entity framework...

so i gave up on the convenience using EF to gain speed

but, if u ever used ADO.NET u know that the need to write all the Sql queries yourself

can be annoying, for example stupid typo like 'INSERT INTO YABLE' instead of 'INSERT INTO TABLE'

each typo like that is time consuming and will cost a lot of man power in big projects

so i wrapped all the INSERT, UPDATE DELETE ETC... into one box.

removed the annoying part and gained my speed. 
