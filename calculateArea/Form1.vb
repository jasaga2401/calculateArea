Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Crypto.Parameters

Public Class Form1
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click

        Dim width As String = Convert.ToInt32(txtWidth.Text)
        Dim length As String = Convert.ToInt32(txtHeight.Text)

        ' database connection string
        Dim connectionString As String = "Server=localhost; Database=dbuser; User ID=root; Password=12Yellow34!"

        Using conn As New MySqlConnection(connectionString)

            conn.Open()

            Using cmd As New MySqlCommand("sp_calculate_area", conn)
                cmd.CommandType = CommandType.StoredProcedure

                ' using stored procedure to calculate area
                cmd.Parameters.AddWithValue("@width", width)
                cmd.Parameters.AddWithValue("@length", length)

                ' output parameter from stored procedure
                Dim areaParam As New MySqlParameter("@area", MySqlDbType.Int32)
                areaParam.Direction = ParameterDirection.Output
                cmd.Parameters.Add(areaParam)

                ' execute the stored procedure
                cmd.ExecuteNonQuery()

                ' get the output parameter value and output to the text box
                Dim area As Integer = Convert.ToInt32(cmd.Parameters("@area").Value)
                txtArea.Text = area.ToString()

            End Using
        End Using

    End Sub
End Class
