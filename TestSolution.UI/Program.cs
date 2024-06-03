using TestSolution.DataPersistence;

SessionFactory.Init("Host=localhost;Username=postgres;Password=admin;Database=testdb");

var session = SessionFactory.OpenSession();