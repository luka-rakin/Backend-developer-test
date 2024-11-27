# Uputstvo za pokretanje i korišćenje aplikacije

## Povezivanje sa lokalnom bazom podataka

Za početak je potrebno konfigurisati vezu između aplikacije i lokalne baze podataka na vašem računaru.
To ćete uraditi tako što ćete dodati sledeće u appsettings.json fajl:

```
"ConnectionStrings": {
  "DefaultConnection": "YourConnectionString"
}
```

Umesto _YourConnectionString_ dodati pravi Connection string.

Nakon ovoga potrebno je pokrenuti migracije,

```
Add-Migration InitMigration -Project VehicleManager.Services -StartupProject VehicleManager
```

a zatim i update baze podataka.

```
Update-database
```

## Kako koristiti aplikaciju

Nakon što ste uspešno konfigurisali vezu sa lokalnom bazom podataka možete pokrenuti aplikaciju.

Kada se aplikacija pokrene prikazaće se stranica za upravljanje proizvođačima automobila. U navigacionom meniu stranice se može birati između straniec za upravljanje proizvođačima ili stranice za upravljanje modelima.

I na jednoj i na drugoj stranici postoji opcija za kreiranje novog proizvođača/novog modela, s tim što je prvo potrebno kreirati bar jednog proizvođača kako bi se mogao dodati novi model vozila.
Na obe stranice postoji tabelarni prikaz postojećih proizvođača/modela i opcije za sortiranje i upravljanje paginacijom, dok na stranici za modele postoji i opcija za filtriranje modela po proizvođačima.
