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

Table_countries.OrderByDescending(c => c.Area).Take(5),
Table_countries.OrderByDescending(c => c.Population).Take(5),
Table_countries.OrderByDescending(c => c.Area).FirstOrDefault(),
Table_countries.Where(c => c.Continent=="Африка").OrderBy(c => c.Area).FirstOrDefault(),
Table_countries.Where(c => c.Continent == "Азия").Average(c => c.Area),