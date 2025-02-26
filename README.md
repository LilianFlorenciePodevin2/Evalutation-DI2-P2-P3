Prérequis
SQL Server (assurez-vous qu'il est installé et configuré)
.NET SDK
Node.js et npm
Back-End (PasswordManager.API)
Configuration de la base de données

Allez dans le dossier PasswordManager.API.
Ouvrez le fichier appsettings.json et modifiez la configuration de la base de données selon votre environnement SQL Server.
Migration et mise à jour de la base de données

Dans le dossier PasswordManager.API, exécutez la commande suivante pour créer la migration initiale :
bash
Copier
dotnet ef migrations add InitialCreate
Puis, mettez à jour la base de données avec la migration :
bash
Copier
dotnet ef database update
Lancement de l'API

Toujours dans le dossier PasswordManager.API, lancez l'application :
bash
Copier
dotnet run
L'API devrait être accessible (par exemple, sur https://localhost:7126).
Front-End (password-manager-front)
Installation des dépendances

Allez dans le dossier password-manager-front.
Exécutez la commande suivante pour installer les dépendances npm :
bash
Copier
npm install
Lancement de l'application Angular

Dans le dossier password-manager-front, lancez l'application Angular avec la commande :
bash
Copier
ng serve
L'application sera disponible via http://localhost:4200.
