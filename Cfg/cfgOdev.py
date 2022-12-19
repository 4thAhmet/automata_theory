# CFG'ye dil ağacı üretme

#Dallanma olan eleman ve içindeki pozisyon numarası
ctrlDe = 0
ctrlDp = 0
breanching = 1

def isbreanching():
    # bu fonksiyon içinde tanımlanan boy, i, deger, pozisyon, j, d,
    # de, dp, dallanmavar değişkenlerinin tamamı local değişkenlerdir.
    boy = len(tree)
    for i in range(boy):
        deger = tree[i]
        pozisyon = len(deger)
        for j in range(pozisyon):
            d = deger[j]
            if d in sozluk.keys():
                de = i
                dp = j
                dallanmavar = 1       
                break
        else:
            # j bitmiş. i'i dönmeye devam et
            continue
        # üstten yani j içinden break ile gelinmişse, i'i de break et.
        break
    else:
        # i döngüsü de bitmiş.
        dallanmavar, de, dp = 0, 0, 0
    # buraya ya break break'le gelinir veya son else işlendikten sonra gelinir.
    return (dallanmavar, de, dp)



cfg = "S-->aa|bX|aXXXXX,X-->ab|b"
#cfg = "S-->aa|bX|aXX|aZ,X-->ab|b,Z-->a|bb"
#cfg = "S-->aa|bX|aXX|aZ,X-->ab|b|Zb,Z-->a|bb"
print("CFG: ",cfg)
#Satırları ayıralım
k=cfg.split(',')

#Alt kümeleri oluşturalım
ilk = True
giris = ''
sozluk = {}
for i in k:
    degisken, degerler = i.split('-->')
    sozluk[degisken]=degerler.split('|')
    if ilk == True: giris = degisken ; ilk = False 



#Tum dil ağacı icin girişten başlayalım
tree = sozluk[giris]
maxdongu = 100
for dongusay in range(maxdongu):    
    breanching, ctrlDe, ctrlDp = isbreanching()   
    if breanching == 1:
        eleman = tree[ctrlDe]
        elemansol, elemansag = eleman[:ctrlDp], eleman[ctrlDp+1:]  
        altdallar = []
        altdal = tree[ctrlDe][ctrlDp]
        for dal in sozluk[altdal]:
            altdallar.append(elemansol + dal + elemansag)
        # Altdalı olan eleman silinip, alt dalları yerleştirilecek.
        tree[ctrlDe:ctrlDe+1] = altdallar
    else:
        # Dallanma olmadığı için artık döngüye gerek yok
        break
else:
    print()
    print('*** HATA VAR *** ')
    print('{} KEZ DALLANMA YAPILDIĞI HALDE DALLANMA TAMAMLANAMADI !!!!!!'.format(maxdongu))
    

        

print('-'*len('TÜM AĞAÇ: '+repr(tree)))
# Tum ağacın içindeki birden fazla kayıtların ayıklanması
uretilenkelimeler = list(set(tree))
uretilenkelimeler.sort()
print('ÜRETİLEN KELİMELER:', uretilenkelimeler)

#Tekrarlanan kelimelerin bulunması
tekrarlanankelimeler = tree.copy()
#tumagac'dan üretilen kelimeler çıkartılarak, tekrarlanan kelimeler bulunacak
for i in uretilenkelimeler:
    tekrarlanankelimeler.remove(i)

#tekrarlanan kelimelerin birden fazla ise teke indirilmesi
tekrarlanankelimeler = list(set(tekrarlanankelimeler))
tekrarlanankelimeler.sort()
print('TEKRARLANAN KELİMELER:', tekrarlanankelimeler)
