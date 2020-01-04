# .NET-CORE-ADO.NET-library

an wrapper for MySql ado.net
why do i use ado.net and not entity framework ?

i needed speed for my URL'S tracking system -> http://j.davidmunsa.com/demo
all of my tests showed that ado.net is faster by > 20% than entity framework...

but, if u ever used ADO.NET u know that the need to write the Sql queries yourself
can be annoying, for example stupid typo like 'INSERT INTO YABLE' instead of 'INSERT INTO TABLE'

each typo like that is time consuming and will cost a lot of man power in big projects

so i wrapped all the INSERT, UPDATE DELETE ETC... into one box.
