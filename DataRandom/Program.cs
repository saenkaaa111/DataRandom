using DataRandom;

//LogicForTransaction logicForTransaction = new LogicForTransaction();
//logicForTransaction.AddDbForTransaction();

//LogicForAccount logicForAccount = new LogicForAccount();
//logicForAccount.AddDbForAccount();

ServiceDb serviceDb = new ServiceDb();
serviceDb.AddServiceToLeadTable();

Console.WriteLine("Hello, World!");
Console.ReadKey();