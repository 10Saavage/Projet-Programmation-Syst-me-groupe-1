#------------------------------------------------------------
#        Script MySQL.
#------------------------------------------------------------


#------------------------------------------------------------
# Table: Matériels
#------------------------------------------------------------

CREATE TABLE Materiels(
        ID_Materiel       Int  Auto_increment  NOT NULL ,
        Nom_Materiel      Varchar (5) NOT NULL ,
        Quantite_Materiel Int NOT NULL
	,CONSTRAINT Materiels_PK PRIMARY KEY (ID_Materiel)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Recette
#------------------------------------------------------------

CREATE TABLE Recette(
        ID_Recette           Int  Auto_increment  NOT NULL ,
        Ingredients          Varchar (50) NOT NULL ,
        Quantite_ingredients Int NOT NULL ,
        Etapes               Varchar (50) NOT NULL ,
        TmpPrepa             Int NOT NULL ,
        TmpCuisson           Int NOT NULL ,
        TmpRepos             Int NOT NULL
	,CONSTRAINT Recette_PK PRIMARY KEY (ID_Recette)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Entrée
#------------------------------------------------------------

CREATE TABLE Entree(
        ID_Entree  Int  Auto_increment  NOT NULL ,
        ID_Recette Int NOT NULL ,
        Nom_Entree Varchar (50) NOT NULL
	,CONSTRAINT Entree_PK PRIMARY KEY (ID_Entree)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Plats
#------------------------------------------------------------

CREATE TABLE Plats(
        ID_Plats   Int  Auto_increment  NOT NULL ,
        Nom_Plats  Varchar (50) NOT NULL ,
        ID_Recette Int NOT NULL
	,CONSTRAINT Plats_PK PRIMARY KEY (ID_Plats)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Desserts
#------------------------------------------------------------

CREATE TABLE Desserts(
        ID_Desserts  Int  Auto_increment  NOT NULL ,
        Nom_Desserts Varchar (50) NOT NULL ,
        ID_Recette   Int NOT NULL
	,CONSTRAINT Desserts_PK PRIMARY KEY (ID_Desserts)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Menus
#------------------------------------------------------------

CREATE TABLE Menus(
        ID_menus       Int  Auto_increment  NOT NULL ,
        Intitule_menus Varchar (50) NOT NULL ,
        Prix_menus     Int NOT NULL ,
        ID_Plats       Int NOT NULL ,
        ID_Entree      Int NOT NULL ,
        ID_Desserts    Int NOT NULL
	,CONSTRAINT Menus_PK PRIMARY KEY (ID_menus)

	,CONSTRAINT Menus_Plats_FK FOREIGN KEY (ID_Plats) REFERENCES Plats(ID_Plats)
	,CONSTRAINT Menus_Entree0_FK FOREIGN KEY (ID_Entree) REFERENCES Entree(ID_Entree)
	,CONSTRAINT Menus_Desserts1_FK FOREIGN KEY (ID_Desserts) REFERENCES Desserts(ID_Desserts)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Produits_surgelés
#------------------------------------------------------------

CREATE TABLE Produits_surgeles(
        ID_Produits_surgeles       Int  Auto_increment  NOT NULL ,
        Nom_Produits_surgeles      Varchar (50) NOT NULL ,
        Quantite_Produits_surgeles Int NOT NULL ,
        Date_livraison_surgeles    Date NOT NULL ,
        Date_peremption_surgeles   Date NOT NULL
	,CONSTRAINT Produits_surgeles_PK PRIMARY KEY (ID_Produits_surgeles)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Produits_frais
#------------------------------------------------------------

CREATE TABLE Produits_frais(
        ID_Produits_frais       Int  Auto_increment  NOT NULL ,
        Nom_Produits_frais      Varchar (50) NOT NULL ,
        Quantite_Produits_frais Int NOT NULL ,
        Date_livraison_frais    Date NOT NULL ,
        Date_peremption_frais   Date NOT NULL ,
        Temp_min_frais          Int NOT NULL ,
        Temp_max_frais          Int NOT NULL
	,CONSTRAINT Produits_frais_PK PRIMARY KEY (ID_Produits_frais)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Produits_LC
#------------------------------------------------------------

CREATE TABLE Produits_LC(
        ID_Produits_lc       Int  Auto_increment  NOT NULL ,
        Nom_Produits_lc      Varchar (50) NOT NULL ,
        Quantite_Produits_lc Int NOT NULL ,
        Date_livraison_lc    Date NOT NULL
	,CONSTRAINT Produits_LC_PK PRIMARY KEY (ID_Produits_lc)
)ENGINE=InnoDB;


#------------------------------------------------------------
# Table: Contenu
#------------------------------------------------------------

CREATE TABLE Contenu(
        ID_Plats    Int NOT NULL ,
        ID_Desserts Int NOT NULL ,
        ID_Recette  Int NOT NULL ,
        ID_Entree   Int NOT NULL
	,CONSTRAINT Contenu_PK PRIMARY KEY (ID_Plats,ID_Desserts,ID_Recette,ID_Entree)

	,CONSTRAINT Contenu_Plats_FK FOREIGN KEY (ID_Plats) REFERENCES Plats(ID_Plats)
	,CONSTRAINT Contenu_Desserts0_FK FOREIGN KEY (ID_Desserts) REFERENCES Desserts(ID_Desserts)
	,CONSTRAINT Contenu_Recette1_FK FOREIGN KEY (ID_Recette) REFERENCES Recette(ID_Recette)
	,CONSTRAINT Contenu_Entree2_FK FOREIGN KEY (ID_Entree) REFERENCES Entree(ID_Entree)
)ENGINE=InnoDB;

