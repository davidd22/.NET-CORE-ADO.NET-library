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

<h1>CODE EXAMPLES</h1>

<h3>INSERT</h3>

    CMySqlInsert cMySqlInsert = new CMySqlInsert([DB_CONNECTION_STRING]
                                                        , [TABLE_NAME]
                                                        , true -> true if u need the LastInsertedId
                                                        , new Dictionary<string, object>()
                                                        {
                                                             { [TABLE_COLUMN_NAME_1], [COLUMN1_VALUE1]}
                                                            ,{ [TABLE_COLUMN_NAME_2], [COLUMN1_VALUE2]}
                                                            ,{ [TABLE_COLUMN_NAME_3], [COLUMN1_VALUE3]}
                                                        }

                                                        );

            CDdlReturnValue result = await cMySqlInsert.ExecuteAsync();
            
            
<h3>UPDATE</h3>

     List<CCondition> updateWhereCondition
                = new List<CCondition>() { new CCondition(COperatorCondition.AND, [COLUMN1_VALUE], 1, COperatorCondition.EQUAL)
                                          ,new CCondition(COperatorCondition.AND, [COLUMN2_VALUE], 2, COperatorCondition.EQUAL) };

            Dictionary<string, object> updateValues = new Dictionary<string, object>() {
                { [TABLE_COLUMN_NAME_1],'HELLO'}
              , { [TABLE_COLUMN_NAME_2],'WORLD'}
            };

            CMySqlUpdate cMySqlUpdate = new CMySqlUpdate([DB_CONNECTION_STRING]
                                                        , [TABLE_NAME]
                                                        , updateWhereCondition
                                                        , updateValues
                                                        , InvokeActionOnDbError
                                                        );

            CDdlReturnValue result = await cMySqlUpdate.ExecuteAsync();
