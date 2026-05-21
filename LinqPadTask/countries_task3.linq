<Query Kind="Expression">
  <Connection>
    <ID>f461ac8f-a870-4d32-a4ad-dc9797152921</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <SqlSecurity>true</SqlSecurity>
    <AttachFileName>&lt;UserProfile&gt;\Загрузки\countries.db</AttachFileName>
    <DriverData>
      <EncryptSqlTraffic>True</EncryptSqlTraffic>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.Sqlite</EFProvider>
    </DriverData>
  </Connection>
</Query>

Table_countries.Where(c => c.Name.ToLower().Contains("а") && c.Name.ToLower().Contains("u")),
Table_countries.Where(c => c.Name.ToLower().StartsWith("a")),
Table_countries.Where(c => c.Area >= 1000000 && c.Area <= 5000000).Select(c => c.Name),
Table_countries.Where(c => c.Population > 100000000).Select(c => c.Name),
