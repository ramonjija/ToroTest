using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Populate_Shares : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            BEGIN TRAN
                IF NOT EXISTS(SELECT * FROM [Shares])
                    BEGIN
	                INSERT INTO SHARES VALUES 
	                     ('PETR4', 28.44)
	                    ,('MGLU3', 25.91)
	                    ,('VVAR3', 25.91)
	                    ,('SANB11', 40.77)
	                    ,('TORO4', 115.98)
                    END
            COMMIT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM SHARES WHERE Symbol in('PETR4', 'MGLU3', 'VVAR3', 'SANB11', 'TORO4')");
        }
    }
}
