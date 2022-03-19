using DataRandom;

//LogicForTransaction logicForTransaction = new LogicForTransaction();
//logicForTransaction.AddDbForTransaction();

//LogicForAccount logicForAccount = new LogicForAccount();
//logicForAccount.AddDbForAccount();

//LogicForServiceDb logicfroServiceDb = new LogicForServiceDb();
//logicfroServiceDb.AddServiceToLeadTable();

LogicForServiceTransaction logicForServiceTransaction = new LogicForServiceTransaction();
logicForServiceTransaction.AddDbForTransaction();

Console.WriteLine("Hello, World!");
Console.ReadKey();