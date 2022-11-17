package moore;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Scanner;

/**
 *
 * @author Mehmet
 */
public class Moore {

    public static int satirsay, sutunsay;
    static ArrayList girisler = new ArrayList();
    static int[][] tablo;
    static int[] cıkıslar;

    public static void say() throws FileNotFoundException, IOException {
        File file = new File("INPUT.txt");
        if (!file.exists()) {
            file.createNewFile();
        }
        int say = 0;
        FileReader fileReader = new FileReader(file);
        String line;
        BufferedReader br = new BufferedReader(fileReader);
        while ((line = br.readLine()) != null) {
            for (int i = 0; i < line.length(); i++) {
                if (say == 0) {
                    if (line.charAt(i) == 'q') {
                        satirsay++;
                    }
                } else if (say == 1) {
                    line = line.replace('\t', ' ');
                    if ((int) line.charAt(i) >= 97 && (int) line.charAt(i) <= 122) {
                        sutunsay++;
                        girisler.add(line.charAt(i));
                    }
                }
            }
            say++;
        }
        tablo = new int[satirsay][sutunsay];
        cıkıslar = new int[satirsay];
        br.close();
    }

    public static void oku() throws FileNotFoundException, IOException {
        String kelime = "";
        int sayıcı = 0, s = 1;
        int sutunid = 0, satirid = 0;
        File file = new File("GECISTABLOSU.txt");
        if (!file.exists()) {
            file.createNewFile();
        }
        int say = 0;
        FileReader fileReader = new FileReader(file);
        String line;
        BufferedReader br = new BufferedReader(fileReader);
        line = br.readLine();
        while ((line = br.readLine()) != null) {
            s = 1;
            line += "\t";
            for (int k = 0; k < line.length(); k++) {
                if ((int) line.charAt(k) >= 48 && (int) line.charAt(k) <= 57) {
                    kelime += line.charAt(k);
                } else if (line.charAt(k) == '\t') {
                    if (s == 1) {
                        s++;
                        kelime = "";
                        continue;
                    }
                    tablo[satirid][sutunid] = Integer.parseInt(kelime);
                    sutunid++;
                    kelime = "";
                }
            }
            sutunid = 0;
            satirid++;
        }
        br.close();
        File file1 = new File("OUTPUT.txt");
        if (!file1.exists()) {
            file1.createNewFile();
        }
        FileReader fileReader1 = new FileReader(file1);
        String linecikti;
        String cıkıs = "";
        BufferedReader br1 = new BufferedReader(fileReader1);
        linecikti = br1.readLine();
        while ((linecikti = br1.readLine()) != null) {
            char a = linecikti.charAt(linecikti.length() - 1);
            cıkıs += a;
            cıkıslar[sayıcı] = Integer.parseInt(cıkıs);
            sayıcı++;
            cıkıs = "";
        }
        br1.close();
    }
    static int Astate;
    static String Cikisyaz = "";
    static String DurumYaz = "";

    public static void State(String A) {
        char ch;
        DurumYaz = "q" + Astate + "\t";
        Cikisyaz = cıkıslar[Astate] + "\t";
        for (int i = 0; i < A.length(); i++) {
            ch = A.charAt(i);
            int index = girisler.indexOf(ch);
            Astate = tablo[Astate][index];
            DurumYaz += "q" + Astate + "\t";
            Cikisyaz += cıkıslar[Astate] + "\t";
        }
    }

    public static void main(String[] args) throws IOException {
        boolean kontrol = true;
        say();
        oku();
        Scanner input = new Scanner(System.in);
        System.out.print("İfade Giriniz: ");
        String dizi = input.nextLine();
        for (int i = 0; i < dizi.length(); i++) {
            if (girisler.indexOf(dizi.charAt(i)) == -1) {
                System.out.println("Yanlış İfade Girdiniz!");
                kontrol = false;
                break;
            } else {
                kontrol = true;
            }
        }
        if (kontrol == true) {
            State(dizi);
            String diziyaz = "\t";
            for (int i = 0; i < dizi.length(); i++) {
                diziyaz += dizi.charAt(i) + "\t";
            }
            System.out.println(diziyaz + "\n" + DurumYaz + "\n" + Cikisyaz);
        }
    }
}
