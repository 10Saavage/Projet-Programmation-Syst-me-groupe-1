/*------------------------------------------------------------
*        Script SQLSERVER 
------------------------------------------------------------*/


/*------------------------------------------------------------
-- Table: Matériels
------------------------------------------------------------*/
CREATE TABLE Materiels(
	ID_Materiel         INT IDENTITY (1,1) NOT NULL ,
	Nom_Materiel        VARCHAR (5) NOT NULL ,
	Quantite_Materiel   INT  NOT NULL  ,
	CONSTRAINT Materiels_PK PRIMARY KEY (ID_Materiel)
);


/*------------------------------------------------------------
-- Table: Recette
------------------------------------------------------------*/
CREATE TABLE Recette(
	ID_Recette             INT IDENTITY (1,1) NOT NULL ,
	Ingredients            VARCHAR (50) NOT NULL ,
	Quantite_ingredients   INT  NOT NULL ,
	Etapes                 VARCHAR (50) NOT NULL ,
	TmpPrepa               INT  NOT NULL ,
	TmpCuisson             INT  NOT NULL ,
	TmpRepos               INT  NOT NULL  ,
	CONSTRAINT Recette_PK PRIMARY KEY (ID_Recette)
);


/*------------------------------------------------------------
-- Table: Entrée
------------------------------------------------------------*/
CREATE TABLE Entree(
	ID_Entree    INT IDENTITY (1,1) NOT NULL ,
	ID_Recette   INT  NOT NULL ,
	Nom_Entree   VARCHAR (50) NOT NULL  ,
	CONSTRAINT Entree_PK PRIMARY KEY (ID_Entree)
);


/*------------------------------------------------------------
-- Table: Plats
------------------------------------------------------------*/
CREATE TABLE Plats(
	Id           INT IDENTITY (1,1) NOT NULL ,
	Nom_Plats    VARCHAR (50) NOT NULL ,
	ID_Recette   INT  NOT NULL  ,
	CONSTRAINT Plats_PK PRIMARY KEY (Id)
);


/*------------------------------------------------------------
-- Table: Desserts
------------------------------------------------------------*/
CREATE TABLE Desserts(
	ID_Desserts    INT IDENTITY (1,1) NOT NULL ,
	Nom_Desserts   VARCHAR (50) NOT NULL ,
	ID_Recette     INT  NOT NULL  ,
	CONSTRAINT Desserts_PK PRIMARY KEY (ID_Desserts)
);


/*------------------------------------------------------------
-- Table: Menus
------------------------------------------------------------*/
CREATE TABLE Menus(
	ID_menus         INT IDENTITY (1,1) NOT NULL ,
	Intitule_menus   VARCHAR (50) NOT NULL ,
	Prix_menus       INT  NOT NULL ,
	Id               INT  NOT NULL ,
	ID_Entree        INT  NOT NULL ,
	ID_Desserts      INT  NOT NULL  ,
	CONSTRAINT Menus_PK PRIMARY KEY (ID_menus)

	,CONSTRAINT Menus_Plats_FK FOREIGN KEY (Id) REFERENCES Plats(Id)
	,CONSTRAINT Menus_Entree0_FK FOREIGN KEY (ID_Entree) REFERENCES Entree(ID_Entree)
	,CONSTRAINT Menus_Desserts1_FK FOREIGN KEY (ID_Desserts) REFERENCES Desserts(ID_Desserts)
);


/*------------------------------------------------------------
-- Table: Produits_surgelés
------------------------------------------------------------*/
CREATE TABLE Produits_surgeles(
	ID_Produits_surgeles         INT IDENTITY (1,1) NOT NULL ,
	Nom_Produits_surgeles        VARCHAR (50) NOT NULL ,
	Quantite_Produits_surgeles   INT  NOT NULL ,
	Date_livraison_surgeles      DATETIME NOT NULL ,
	Date_peremption_surgeles     DATETIME NOT NULL  ,
	CONSTRAINT Produits_surgeles_PK PRIMARY KEY (ID_Produits_surgeles)
);


/*------------------------------------------------------------
-- Table: Produits_frais
------------------------------------------------------------*/
CREATE TABLE Produits_frais(
	ID_Produits_frais         INT IDENTITY (1,1) NOT NULL ,
	Nom_Produits_frais        VARCHAR (50) NOT NULL ,
	Quantite_Produits_frais   INT  NOT NULL ,
	Date_livraison_frais      DATETIME NOT NULL ,
	Date_peremption_frais     DATETIME NOT NULL ,
	Temp_min_frais            INT  NOT NULL ,
	Temp_max_frais            INT  NOT NULL  ,
	CONSTRAINT Produits_frais_PK PRIMARY KEY (ID_Produits_frais)
);


/*------------------------------------------------------------
-- Table: Produits_LC
------------------------------------------------------------*/
CREATE TABLE Produits_LC(
	ID_Produits_lc         INT IDENTITY (1,1) NOT NULL ,
	Nom_Produits_lc        VARCHAR (50) NOT NULL ,
	Quantite_Produits_lc   INT  NOT NULL ,
	Date_livraison_lc      DATETIME NOT NULL  ,
	CONSTRAINT Produits_LC_PK PRIMARY KEY (ID_Produits_lc)
);


/*------------------------------------------------------------
-- Table: Contenu
------------------------------------------------------------*/
CREATE TABLE Contenu(
	Id            INT  NOT NULL ,
	ID_Desserts   INT  NOT NULL ,
	ID_Recette    INT  NOT NULL ,
	ID_Entree     INT  NOT NULL  ,
	CONSTRAINT Contenu_PK PRIMARY KEY (Id,ID_Desserts,ID_Recette,ID_Entree)

	,CONSTRAINT Contenu_Plats_FK FOREIGN KEY (Id) REFERENCES Plats(Id)
	,CONSTRAINT Contenu_Desserts0_FK FOREIGN KEY (ID_Desserts) REFERENCES Desserts(ID_Desserts)
	,CONSTRAINT Contenu_Recette1_FK FOREIGN KEY (ID_Recette) REFERENCES Recette(ID_Recette)
	,CONSTRAINT Contenu_Entree2_FK FOREIGN KEY (ID_Entree) REFERENCES Entree(ID_Entree)
);



