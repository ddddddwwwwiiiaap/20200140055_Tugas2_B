using System;
using System.Data.SqlClient;

namespace Tgs2_20200140055_B
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = new Database();
            while (true)
            {
                try
                {
                    Console.WriteLine("\nMenu");
                    Console.WriteLine("1. Untuk Membuat Tabel");
                    Console.WriteLine("2. Untuk Memasukkan Data");
                    Console.WriteLine("3. Keluar");
                    Console.Write("\nPilih (1-3): ");
                    char ch = Convert.ToChar(Console.ReadLine());
                    switch (ch)
                    {
                        case '1':
                            {
                                db.Create_Tabel();
                            }
                            break;
                        case '2':
                            {
                                db.InsertData();
                            }
                            break;
                        case '3':
                            return;
                        default:
                            {
                                Console.WriteLine("Berhasil");
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Selesai");
                    break;
                }
            }
        }

        class Database
        {
            public string connectionStr = "data source=DWI-APRILYA-A-P;" +
                "database=ExerciseApotek;User ID=sa;Password=HAdpt023467";
            public void Connection()
            {
                using (SqlConnection con = new SqlConnection(connectionStr))
                {
                    con.Open();
                    Console.WriteLine("Connection Succesfully");
                }
            }
            public void Create_Tabel()
            {
                SqlConnection con = null;
                try
                {
                    con = new SqlConnection(connectionStr);
                    con.Open();

                    SqlCommand cm = new SqlCommand(
                        //Membuat Tabel Apoteker
                        "create table Apoteker " +
                        "(Id_Apoteker char(15) Check (Id_Apoteker in ('Apoteker1', 'Apoteker2', 'Apoteker3', 'Apoteker4', 'Apoteker5')) not null primary key, " +
                        "Nama_Apoteker varchar(50) not null, " +
                        "Jk_Apoteker char(1) Check (Jk_Apoteker in ('P', 'L')) not null, " +
                        "Alamat_Apoteker varchar(50) not null," +
                        "No_TeleponApoteker char(15) not null)"
                        //Membuat Tabel Supplier
                        + "create table Supplier " +
                        "(Id_Supplier char(15) Check (Id_Supplier in ('Supplier1', 'Supplier2', 'Supplier3', 'Supplier4', 'Supplier5')) not null primary key, " +
                        "Nama_Supplier varchar(50) not null, " +
                        "Jk_Supplier char(1) Check (Jk_Supplier in ('P', 'L')) not null, " +
                        "Alamat_Supplier varchar(50) not null," +
                        "No_TeleponSupplier char(15) not null)"
                        //Membuat Tabel Karyawan
                        + "create table Karyawan " +
                        "(Id_Karyawan char(15) Check (Id_Karyawan in ('Karyawan1', 'Karyawan2', 'Karyawan3', 'Karyawan4', 'Karyawan5')) not null primary key, " +
                        "Nama_Karyawan varchar(50) not null, " +
                        "Jk_Karyawan char(1) Check (Jk_Karyawan in ('P', 'L')) not null, " +
                        "Alamat_Karyawan varchar(50) not null, " +
                        "Status_Karyawan char(15) Check (Status_Karyawan in ('Aktif', 'Tidak Aktif')) not null, " +
                        "No_TeleponKaryawan char(15) not null)"
                        //Membuat Tabel Konsumen
                        + "create table Konsumen " +
                        "(Id_Konsumen char(15) not null primary key, " +
                        "Nama_Konsumen varchar(50) not null, " +
                        "No_TeleponKonsumen char(15) not null, " +
                        "Email_Konsumen varchar(50) not null, " +
                        "Alamat_Konsumen varchar(50) not null)"
                        //Membuat Tabel Obat Apotek
                        + "create table Obat_Apotek " +
                        "(Id_ObatApotek char(15) not null primary key, " +
                        "Nama_Obat varchar(50) not null, " +
                        "Stock_Obat int not null, " +
                        "Harga_Obat money not null, " +
                        "Jenis_Obat varchar(50) not null, " +
                        "Id_Supplier char(15) FOREIGN KEY REFERENCES Supplier(Id_Supplier) not null, " +
                        "Id_Apoteker char(15) FOREIGN KEY REFERENCES Apoteker(Id_Apoteker) not null, " +
                        "Id_Karyawan char(15) FOREIGN KEY REFERENCES Karyawan(Id_Karyawan) not null, " +
                        "Id_Konsumen char(15) FOREIGN KEY REFERENCES Konsumen(Id_Konsumen) not null)"
                        //Membuat Tabel Nota Penjualan
                        + "create table Nota_Penjualan " +
                        "(Id_NotaPenjualan char(15) not null primary key, " +
                        "Tanggal date not null, " +
                        "Waktu time not null, " +
                        "Qty_Total int not null, " +
                        "Total_Harga money not null, " +
                        "Total_Bayar money not null, " +
                        "Pajak money not null, " +
                        "Id_Konsumen char(15) FOREIGN KEY REFERENCES Konsumen(Id_Konsumen) not null, " +
                        "Id_Karyawan char(15) FOREIGN KEY REFERENCES Karyawan(Id_Karyawan) not null)"
                        //Membuat Tabel Nota Supplier
                        + "create table Nota_Supplier " +
                        "(Id_NotaSupplier char(15) not null primary key, " +
                        "Tanggal date not null, " +
                        "Waktu time not null, " +
                        "Jumlah int not null, " +
                        "Total_Harga money not null, " +
                        "Total_Bayar money not null, " +
                        "Pajak money not null, " +
                        "Id_Supplier char(15) FOREIGN KEY REFERENCES Supplier(Id_Supplier) not null, " +
                        "Id_ObatApotek char(15) FOREIGN KEY REFERENCES Obat_Apotek(Id_ObatApotek) not null)", con);
                    cm.ExecuteNonQuery();

                    Console.WriteLine("Tabel sukses dibuat!");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops, sepertinya ada yang salah. " + e);
                    Console.ReadKey();
                }
                finally
                {
                    con.Close();
                }
            }

            public void InsertData()
            {
                SqlConnection con = null;
                try
                {
                    con = new SqlConnection(connectionStr);
                    con.Open();

                    SqlCommand cm = new SqlCommand(
                        //Memasukkan data Tabel Apoteker
                        "insert into Apoteker " +
                        "(Id_Apoteker, " +
                        "Nama_Apoteker, " +
                        "Jk_Apoteker, " +
                        "Alamat_Apoteker, " +
                        "No_TeleponApoteker) values " +
                        "('Apoteker1','Eka Putri','P','Papua','08511111111')" + "insert into Apoteker " +
                        "(Id_Apoteker, " +
                        "Nama_Apoteker, " +
                        "Jk_Apoteker, " +
                        "Alamat_Apoteker, " +
                        "No_TeleponApoteker) values " +
                        "('Apoteker2','Dwi Putra','L','Yogyakarta','08511111112')"
                        + "insert into Apoteker " +
                        "(Id_Apoteker, " +
                        "Nama_Apoteker, " +
                        "Jk_Apoteker, " +
                        "Alamat_Apoteker, " +
                        "No_TeleponApoteker) values " +
                        "('Apoteker3','Tri Putri','P','Bandung','08511111113')"
                        + "insert into Apoteker " +
                        "(Id_Apoteker, " +
                        "Nama_Apoteker, " +
                        "Jk_Apoteker, " +
                        "Alamat_Apoteker, " +
                        "No_TeleponApoteker) values " +
                        "('Apoteker4','Catur Putra','L','Bali','08511111114')"
                        + "insert into Apoteker " +
                        "(Id_Apoteker, " +
                        "Nama_Apoteker, " +
                        "Jk_Apoteker, " +
                        "Alamat_Apoteker, " +
                        "No_TeleponApoteker) values " +
                        "('Apoteker5','Panca Putri','L','Lombok','08511111115')"

                        //Memasukkan data Tabel Supplier
                        + "insert into Supplier " +
                        "(Id_Supplier, " +
                        "Nama_Supplier, " +
                        "Jk_Supplier, " +
                        "Alamat_Supplier, " +
                        "No_TeleponSupplier) values " +
                        "('Supplier1','Sat','L','Tangsel','08511111116')"
                        + "insert into Supplier " +
                        "(Id_Supplier, " +
                        "Nama_Supplier, " +
                        "Jk_Supplier, " +
                        "Alamat_Supplier, " +
                        "No_TeleponSupplier) values " +
                        "('Supplier2','Sapta','L','Tanggerang','08511111117')"
                        + "insert into Supplier " +
                        "(Id_Supplier, " +
                        "Nama_Supplier, " +
                        "Jk_Supplier, " +
                        "Alamat_Supplier, " +
                        "No_TeleponSupplier) values " +
                        "('Supplier3','Astha','p','Bali','08511111118')"
                        + "insert into Supplier " +
                        "(Id_Supplier, " +
                        "Nama_Supplier, " +
                        "Jk_Supplier, " +
                        "Alamat_Supplier, " +
                        "No_TeleponSupplier) values " +
                        "('Supplier4','Nawa','P','Aceh','08511111119')"
                        + "insert into Supplier " +
                        "(Id_Supplier, " +
                        "Nama_Supplier, " +
                        "Jk_Supplier, " +
                        "Alamat_Supplier, " +
                        "No_TeleponSupplier) values " +
                        "('Supplier5','Dasa','P','NTT','08511111110')"

                        //Memasukkan data Tabel Karyawan
                        + "insert into Karyawan " +
                        "(Id_Karyawan, " +
                        "Nama_Karyawan, " +
                        "Jk_Karyawan, " +
                        "Alamat_Karyawan, " +
                        "Status_Karyawan, " +
                        "No_TeleponKaryawan) values " +
                        "('Karyawan1','Sata','P','Bali','Aktif','085211111110')"
                        + "insert into Karyawan " +
                        "(Id_Karyawan, " +
                        "Nama_Karyawan, " +
                        "Jk_Karyawan, " +
                        "Alamat_Karyawan, " +
                        "Status_Karyawan, " +
                        "No_TeleponKaryawan) values " +
                        "('Karyawan2','Sahasra','P','Sulawesi','Tidak Aktif','085211111111')"
                        + "insert into Karyawan " +
                        "(Id_Karyawan, " +
                        "Nama_Karyawan, " +
                        "Jk_Karyawan, " +
                        "Alamat_Karyawan, " +
                        "Status_Karyawan, " +
                        "No_TeleponKaryawan) values " +
                        "('Karyawan3','Ayuta','P','Solo','Aktif','085211111112')"
                        + "insert into Karyawan " +
                        "(Id_Karyawan, " +
                        "Nama_Karyawan, " +
                        "Jk_Karyawan, " +
                        "Alamat_Karyawan, " +
                        "Status_Karyawan, " +
                        "No_TeleponKaryawan) values " +
                        "('Karyawan4','Laksa','L','GunungKidul','Aktif','085211111113')"
                        + "insert into Karyawan " +
                        "(Id_Karyawan, " +
                        "Nama_Karyawan, " +
                        "Jk_Karyawan, " +
                        "Alamat_Karyawan, " +
                        "Status_Karyawan, " +
                        "No_TeleponKaryawan) values " +
                        "('Karyawan5','Prayuta','L','Semarang','Tidak Aktif','085211111114')"

                        //Memasukkan data Tabel Konsumen
                        + "insert into Konsumen " +
                        "(Id_Konsumen, " +
                        "Nama_Konsumen, " +
                        "No_TeleponKonsumen, " +
                        "Email_Konsumen, " +
                        "Alamat_Konsumen) values " +
                        "('001','Koti','085311111115','Koti@gmail.com','Kaliurang')"
                        + "insert into Konsumen " +
                        "(Id_Konsumen, " +
                        "Nama_Konsumen, " +
                        "No_TeleponKonsumen, " +
                        "Email_Konsumen, " +
                        "Alamat_Konsumen) values " +
                        "('002','Vyarbuda','085311111116','vyarbuda@gmail.com','Solo Baru')"
                        + "insert into Konsumen " +
                        "(Id_Konsumen, " +
                        "Nama_Konsumen, " +
                        "No_TeleponKonsumen, " +
                        "Email_Konsumen, " +
                        "Alamat_Konsumen) values " +
                        "('003','Padma','085311111117','padma@gmail.com','Kota Baru')"
                        + "insert into Konsumen " +
                        "(Id_Konsumen, " +
                        "Nama_Konsumen, " +
                        "No_TeleponKonsumen, " +
                        "Email_Konsumen, " +
                        "Alamat_Konsumen) values " +
                        "('004','Hansa','085311111118','Hansa@gmail.com','Sleman')"
                        + "insert into Konsumen " +
                        "(Id_Konsumen, " +
                        "Nama_Konsumen, " +
                        "No_TeleponKonsumen, " +
                        "Email_Konsumen, " +
                        "Alamat_Konsumen) values " +
                        "('005','Hara','085311111119','Hara@gmail.com','Bantul')"

                        //Memasukkan data Tabel Obat Apotek
                        + "insert into Obat_Apotek " +
                        "(Id_ObatApotek, " +
                        "Id_Supplier, " +
                        "Id_Apoteker, " +
                        "Id_Karyawan, " +
                        "Id_Konsumen, " +
                        "Nama_Obat, " +
                        "Stock_Obat, " +
                        "Harga_Obat, " +
                        "Jenis_Obat) values " +
                        "('001','Supplier1','Apoteker1','Karyawan1','001','Acarbose','10','5000,00','Obat Bebas (OB)')"
                        + "insert into Obat_Apotek " +
                        "(Id_ObatApotek, " +
                        "Id_Supplier, " +
                        "Id_Apoteker, " +
                        "Id_Karyawan, " +
                        "Id_Konsumen, " +
                        "Nama_Obat, " +
                        "Stock_Obat, " +
                        "Harga_Obat, " +
                        "Jenis_Obat) values " +
                        "('002','Supplier2','Apoteker2','Karyawan1','002','Albendazole','20','10000,00','Obat Bebas Terbatas (OBT)')"
                        + "insert into Obat_Apotek " +
                        "(Id_ObatApotek, " +
                        "Id_Supplier, " +
                        "Id_Apoteker, " +
                        "Id_Karyawan, " +
                        "Id_Konsumen, " +
                        "Nama_Obat, " +
                        "Stock_Obat, " +
                        "Harga_Obat, " +
                        "Jenis_Obat) values " +
                        "('003','Supplier3','Apoteker3','Karyawan1','003','Apixaban','30','20000,00','Obat Bebas (OB)')"
                        + "insert into Obat_Apotek " +
                        "(Id_ObatApotek, " +
                        "Id_Supplier, " +
                        "Id_Apoteker, " +
                        "Id_Karyawan, " +
                        "Id_Konsumen, " +
                        "Nama_Obat, " +
                        "Stock_Obat, " +
                        "Harga_Obat, " +
                        "Jenis_Obat) values " +
                        "('004','Supplier4','Apoteker4','Karyawan1','004','Alfentanil','40','30000,00','Obat Bebas (OB)')"
                        + "insert into Obat_Apotek " +
                        "(Id_ObatApotek, " +
                        "Id_Supplier, " +
                        "Id_Apoteker, " +
                        "Id_Karyawan, " +
                        "Id_Konsumen, " +
                        "Nama_Obat, " +
                        "Stock_Obat, " +
                        "Harga_Obat, " +
                        "Jenis_Obat) values " +
                        "('005','Supplier5','Apoteker5','Karyawan1','005','Acyclovir Topikal','50','40000,00','Obat Bebas Terbatas (OBT)')"

                        //Memasukkan data Tabel Nota Penjualan
                        + "insert into Nota_Penjualan " +
                        "(Id_NotaPenjualan, " +
                        "Id_Konsumen, " +
                        "Id_Karyawan, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Qty_Total, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('001','001','Karyawan1','01-04-2022','1:40:53','10','10000,00', '11000,00', '1000,00')"
                        + "insert into Nota_Penjualan " +
                        "(Id_NotaPenjualan, " +
                        "Id_Konsumen, " +
                        "Id_Karyawan, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Qty_Total, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('002','002','Karyawan2','02-04-2022','2:40:53','10','20000,00', '21000,00', '1000,00')"
                        + "insert into Nota_Penjualan " +
                        "(Id_NotaPenjualan, " +
                        "Id_Konsumen, " +
                        "Id_Karyawan, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Qty_Total, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('003','003','Karyawan3','03-04-2022','3:40:53','10','30000,00', '31000,00', '1000,00')"
                        + "insert into Nota_Penjualan " +
                        "(Id_NotaPenjualan, " +
                        "Id_Konsumen, " +
                        "Id_Karyawan, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Qty_Total, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('004','004','Karyawan4','04-04-2022','4:40:53','10','400000,00', '41000,00', '1000,00')"
                        + "insert into Nota_Penjualan " +
                        "(Id_NotaPenjualan, " +
                        "Id_Konsumen, " +
                        "Id_Karyawan, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Qty_Total, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('005','005','Karyawan5','05-04-2022','5:40:53','10','50000,00', '51000,00', '1000,00')"

                        //Memasukkan data Tabel Nota Supplier
                        + "insert into Nota_Supplier " +
                        "(Id_NotaSupplier, " +
                        "Id_Supplier, " +
                        "Id_ObatApotek, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Jumlah, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('001','Supplier1','001','01-04-2022','11:40:53','10','1000000,00', '1100000,00', '100000,00')"
                        + "insert into Nota_Supplier " +
                        "(Id_NotaSupplier, " +
                        "Id_Supplier, " +
                        "Id_ObatApotek, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Jumlah, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('002','Supplier2','002','02-04-2022','12:40:53','10','2000000,00', '2100000,00', '1000,00')"
                        + "insert into Nota_Supplier " +
                        "(Id_NotaSupplier, " +
                        "Id_Supplier, " +
                        "Id_ObatApotek, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Jumlah, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('003','Supplier3','003','03-04-2022','13:40:53','10','3000000,00', '3100000,00', '100000,00')"
                        + "insert into Nota_Supplier " +
                        "(Id_NotaSupplier, " +
                        "Id_Supplier, " +
                        "Id_ObatApotek, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Jumlah, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('004','Supplier4','004','04-04-2022','14:40:53','10','4000000,00', '4100000,00', '100000,00')"
                        + "insert into Nota_Supplier " +
                        "(Id_NotaSupplier, " +
                        "Id_Supplier, " +
                        "Id_ObatApotek, " +
                        "Tanggal, " +
                        "Waktu, " +
                        "Jumlah, " +
                        "Total_Harga, " +
                        "Total_Bayar, " +
                        "Pajak) values " +
                        "('005','Supplier5','005','05-04-2022','15:40:53','10','5000000,00', '5100000,00', '100000,00')", con);
                    cm.ExecuteNonQuery();

                    Console.WriteLine("Data sukses dibuat!");
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops, sepertinya ada yang salah di Memasukkan data. " + e);
                    Console.ReadKey();
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}