using System;
using System.IO;

namespace ExtloadOdms
{
    class Program
    {
        /* string arg_zero = "NewClass";
           string arg_one = "Attribute";
           string arg_two = "subClassOf";
           string arg_three = "cims_belongsToCategory";
           string arg_four = "rdfs comment";
           strings rdf1-5 = GUID;
           */
           // Here is a change in the module

        public static string filename = @"C:\Users\tsimm\Documents\Code Projects\AEP\OutputFolder\ODMSSQLOutput.txt";
        public static StreamWriter file = null;
        public static string[ ] SQLlines;
        public static string[] separator = new string[] { ";" };

        static void Main(string[] args)
        {
            String C3= "USE AEP_Ohio" +
            "CREATE TABLE AEP_ACLineSeriesSection" +
            "(OID uniqueidentifier, sectionNumber int NOT NULL, FK_ACLS_OID uniqueidentifier," +
            "CONSTRAINT PK_ACLSS PRIMARY KEY NONCLUSTERED(OID)," +
            "CONSTRAINT FK_ACLSS_ACLS FOREIGN KEY(FK_ACLS_OID) REFERENCES dbo.ACLineSegment(OID)" +
            "ON DELETE CASCADE" +
            "ON UPDATE CASCADE);"  +
            "GO ";

            //NOTE:  NEED TO MAKE THE COLUMN NAME FLEXIBLE AS WELL (SOCCID)
            string Composit1 =
            " CREATE TABLE {0}" +
            "(OID uniqueidentifier NOT NULL CONSTRAINT PK_{0} PRIMARY KEY NONCLUSTERED,{1} float,SOCCID uniqueidentifier);" +
            " ALTER TABLE {0} ADD CONSTRAINT FK_{2} FOREIGN KEY(OID) REFERENCES {2}(OID) ON DELETE CASCADE;" +
            " INSERT INTO Resources(OID, URI, ResourceType, TableName) \n VALUES(‘rdf1’, 'aep:{0}', 1, '{0}');" +
            " INSERT INTO Class(ClassID, rdf_ID, rdfs_label, rdfs_comment, cims_belongsToCategory, rdfs_subClassOf, ClassType) \n VALUES(‘rdf1', '{0}', '{0}', '{4}', '{3}', '{2}', 'Class');" +
            " INSERT INTO Resources (OID, URI, ResourceType, TableName, ColumnName) \n VALUES (‘rdf2', 'aep:{0}.{1}', 3, '{0}', '{1}');" +
            " INSERT INTO Property (PropertyID, ClassID, rdf_ID, rdfs_label, rdfs_comment, rdfs_domain, rdfs_range, cims_multiplicity, cims_dataType) \n VALUES (‘rdf2', ‘rdf1', '{0}.{1}', '{1}', '{4}', '{0}', NULL, 'M:0..1', 'Status');" +
            " UPDATE Class SET SubClassOfID = ‘rdf6’ WHERE ClassID = ‘rdf1';" +
            " UPDATE Property SET InverseRoleNameID = ‘rdf4’ WHERE PropertyID = ‘rdf3';" +
            " UPDATE Property SET InverseRoleNameID = ‘rdf3' WHERE PropertyID = ‘rdf4’";

            // Here is another change in the module

            string Composit2 = 
                " ALTER TABLE {0} ADD FOREIGN KEY(SOCCID) REFERENCES {1}(OID);" +
                " INSERT INTO Resources(OID, URI, ResourceType, TableName, ColumnName) \n VALUES(‘rdf3', 'aep:{0}.{1}', 3, '{1}', NULL);" +
                " INSERT INTO Resources (OID, URI, ResourceType, TableName, ColumnName) \n VALUES (‘rdf4’, 'aep:{1}.{0}', 3, '{0}', 'SOCCID');" +
                " INSERT INTO Property(PropertyID, ClassID, rdf_ID, rdfs_label, rdfs_comment, rdfs_domain, rdfs_range, cims_multiplicity, cims_inverseRoleName) " +
                " \n VALUES(‘rdf3', ‘rdf1', '{0}.{1}', '{1}', '{1} of {0}', '{0}', '{1}', 'M:0..1', '{1}.{0}');" +
                " INSERT INTO Property(PropertyID, ClassID, rdf_ID, rdfs_label, rdfs_comment, rdfs_domain, rdfs_range, cims_multiplicity, cims_inverseRoleName) " +
                " \n VALUES(‘rdf4’, ‘rdf5’, '{1}.{0}', '{0}', '{0} contained in {1}', '{1}', '{0}', 'M:0..1', '{0}.{1}')" ;


        Console.WriteLine(Composit1,"AEP_ACLineSegment", "baseKVPU","ACLineSegment", "Package_AEP", "AEP_ACLineSegment"  );
        Console.ReadLine();
        Console.WriteLine("\n====================== Separator\n");

            SQLlines = Composit1.Split(separator, StringSplitOptions.None);
            foreach (string line in SQLlines)
            {
                Console.WriteLine(line + ";", "AEP_ACLineSegment", "baseKVPU", "ACLineSegment", "Package_AEP", "AEP_ACLineSegment");
            }
            Console.ReadLine();
            Console.WriteLine("\n====================== Separator\n");


            SQLlines = Composit2.Split(separator, StringSplitOptions.None);
            foreach (string line in SQLlines)
            {
                Console.WriteLine(line + ";", "AEP_ACLineSegment", "AEP_ACLineSeriesSection");
            }
            Console.ReadLine();
   

            file =  new System.IO.StreamWriter(filename) ;

            file.WriteLine(Composit1, "AEP_ACLineSegment", "baseKVPU", "ACLineSegment", "Package_AEP", "AEP_ACLineSegment");
            file.WriteLine();

            file.Close();


            file = new System.IO.StreamWriter(filename, true);  // Append to existing file

            file.WriteLine(Composit2, "AEP_ACLineSegment", "AEP_ACLineSeriesSection");
            file.WriteLine();
            file.Close();


            SQLlines = Composit1.Split(separator, StringSplitOptions.None);
            file = new System.IO.StreamWriter(filename, true);  // Append to existing file

            foreach (string line in SQLlines)
                {
                file.WriteLine(line + ";", "AEP_ACLineSegment", "baseKVPU", "ACLineSegment", "Package_AEP", "AEP_ACLineSegment");
            }
            file.WriteLine();
            file.Close();

            SQLlines = Composit2.Split(separator, StringSplitOptions.None);
            file = new System.IO.StreamWriter(filename, true);  // Append to existing file

            foreach (string line in SQLlines)
            {
                file.WriteLine(line + ";", "AEP_ACLineSegment", "AEP_ACLineSeriesSection");
            }

            file.Close();

        }
    }
}
