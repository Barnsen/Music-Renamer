'Sorry for the dirty code. It was written in 15 minutes. :D

Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading

Module MusicRenamer

    Sub Main()
        Try
            Dim regexstring As String = ""
            Console.Title = "MUSIC RENAME"
            Console.WriteLine("=> MUSIC RENAME <=")
            Console.WriteLine("Enter your music folder path: ")
            Dim path As String = Console.ReadLine()
            If Not (File.Exists(path & "\regex.txt")) Then
                Console.BackgroundColor = ConsoleColor.DarkRed
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine("ERROR: Can't find regex.txt in folder " & path)
                Console.ReadKey()
                Console.Clear()
                Thread.Sleep(500)
                Environment.Exit(0)
            Else
                Using sr As New StreamReader(path & "\regex.txt")
                    Dim line As String
                    line = sr.ReadToEnd()
                    regexstring = line
                    Console.WriteLine("------------------------------------------------")
                    Console.WriteLine("Regex: " & regexstring)
                    Console.WriteLine("------------------------------------------------")
                End Using
            End If
            Console.WriteLine("Files found: " & Directory.GetFiles(path, "*.mp3").Count)
            If (Directory.GetFiles(path, "*.mp3").Count = 0) Then
                Console.BackgroundColor = ConsoleColor.DarkRed
                Console.ForegroundColor = ConsoleColor.White
                Console.WriteLine("ERROR: 0 Songs...")
                Console.ReadKey()
                Console.Clear()
                Thread.Sleep(500)
                Environment.Exit(0)
            End If

            Thread.Sleep(1000)
            Console.WriteLine("Preview new file names:")
            Console.WriteLine("------------------------------------------------")
            Thread.Sleep(1000)
            Dim dii As New DirectoryInfo(path)
            Dim fiiArr As FileInfo() = dii.GetFiles("*.mp3")
            Dim frii As FileInfo
            For Each frii In fiiArr
                Dim nn As String = frii.Name
                Dim regOptions As RegexOptions = RegexOptions.IgnoreCase Or RegexOptions.Singleline
                Dim out As String = Regex.Replace(nn, regexstring, String.Empty)
                Console.WriteLine(out)
            Next frii
            Console.WriteLine("------------------------------------------------")
            Console.WriteLine("It's okay? (Press Y or N)")
            Dim cki As ConsoleKeyInfo
            cki = Console.ReadKey(True)
            If (cki.Key = ConsoleKey.N) Then
                Console.WriteLine("Okay... then edit your regex! Bye.")
                Thread.Sleep(3000)
                Console.Clear()
                Thread.Sleep(500)
                Environment.Exit(0)
            End If
            If (cki.Key = ConsoleKey.Y) Then
                Console.WriteLine("Renaiming.")
                For Each frii In fiiArr
                    Dim nn As String = frii.Name
                    Dim regOptions As RegexOptions = RegexOptions.IgnoreCase Or RegexOptions.Singleline
                    Dim out As String = Regex.Replace(nn, "([0-9])\w+. ", String.Empty)
                    My.Computer.FileSystem.RenameFile(path & "\" & frii.Name, out)
                    Console.Write(".")
                Next frii
            End If
        Catch ex As Exception
            Console.BackgroundColor = ConsoleColor.DarkRed
            Console.ForegroundColor = ConsoleColor.White
            Console.WriteLine("ERROR: " & ex.Message)
            Console.ReadKey()
        End Try
    End Sub

End Module
