Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types

'Form2 untuk crystalreport cetak mutasi jabatan
'Form3 untuk crystalreport cetak proses mutasi jabatan
'Form4 untuk crystalreport cetak kecelakaan kerja
'Form5 untuk crystalreport cetak proses kecelakaan

Public Class Form1
    Dim kueri As String
    Dim sql As String

    'Method untuk menampilkan hasil kueri pencarian data mutasi ke dbgridview saja
    Sub CariMutasi()
        kueri = TextBox1.Text.ToString.ToUpper
        sql = "select a.id_tran,a.status,to_char(a.tgl_cr, 'fmdd MON yyyy')as tgl_cr,a.npk_1,b.nama, d.nama_departemen," &
                "a.npk_2, e.nama as nama_proses,f.n_jabatan as jabatan_lama, g.n_jabatan as jabatan_baru, a.ket " &
                "from t_mutasi_jab a join karyawan b on a.npk_1=b.npk " &
                "join jab c on b.jab=c.id_jabatan join dept d on b.dept=d.id_departemen " &
                "join karyawan e on e.npk=a.npk_2 join jab f on a.n_jab=f.id_jabatan " &
                "join jab g on a.o_jab=g.id_jabatan where b.nama like '%" + kueri + "%'"
        da = New OracleDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "t_mutasi_jab")
        DataGridView1.DataSource = ds.Tables("t_mutasi_jab")
        DataGridView1.ReadOnly = True
        DataGridView1.Columns(0).HeaderText = "KODE TRANSAKSI" '
        DataGridView1.Columns(1).HeaderText = "STATUS"
        DataGridView1.Columns(2).HeaderText = "TANGGAL"
        DataGridView1.Columns(3).HeaderText = "NPK PEMINTA"
        DataGridView1.Columns(4).HeaderText = "NAMA"
        DataGridView1.Columns(5).HeaderText = "DEPARTEMEN"
        DataGridView1.Columns(6).HeaderText = "NPK PROSES"
        DataGridView1.Columns(7).HeaderText = "NAMA"
        DataGridView1.Columns(8).HeaderText = "JABATAN LAMA"
        DataGridView1.Columns(9).HeaderText = "JABATAN BARU"
        DataGridView1.Columns(10).HeaderText = "KETERANGAN"
    End Sub

    'Method untuk menampikan hasil pencarian proses mutasi ke dbgridview saja
    Sub CariProsesMutasi()
        kueri = TextBox2.Text.ToString.ToUpper
        sql = "select b.id_tran, b.status,to_char(b.tgl_cr, 'fmdd MON yyyy')as tgl_cr, a.id_tran as kode_permintaan,k.npk, k.nama," &
            " b.no_tap, b.tgl_tap, b.ket from t_mutasi_jab a left join t_mutasi_jab_pro b on b.id_minta_mutasi=a.id_tran" &
            " left join karyawan k on k.npk=a.npk_2 where b.status='OPEN' and k.nama like '%" + kueri + "%'"
        da = New OracleDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "t_mutasi_jab_pro")
        DataGridView2.DataSource = ds.Tables("t_mutasi_jab_pro")
        DataGridView2.ReadOnly = True
        DataGridView2.Columns(0).HeaderText = "KODE TRANSAKSI" '
        DataGridView2.Columns(1).HeaderText = "STATUS"
        DataGridView2.Columns(2).HeaderText = "TANGGAL"
        DataGridView2.Columns(3).HeaderText = "KODE PERMINTAAN" '
        DataGridView2.Columns(4).HeaderText = "NPK"
        DataGridView2.Columns(5).HeaderText = "NAMA"
        DataGridView2.Columns(6).HeaderText = "NO TAP"
        DataGridView2.Columns(7).HeaderText = "TANGGAL TAP"
        DataGridView2.Columns(8).HeaderText = "KETERANGAN"
    End Sub

    'Method untuk menampikan hasil kueri pencarian kecelakaan kerja ke Datagridview
    Sub CariKecelakaanKerja()
        kueri = TextBox3.Text.ToString.ToUpper
        sql = "select a.id_tran,a.status,a.tgl_cr,a.no_kpa,a.npk,b.nama,a.tkp,a.t_rawat,a.tgl_tkp,a.ket" &
            " from t_celaka a left join karyawan b on b.npk=a.npk where b.nama like '%" + kueri + "%' order by a.id_tran"
        da = New OracleDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "t_celaka")
        DataGridView3.DataSource = ds.Tables("t_celaka")
        DataGridView3.ReadOnly = True
        DataGridView3.Columns(0).HeaderText = "KODE TRANSAKSI" '
        DataGridView3.Columns(1).HeaderText = "STATUS"
        DataGridView3.Columns(2).HeaderText = "TANGGAL"
        DataGridView3.Columns(3).HeaderText = "NO KPA" '
        DataGridView3.Columns(4).HeaderText = "NPK"
        DataGridView3.Columns(5).HeaderText = "NAMA"
        DataGridView3.Columns(6).HeaderText = "TEMPAT KK"
        DataGridView3.Columns(7).HeaderText = "TEMPAT RAWAT"
        DataGridView3.Columns(8).HeaderText = "TANGGAL KK"
        DataGridView3.Columns(9).HeaderText = "KETERANGAN"
    End Sub

    'Method untuk menampilkan hasil kueri pencarian proses kecelakaan ke datagridview
    Sub CariProsesKecelakaan()
        kueri = TextBox4.Text.ToString.ToUpper
        sql = "select b.id_tran,b.status,b.no_laka, a.tgl_cr,a.npk,c.nama,a.ket" &
            " from t_celaka a left join t_celaka_pro b on b.no_laka=a.id_tran left join" &
            " karyawan c on a.npk=c.npk where a.status='OPEN' and c.nama like '%" + kueri + "%' order by b.id_tran"
        da = New OracleDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "t_celaka_pro")
        DataGridView4.DataSource = ds.Tables("t_celaka_pro")
        DataGridView4.ReadOnly = True
        DataGridView4.Columns(0).HeaderText = "KODE TRANSAKSI" '
        DataGridView4.Columns(1).HeaderText = "STATUS"
        DataGridView4.Columns(2).HeaderText = "KODE TRAN PENGAJUAN"
        DataGridView4.Columns(3).HeaderText = "NPK" '
        DataGridView4.Columns(4).HeaderText = "NAMA"
        DataGridView4.Columns(5).HeaderText = "TEMPAT KK"
    End Sub

    'Method untuk load kueri pencarian mutasi jabatan ke dataset untuk dicetak ke crystalreport
    Sub loadHasil1()
        kueri = TextBox1.Text.ToString.ToUpper
        sql = "select a.id_tran,a.status,to_char(a.tgl_cr, 'fmdd MON yyyy')as tgl_cr,a.npk_1,b.nama," &
                " d.nama_departemen, a.npk_2, e.nama as nama_proses,f.n_jabatan as jabatan_lama, g.n_jabatan as jabatan_baru," &
                " a.ket,  h.nama as dibuat, l.n_jabatan as jab_buat, i.nama as diperiksa, m.n_jabatan as jab_periksa," &
                " j.nama as disetujui, n.n_jabatan as jab_setuju, k.nama as mengetahui, o.n_jabatan as jab_mengetahui" &
                " from t_mutasi_jab a join karyawan b on a.npk_1=b.npk join jab c on b.jab=c.id_jabatan" &
                " join dept d on b.dept=d.id_departemen join karyawan e on e.npk=a.npk_2" &
                " join jab f on a.n_jab=f.id_jabatan join jab g on a.o_jab=g.id_jabatan" &
                " left join karyawan h on h.npk=a.lev_1 left join karyawan i on i.npk=a.lev_2" &
                " left join karyawan j on j.npk=a.lev_3 left join karyawan k on k.npk=a.lev_4" &
                " left join jab l on l.id_jabatan=h.jab left join jab m on m.id_jabatan=i.jab left" &
                " join jab n on n.id_jabatan=j.jab left join jab o on o.id_jabatan=k.jab where b.nama like '%" + kueri + "%'"
        da = New OracleDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "t_mutasi_jab")
    End Sub

    'Method untuk load kueri pencarian proses mutasi jabatan ke dataset untuk di cetak ke crystal report
    Sub loadHasil2()
        kueri = TextBox2.Text.ToString.ToUpper
        sql = "select b.id_tran, b.status, to_char(b.tgl_cr, 'fmdd MON yyyy')as tgl_cr, a.id_tran as kode_permintaan, k.nama," &
            " o.nama_departemen,l.n_jabatan as jab_lama, m.n_jabatan as jab_baru, n.n_jkary," &
            " b.no_tap, to_char(b.tgl_tap, 'fmdd MON yyyy')as tgl_tap, b.ket," &
            " c.nama as dibuat, g.n_jabatan as jab_buat, d.nama as diperiksa, h.n_jabatan as jab_periksa," &
            " e.nama as disetujui, i.n_jabatan as jab_setuju, f.nama as mengetahui, j.n_jabatan as jab_mengetahui" &
            " from t_mutasi_jab a left join t_mutasi_jab_pro b on b.id_minta_mutasi=a.id_tran" &
            " left join karyawan c on c.npk=a.lev_1 left join karyawan d on d.npk=a.lev_2" &
            " left join karyawan e on e.npk=a.lev_3 left join karyawan f on f.npk=a.lev_4" &
            " left join jab g on g.id_jabatan=c.jab left join jab h on h.id_jabatan=d.jab" &
            " left join jab i on i.id_jabatan=e.jab left join jab j on j.id_jabatan=f.jab" &
            " left join karyawan k on k.npk=a.npk_2 left join jab l on l.id_jabatan=a.o_jab" &
            " left join jab m on m.id_jabatan=a.n_jab left join j_kary n on n.id_jkary=k.j_kary" &
            " left join dept o on o.id_departemen=k.dept" &
            " where b.status='OPEN' and k.nama like '%" + kueri + "%'"
        da = New OracleDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "t_mutasi_jab_pro")
    End Sub

    'Method untuk load hasil kueri pencarian kecelakaan kerja ke dataset untuk di cetak ke crystal report
    Sub LoadKecelakaan1()
        kueri = TextBox3.Text.ToString.ToUpper
        sql = "select a.id_tran,a.status,to_char(a.tgl_cr, 'fmdd MON yyyy')as tgl_cr,a.no_kpa,a.npk,b.nama,a.tkp,a.t_rawat,to_char(a.tgl_tkp, 'fmdd MON yyyy')as tgl_tkp,a.ket," &
            "h.nama as pembuat,l.n_jabatan as jab_buat, i.nama as diperiksa, m.n_jabatan as jab_periksa," &
            " j.nama as disetujui,n.n_jabatan as jab_setuju, k.nama as mengetahui, o.n_jabatan as jab_mengetahui" &
            " from t_celaka a left join karyawan b on b.npk=a.npk left join karyawan h on h.npk=a.lev_1" &
            " left join karyawan i on i.npk=a.lev_2 left join karyawan j on j.npk=a.lev_3" &
            " left join karyawan k on k.npk=a.lev_4 left join jab l on l.id_jabatan=h.jab" &
            " left join jab m on m.id_jabatan=i.jab left join jab n on n.id_jabatan=j.jab" &
            " left join jab o on o.id_jabatan=k.jab where b.nama like '%" + kueri + "%' order by a.id_tran"
        da = New OracleDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "t_celaka")
    End Sub

    'Method untuk load hasil kueri pencarian proses kecelakaan kerja ke dataset untuk di cetak ke crystal report
    Sub LoadKecelakaan2()
        kueri = TextBox4.Text.ToString.ToUpper
        sql = "select b.id_tran,b.status,b.no_laka, to_char(a.tgl_cr, 'fmdd MON yyyy')as tgl_cr,a.npk,c.nama,a.ket, d.nama as dibuat," &
            " e.n_jabatan as jab_buat, f.nama as diperiksa, g.n_jabatan as jab_periksa, h.nama as disetujui, i.n_jabatan as jab_setuju, j.nama as mengetahui, k.n_jabatan as jab_mengetahui" &
            " from t_celaka a left join t_celaka_pro b on b.no_laka=a.id_tran" &
            " left join karyawan c on a.npk=c.npk left join karyawan d on d.npk=b.lev_1" &
            " left join jab e on e.id_jabatan=d.jab left join karyawan f on f.npk=b.lev_2" &
            " left join jab g on g.id_jabatan=f.jab left join karyawan h on h.npk=b.lev_3" &
            " left join jab i on i.id_jabatan=h.jab left join karyawan j on j.npk=b.lev_4" &
            " left join jab k on k.id_jabatan=j.jab and c.nama like '%" + kueri + "%' order by b.id_tran"
        da = New OracleDataAdapter(sql, con)
        ds = New DataSet
        da.Fill(ds, "t_celaka_pro")
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call koneksi()
        Call loadHasil1()
        Form2.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call koneksi()
        Call loadHasil2()
        Form3.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call koneksi()
        Call LoadKecelakaan1()
        Form4.Show()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Call koneksi()
        Call LoadKecelakaan2()
        Form5.Show()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Call koneksi()
        Call CariMutasi()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Call koneksi()
        Call CariProsesMutasi()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Call koneksi()
        Call CariKecelakaanKerja()
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Call koneksi()
        Call CariProsesKecelakaan()
    End Sub
End Class
