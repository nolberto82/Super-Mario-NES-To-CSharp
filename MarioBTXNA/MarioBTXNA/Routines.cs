using Microsoft.Xna.Framework;

namespace MarioBTXNA
{
    public partial class Game1 : Game
    {
        byte[] ram;
        public byte[] vram;
        public byte ppuScrollX;
        byte a, x, y;
        int ppuAddr;
        int cycles;
        bool c, n, z;
        bool writeToggle;
        int s;

        void Init(byte[] file)
        {
            ram = new byte[0x10000];
            vram = new byte[0x4000];
            for (int i = 0x8000; i < 0x10000; i++)
                ram[i] = file[i - 0x8000 + 0x10];

            for (int i = 0x0; i < 0x2000; i++)
                vram[i] = file[i + 0x8000 + 0x10];
        }

        void Label8000()
        {
            lda(0x10);//8002
            str(0x2000, a);//8004
            ldx(0xFF);//8007
            txs();//8009
            Label800A:
            ldam(0x2002);//800A
            if (!n)
                goto Label800A;//800D
            Label800F:
            ldam(0x2002);//800F
            if (!n)
                goto Label800F;//8012
            ldy(0xFE);//8014
            ldx(0x05);//8016
            Label8018:
            ldam(0x07D7 + x);//8018
            cmp(a, 0x0A);//801B
            if (c)
                goto Label802B;//801D
            dex();//801F
            if (!n)
                goto Label8018;//8020
            ldam(0x07FF);//8022
            cmp(a, 0xA5);//8025
            if (!z)
                goto Label802B;//8027
            ldy(0xD6);//8029
            Label802B:
            Label90CC();//802B
            str(0x4011, a);//802E
            str(0x0770, a);//8031
            lda(0xA5);//8034
            str(0x07FF, a);//8036
            str(0x07A7, a);//8039
            lda(0x0F);//803C
            str(0x4015, a);//803E
            lda(0x06);//8041
            str(0x2001, a);//8043
            Label8220();//8046
            Label8E19();//8049
            inc(0x0774);//804C
            ldam(0x0778);//804F
            or(0x80);//8052
            Label8EED();//8054
            //Label8057(); //8057
            return;
        }

        void Label8082()
        {
            ldam(0x0778);//8082
            and(0x7F);//8085
            str(0x0778, a);//8087
            and(0x7E);//808A
            str(0x2000, a);//808C
            ldam(0x0779);//808F
            and(0xE6);//8092
            ldym(0x0774);//8094
            if (!z)
                goto Label809E;//8097
            ldam(0x0779);//8099
            or(0x1E);//809C
            Label809E:
            str(0x0779, a);//809E
            and(0xE7);//80A1
            str(0x2001, a);//80A3
            ldxm(0x2002);//80A6
            lda(0x00);//80A9
            Label8EE6();//80AB
            str(0x2003, a);//80AE
            lda(0x02);//80B1
            str(0x4014, a);//80B3
            ldxm(0x0773);//80B6
            ldam(0x805A + x);//80B9
            str(0x00, a);//80BC
            ldam(0x806D + x);//80BE
            str(0x01, a);//80C1
            Label8EDD();//80C3
            ldy(0x00);//80C6
            ldxm(0x0773);//80C8
            cmp(x, 0x06);//80CB
            if (!z)
                goto Label80D0;//80CD
            iny();//80CF
            Label80D0:
            ldxm(0x8080 + y);//80D0
            lda(0x00);//80D3
            str(0x0300 + x, a);//80D5
            str(0x0301 + x, a);//80D8
            str(0x0773, a);//80DB
            ldam(0x0779);//80DE
            str(0x2001, a);//80E1
            LabelF2D0();//80E4
            Label8E5C();//80E7
            Label8182();//80EA
            Label8F97();//80ED
            ldam(0x0776);//80F0
            lsr();//80F3
            if (c)
                goto Label811B;//80F4
            ldam(0x0747);//80F6
            if (z)
                goto Label8100;//80F9
            dec(0x0747);//80FB
            if (!z)
                goto Label8119;//80FE
            Label8100:
            ldx(0x14);//8100
            dec(0x077F);//8102
            if (!n)
                goto Label810E;//8105
            lda(0x14);//8107
            str(0x077F, a);//8109
            ldx(0x23);//810C
            Label810E:
            ldam(0x0780 + x);//810E
            if (z)
                goto Label8116;//8111
            dec(0x0780 + x);//8113
            Label8116:
            dex();//8116
            if (!n)
                goto Label810E;//8117
            Label8119:
            inc(0x09);//8119
            Label811B:
            ldx(0x00);//811B
            ldy(0x07);//811D
            ldam(0x07A7);//811F
            and(0x02);//8122
            str(0x00, a);//8124
            ldam(0x07A8);//8126
            and(0x02);//8129
            eorm(0x00);//812B
            c = false;//812D
            if (z)
                goto Label8131;//812E
            c = true;//8130
            Label8131:
            rorm(0x07A7 + x);//8131
            inx();//8134
            dey();//8135
            if (!z)
                goto Label8131;//8136
            ldam(0x0722);//8138
            if (z)
                goto Label815C;//813B
            Label813D:
            ldam(0x2002);//813D
            and(0x40);//8140
            if (!z)
                goto Label813D;//8142
            ldam(0x0776);//8144
            lsr();//8147
            if (c)
                goto Label8150;//8148
            Label8223();//814A
            Label81C6();//814D
            Label8150:
            ldam(0x2002);//8150
            and(0x40);//8153
            if (z)
                goto Label8150;//8155
            ldy(0x14);//8157
            Label8159:
            dey();//8159
            if (!z)
                goto Label8159;//815A
            Label815C:
            ldam(0x073F);//815C
            str(0x2005, a);//815F
            ldam(0x0740);//8162
            str(0x2005, a);//8165
            ldam(0x0778);//8168
            pha();//816B
            str(0x2000, a);//816C
            ldam(0x0776);//816F
            lsr();//8172
            if (c)
                goto Label8178;//8173
            Label8212();//8175
            Label8178:
            ldam(0x2002);//8178
            pla();//817B
            or(0x80);//817C
            str(0x2000, a);//817E
        }

        void Label8182()
        {
            ldam(0x0770);//8182
            cmp(a, 0x02);//8185
            if (z)
                goto Label8194;//8187
            cmp(a, 0x01);//8189
            if (!z)
                return;//818B
            ldam(0x0772);//818D
            cmp(a, 0x03);//8190
            if (!z)
                return;//8192
            Label8194:
            ldam(0x0777);//8194
            if (z)
                goto Label819D;//8197
            dec(0x0777);//8199
            return;
            Label819D:
            ldam(0x06FC);//819D
            and(0x10);//81A0
            if (z)
                goto Label81BD;//81A2
            ldam(0x0776);//81A4
            and(0x80);//81A7
            if (!z)
                return;//81A9
            lda(0x2B);//81AB
            str(0x0777, a);//81AD
            ldam(0x0776);//81B0
            tay();//81B3
            iny();//81B4
            str(0xFA, y);//81B5
            eor(0x01);//81B7
            or(0x80);//81B9
            if (!z)
                goto Label81C2;//81BB
            Label81BD:
            ldam(0x0776);//81BD
            and(0x7F);//81C0
            Label81C2:
            str(0x0776, a);//81C2
        }

        void Label81C6()
        {
            ldym(0x074E);//81C6
            lda(0x28);//81C9
            str(0x00, a);//81CB
            ldx(0x0E);//81CD
            Label81CF:
            ldam(0x06E4 + x);//81CF
            cmp(a, ram[0x00]);//81D2
            if (!c)
                goto Label81E5;//81D4
            ldym(0x06E0);//81D6
            c = false;//81D9
            adc(ram[0x06E1 + y]);//81DA
            if (!c)
                goto Label81E2;//81DD
            c = false;//81DF
            adc(ram[0x00]);//81E0
            Label81E2:
            str(0x06E4 + x, a);//81E2
            Label81E5:
            dex();//81E5
            if (!n)
                goto Label81CF;//81E6
            ldxm(0x06E0);//81E8
            inx();//81EB
            cmp(x, 0x03);//81EC
            if (!z)
                goto Label81F2;//81EE
            ldx(0x00);//81F0
            Label81F2:
            str(0x06E0, x);//81F2
            ldx(0x08);//81F5
            ldy(0x02);//81F7
            Label81F9:
            ldam(0x06E9 + y);//81F9
            str(0x06F1 + x, a);//81FC
            c = false;//81FF
            adc(0x08);//8200
            str(0x06F2 + x, a);//8202
            c = false;//8205
            adc(0x08);//8206
            str(0x06F3 + x, a);//8208
            dex();//820B
            dex();//820C
            dex();//820D
            dey();//820E
            if (!n)
                goto Label81F9;//820F
        }

        void Label8212()
        {
            ldam(0x0770);//8212
            switch (a)
            {
                case 0:
                    Label8231();
                    break;
                case 1:
                    LabelAEDC();
                    break;
                case 2:
                    Label838B();
                    break;
                case 3:
                    Label9218();
                    break;
            }
        }

        void Label8220()
        {
            ldy(0x00);//8220
            Label8225();
        }

        void Label8223()
        {
            ldy(0x04);//8223
            Label8225();
        }

        void Label8225()
        {
            lda(0xF8);//8225
            Label8227:
            str(0x0200 + y, a);//8227
            iny();//822A
            iny();//822B
            iny();//822C
            iny();//822D
            if (!z)
                goto Label8227;//822E
        }

        void Label8231()
        {
            ldam(0x0772);//8231
            switch (a)
            {
                case 0:
                    Label8FCF();
                    break;
                case 1:
                    Label8567();
                    break;
                case 2:
                    Label9061();
                    break;
                case 3:
                    Label8245();
                    break;
            }
        }

        void Label8245()
        {
            ldy(0x00);//8245
            ldam(0x06FC);//8247
            orm(0x06FD);//824A
            cmp(a, 0x10);//824D
            if (z)
                goto Label8255;//824F
            cmp(a, 0x90);//8251
            if (!z)
                goto Label8258;//8253
            Label8255:
            goto Label82D8; //8255
            Label8258:
            cmp(a, 0x20);//8258
            if (z)
                goto Label8276;//825A
            ldxm(0x07A2);//825C
            if (!z)
                goto Label826C;//825F
            str(0x0780, a);//8261
            Label836B();//8264
            if (c)
                goto Label82C9;//8267
            Label82C0(); //8269
            return;
            Label826C:
            ldxm(0x07FC);//826C
            if (z)
                goto Label82BB;//826F
            cmp(a, 0x40);//8271
            if (!z)
                goto Label82BB;//8273
            iny();//8275
            Label8276:
            ldam(0x07A2);//8276
            if (z)
                goto Label82C9;//8279
            lda(0x18);//827B
            str(0x07A2, a);//827D
            ldam(0x0780);//8280
            if (!z)
                goto Label82BB;//8283
            lda(0x10);//8285
            str(0x0780, a);//8287
            cmp(y, 0x01);//828A
            if (z)
                goto Label829C;//828C
            ldam(0x077A);//828E
            eor(0x01);//8291
            str(0x077A, a);//8293
            Label8325();//8296
            Label82BB(); //8299
            return;
            Label829C:
            ldxm(0x076B);//829C
            inx();//829F
            txa();//82A0
            and(0x07);//82A1
            str(0x076B, a);//82A3
            Label830E();//82A6
            Label82A9:
            ldam(0x823F + x);//82A9
            str(0x0300 + x, a);//82AC
            inx();//82AF
            cmp(x, 0x06);//82B0
            if (n) //
                goto Label82A9;//82B2
            ldym(0x075F);//82B4
            iny();//82B7
            str(0x0304, y);//82B8
            Label82BB:
            lda(0x00);//82BB
            str(0x06FC, a);//82BD
            LabelAEEA();//82C0
            ldam(0x0E);//82C3
            cmp(a, 0x06);//82C5
            if (!z)
                return;//82C7
            Label82C9:
            lda(0x00);//82C9
            str(0x0770, a);//82CB
            str(0x0772, a);//82CE
            str(0x0722, a);//82D1
            inc(0x0774);//82D4
            return;
            Label82D8:
            ldym(0x07A2);//82D8
            if (z)
                goto Label82C9;//82DB
            asl();//82DD
            if (!c)
                goto Label82E6;//82DE
            ldam(0x07FD);//82E0
            Label830E();//82E3
            Label82E6:
            Label9C03();//82E6
            inc(0x075D);//82E9
            inc(0x0764);//82EC
            inc(0x0757);//82EF
            inc(0x0770);//82F2
            ldam(0x07FC);//82F5
            str(0x076A, a);//82F8
            lda(0x00);//82FB
            str(0x0772, a);//82FD
            str(0x07A2, a);//8300
            ldx(0x17);//8303
            lda(0x00);//8305
            Label8307:
            str(0x07DD + x, a);//8307
            dex();//830A
            if (!n)
                goto Label8307;//830B
        }

        void Label82BB()
        {
            lda(0x00);//82BB
            str(0x06FC, a);//82BD
            LabelAEEA();//82C0
            ldam(0x0E);//82C3
            cmp(a, 0x06);//82C5
            if (!z)
                return;//82C7
            lda(0x00);//82C9
            str(0x0770, a);//82CB
            str(0x0772, a);//82CE
            str(0x0722, a);//82D1
            inc(0x0774);//82D4
        }

        void Label82C0()
        {
            LabelAEEA();//82C0
            ldam(0x0E);//82C3
            cmp(a, 0x06);//82C5
            if (!z)
                return;//82C7
            lda(0x00);//82C9
            str(0x0770, a);//82CB
            str(0x0772, a);//82CE
            str(0x0722, a);//82D1
            inc(0x0774);//82D4
        }

        void Label830E()
        {
            str(0x075F, a);//830E
            str(0x0766, a);//8311
            ldx(0x00);//8314
            str(0x0760, x);//8316
            str(0x0767, x);//8319
        }

        void Label8325()
        {
            ldy(0x07);//8325
            Label8327:
            ldam(0x831D + y);//8327
            str(0x0300 + y, a);//832A
            dey();//832D
            if (!n)
                goto Label8327;//832E
            ldam(0x077A);//8330
            if (z)
                return;//8333
            lda(0x24);//8335
            str(0x0304, a);//8337
            lda(0xCE);//833A
            str(0x0306, a);//833C
        }

        void Label836B()
        {
            ldxm(0x0717);//836B
            ldam(0x0718);//836E
            if (!z)
                goto Label8380;//8371
            inx();//8373
            inc(0x0717);//8374
            c = true;//8377
            ldam(0x8354 + x);//8378
            str(0x0718, a);//837B
            if (z)
                return;//837E
            Label8380:
            ldam(0x833F + x);//8380
            str(0x06FC, a);//8383
            dec(0x0718);//8386
            c = false;//8389
        }

        void Label838B()
        {
            Label83A0();//838B
            ldam(0x0772);//838E
            if (z)
                goto Label839A;//8391
            ldx(0x00);//8393
            str(0x08, x);//8395
            LabelC047();//8397
            Label839A:
            LabelF12A();//839A
            LabelEEE9(); //839D
            return;
        }

        void Label83A0()
        {
            ldam(0x0772);//83A0
            switch (a)
            {
                case 0:
                    LabelCFEC();
                    break;
                case 1:
                    Label83B0();
                    break;
                case 2:
                    Label83BD();
                    break;
                case 3:
                    Label83F6();
                    break;
                case 4:
                    Label8461();
                    break;
            }
        }

        void Label83B0()
        {
            ldxm(0x071B);//83B0
            inx();//83B3
            str(0x34, x);//83B4
            lda(0x08);//83B6
            str(0xFC, a);//83B8
            Label874E(); //83BA
            return;
        }

        void Label83BD()
        {
            ldy(0x00);//83BD
            str(0x35, y);//83BF
            ldam(0x6D);//83C1
            cmp(a, ram[0x34]);//83C3
            if (!z)
                goto Label83CD;//83C5
            ldam(0x86);//83C7
            cmp(a, 0x60);//83C9
            if (c)
                goto Label83D0;//83CB
            Label83CD:
            inc(0x35);//83CD
            iny();//83CF
            Label83D0:
            tya();//83D0
            LabelB0E6();//83D1
            ldam(0x071A);//83D4
            cmp(a, ram[0x34]);//83D7
            if (z)
                goto Label83F1;//83D9
            ldam(0x0768);//83DB
            c = false;//83DE
            adc(0x80);//83DF
            str(0x0768, a);//83E1
            lda(0x01);//83E4
            adc(0x00);//83E6
            tay();//83E8
            LabelAFC4();//83E9
            LabelAF6F();//83EC
            inc(0x35);//83EF
            Label83F1:
            ldam(0x35);//83F1
            if (z)
                Label845D();//83F3
        }

        void Label83F6()
        {
            ldam(0x0749);//83F6
            if (!z)
                goto Label8443;//83F9
            ldam(0x0719);//83FB
            if (z)
                goto Label8418;//83FE
            cmp(a, 0x09);//8400
            if (c)
                goto Label8443;//8402
            ldym(0x075F);//8404
            cmp(y, 0x07);//8407
            if (!z)
                goto Label8414;//8409
            cmp(a, 0x03);//840B
            if (!c)
                goto Label8443;//840D
            sbc(0x01);//840F
            Label8418(); //8411
            return;
            Label8414:
            cmp(a, 0x02);//8414
            if (!c)
                goto Label8443;//8416
            Label8418:
            tay();//8418
            if (!z)
                goto Label8423;//8419
            ldam(0x0753);//841B
            if (z)
                goto Label8434;//841E
            iny();//8420
            if (!z)
                goto Label8434;//8421
            Label8423:
            iny();//8423
            ldam(0x075F);//8424
            cmp(a, 0x07);//8427
            if (z)
                goto Label8434;//8429
            dey();//842B
            cmp(y, 0x04);//842C
            if (c)
                goto Label8456;//842E
            cmp(y, 0x03);//8430
            if (c)
                goto Label8443;//8432
            Label8434:
            cmp(y, 0x03);//8434
            if (!z)
                goto Label843C;//8436
            lda(0x04);//8438
            str(0xFC, a);//843A
            Label843C:
            tya();//843C
            c = false;//843D
            adc(0x0C);//843E
            str(0x0773, a);//8440
            Label8443:
            ldam(0x0749);//8443
            c = false;//8446
            adc(0x04);//8447
            str(0x0749, a);//8449
            ldam(0x0719);//844C
            adc(0x00);//844F
            str(0x0719, a);//8451
            cmp(a, 0x07);//8454
            Label8456:
            if (!c)
                return;//8456
            lda(0x06);//8458
            str(0x07A1, a);//845A
            Label845D();
        }

        void Label8418()
        {
            tay();//8418
            if (!z)
                goto Label8423;//8419
            ldam(0x0753);//841B
            if (z)
                goto Label8434;//841E
            iny();//8420
            if (!z)
                goto Label8434;//8421
            Label8423:
            iny();//8423
            ldam(0x075F);//8424
            cmp(a, 0x07);//8427
            if (z)
                goto Label8434;//8429
            dey();//842B
            cmp(y, 0x04);//842C
            if (c)
                goto Label8456;//842E
            cmp(y, 0x03);//8430
            if (c)
                goto Label8443;//8432
            Label8434:
            cmp(y, 0x03);//8434
            if (!z)
                goto Label843C;//8436
            lda(0x04);//8438
            str(0xFC, a);//843A
            Label843C:
            tya();//843C
            c = false;//843D
            adc(0x0C);//843E
            str(0x0773, a);//8440
            Label8443:
            ldam(0x0749);//8443
            c = false;//8446
            adc(0x04);//8447
            str(0x0749, a);//8449
            ldam(0x0719);//844C
            adc(0x00);//844F
            str(0x0719, a);//8451
            cmp(a, 0x07);//8454
            Label8456:
            if (!c)
                return;//8456
            lda(0x06);//8458
            str(0x07A1, a);//845A
        }

        void Label845D()
        {
            inc(0x0772);//845D
        }

        void Label8461()
        {
            ldam(0x07A1);//8461
            if (!z)
                return;//8464
            ldym(0x075F);//8466
            cmp(y, 0x07);//8469
            if (c)
                goto Label8487;//846B
            lda(0x00);//846D
            str(0x0760, a);//846F
            str(0x075C, a);//8472
            str(0x0772, a);//8475
            inc(0x075F);//8478
            Label9C03();//847B
            inc(0x0757);//847E
            lda(0x01);//8481
            str(0x0770, a);//8483
            return;
            Label8487:
            ldam(0x06FC);//8487
            orm(0x06FD);//848A
            and(0x40);//848D
            if (z)
                return;//848F
            lda(0x01);//8491
            str(0x07FC, a);//8493
            lda(0xFF);//8496
            str(0x075A, a);//8498
            Label9248();//849B
        }

        void Label84C3()
        {
            ldam(0x0110 + x);//84C3
            if (z)
                return;//84C6
            cmp(a, 0x0B);//84C8
            if (!c)
                goto Label84D1;//84CA
            lda(0x0B);//84CC
            str(0x0110 + x, a);//84CE
            Label84D1:
            tay();//84D1
            ldam(0x012C + x);//84D2
            if (!z)
                goto Label84DB;//84D5
            str(0x0110 + x, a);//84D7
            return;
            Label84DB:
            dec(0x012C + x);//84DB
            cmp(a, 0x2B);//84DE
            if (!z)
                goto Label8500;//84E0
            cmp(y, 0x0B);//84E2
            if (!z)
                goto Label84ED;//84E4
            inc(0x075A);//84E6
            lda(0x40);//84E9
            str(0xFE, a);//84EB
            Label84ED:
            ldam(0x84B7 + y);//84ED
            lsr();//84F0
            lsr();//84F1
            lsr();//84F2
            lsr();//84F3
            tax();//84F4
            ldam(0x84B7 + y);//84F5
            and(0x0F);//84F8
            str(0x0134 + x, a);//84FA
            LabelBC27();//84FD
            Label8500:
            ldym(0x06E5 + x);//8500
            ldam(0x16 + x);//8503
            cmp(a, 0x12);//8505
            if (z)
                goto Label852B;//8507
            cmp(a, 0x0D);//8509
            if (z)
                goto Label852B;//850B
            cmp(a, 0x05);//850D
            if (z)
                goto Label8523;//850F
            cmp(a, 0x0A);//8511
            if (z)
                goto Label852B;//8513
            cmp(a, 0x0B);//8515
            if (z)
                goto Label852B;//8517
            cmp(a, 0x09);//8519
            if (c)
                goto Label8523;//851B
            ldam(0x1E + x);//851D
            cmp(a, 0x02);//851F
            if (c)
                goto Label852B;//8521
            Label8523:
            ldxm(0x03EE);//8523
            ldym(0x06EC + x);//8526
            ldxm(0x08);//8529
            Label852B:
            ldam(0x011E + x);//852B
            cmp(a, 0x18);//852E
            if (!c)
                goto Label8537;//8530
            sbc(0x01);//8532
            str(0x011E + x, a);//8534
            Label8537:
            ldam(0x011E + x);//8537
            sbc(0x08);//853A
            LabelE5C1();//853C
            ldam(0x0117 + x);//853F
            str(0x0203 + y, a);//8542
            c = false;//8545
            adc(0x08);//8546
            str(0x0207 + y, a);//8548
            lda(0x02);//854B
            str(0x0202 + y, a);//854D
            str(0x0206 + y, a);//8550
            ldam(0x0110 + x);//8553
            asl();//8556
            tax();//8557
            ldam(0x849F + x);//8558
            str(0x0201 + y, a);//855B
            ldam(0x84A0 + x);//855E
            str(0x0205 + y, a);//8561
            ldxm(0x08);//8564
        }

        void Label8567()
        {
            ldam(0x073C);//8567
            switch (a)
            {
                case 0:
                    Label858B();
                    break;
                case 1:
                    Label859B();
                    break;
                case 2:
                    Label8652();
                    break;
                case 3:
                    Label865A();
                    break;
                case 4:
                    Label8693();
                    break;
                case 5:
                    Label889D();
                    break;
                case 6:
                    Label86A8();
                    break;
                case 7:
                    Label889D();
                    break;
                case 8:
                    Label86E6();
                    break;
                case 9:
                    Label85BF();
                    break;
                case 10:
                    Label85E3();
                    break;
                case 11:
                    Label8643();
                    break;
                case 12:
                    Label86FF();
                    break;
                case 13:
                    Label8732();
                    break;
                case 14:
                    Label8749();
                    break;
            }
        }

        void Label858B()
        {
            Label8220();//858B
            Label8E19();//858E
            ldam(0x0770);//8591
            if (z)
            {
                Label85C8();//8594
                return;
            }
            ldx(0x03);//8596
            Label85C5(); //8598
            return;
        }

        void Label859B()
        {
            ldam(0x0744);//859B
            pha();//859E
            ldam(0x0756);//859F
            pha();//85A2
            lda(0x00);//85A3
            str(0x0756, a);//85A5
            lda(0x02);//85A8
            str(0x0744, a);//85AA
            Label85F1();//85AD
            pla();//85B0
            str(0x0756, a);//85B1
            pla();//85B4
            str(0x0744, a);//85B5
            Label8745(); //85B8
            return;
        }

        void Label85BF()
        {
            ldym(0x074E);//85BF
            ldxm(0x85BB + y);//85C2
            str(0x0773, x);//85C5
            Label8745(); //85C8
            return;
        }

        void Label85C5()
        {
            str(0x0773, x);//85C5
            Label8745();
        }

        void Label85C8()
        {
            Label8745(); //85C8
            return;
        }

        void Label85E3()
        {
            ldym(0x0744);//85E3
            if (z)
                goto Label85EE;//85E6
            ldam(0x85C7 + y);//85E8
            str(0x0773, a);//85EB
            Label85EE:
            inc(0x073C);//85EE
            ldxm(0x0300);//85F1
            ldy(0x00);//85F4
            ldam(0x0753);//85F6
            if (z)
                goto Label85FD;//85F9
            ldy(0x04);//85FB
            Label85FD:
            ldam(0x0756);//85FD
            cmp(a, 0x02);//8600
            if (!z)
                goto Label8606;//8602
            ldy(0x08);//8604
            Label8606:
            lda(0x03);//8606
            str(0x00, a);//8608
            Label860A:
            ldam(0x85D7 + y);//860A
            str(0x0304 + x, a);//860D
            iny();//8610
            inx();//8611
            dec(0x00);//8612
            if (!n)
                goto Label860A;//8614
            ldxm(0x0300);//8616
            ldym(0x0744);//8619
            if (!z)
                goto Label8621;//861C
            ldym(0x074E);//861E
            Label8621:
            ldam(0x85CF + y);//8621
            str(0x0304 + x, a);//8624
            lda(0x3F);//8627
            str(0x0301 + x, a);//8629
            lda(0x10);//862C
            str(0x0302 + x, a);//862E
            lda(0x04);//8631
            str(0x0303 + x, a);//8633
            lda(0x00);//8636
            str(0x0308 + x, a);//8638
            txa();//863B
            c = false;//863C
            adc(0x07);//863D
            str(0x0300, a);//863F
        }

        void Label85F1()
        {
            ldxm(0x0300);//85F1
            ldy(0x00);//85F4
            ldam(0x0753);//85F6
            if (z)
                goto Label85FD;//85F9
            ldy(0x04);//85FB
            Label85FD:
            ldam(0x0756);//85FD
            cmp(a, 0x02);//8600
            if (!z)
                goto Label8606;//8602
            ldy(0x08);//8604
            Label8606:
            lda(0x03);//8606
            str(0x00, a);//8608
            Label860A:
            ldam(0x85D7 + y);//860A
            str(0x0304 + x, a);//860D
            iny();//8610
            inx();//8611
            dec(0x00);//8612
            if (!n)
                goto Label860A;//8614
            ldxm(0x0300);//8616
            ldym(0x0744);//8619
            if (!z)
                goto Label8621;//861C
            ldym(0x074E);//861E
            Label8621:
            ldam(0x85CF + y);//8621
            str(0x0304 + x, a);//8624
            lda(0x3F);//8627
            str(0x0301 + x, a);//8629
            lda(0x10);//862C
            str(0x0302 + x, a);//862E
            lda(0x04);//8631
            str(0x0303 + x, a);//8633
            lda(0x00);//8636
            str(0x0308 + x, a);//8638
            txa();//863B
            c = false;//863C
            adc(0x07);//863D
            str(0x0300, a);//863F
        }

        void Label863F()
        {
            str(0x0300, a);//863F
        }

        void Label8643()
        {
            ldam(0x0733);//8643
            cmp(a, 0x01);//8646
            if (!z)
                goto Label864F;//8648
            lda(0x0B);//864A
            str(0x0773, a);//864C
            Label864F:
            Label8745(); //864F
            return;
        }

        void Label864C()
        {
            str(0x0773, a);//864C
            Label8745();
        }

        void Label864F()
        {
            Label8745(); //864F
            return;
        }

        void Label8652()
        {
            lda(0x00);//8652
            Label8808();//8654
            Label8745(); //8657
            return;
        }

        void Label865A()
        {
            LabelBC30();//865A
            ldxm(0x0300);//865D
            lda(0x20);//8660
            str(0x0301 + x, a);//8662
            lda(0x73);//8665
            str(0x0302 + x, a);//8667
            lda(0x03);//866A
            str(0x0303 + x, a);//866C
            ldym(0x075F);//866F
            iny();//8672
            tya();//8673
            str(0x0304 + x, a);//8674
            lda(0x28);//8677
            str(0x0305 + x, a);//8679
            ldym(0x075C);//867C
            iny();//867F
            tya();//8680
            str(0x0306 + x, a);//8681
            lda(0x00);//8684
            str(0x0307 + x, a);//8686
            txa();//8689
            c = false;//868A
            adc(0x06);//868B
            str(0x0300, a);//868D
            Label8745(); //8690
            return;
        }

        void Label8693()
        {
            ldam(0x0759);//8693
            if (z)
                goto Label86A2;//8696
            lda(0x00);//8698
            str(0x0759, a);//869A
            lda(0x02);//869D
            Label86C7(); //869F
            return;
            Label86A2:
            inc(0x073C);//86A2
            Label8745(); //86A5
            return;
        }

        void Label86A2()
        {
            inc(0x073C);//86A2
            Label8745(); //86A5
        }

        void Label86A8()
        {
            ldam(0x0770);//86A8
            if (z)
                goto Label86E0;//86AB
            cmp(a, 0x03);//86AD
            if (z)
                goto Label86D3;//86AF
            ldam(0x0752);//86B1
            if (!z)
                goto Label86E0;//86B4
            ldym(0x074E);//86B6
            cmp(y, 0x03);//86B9
            if (z)
                goto Label86C2;//86BB
            ldam(0x0769);//86BD
            if (!z)
                goto Label86E0;//86C0
            Label86C2:
            LabelEFA4();//86C2
            lda(0x01);//86C5
            Label8808();//86C7
            Label88A5();//86CA
            lda(0x00);//86CD
            str(0x0774, a);//86CF
            return;
            Label86D3:
            lda(0x12);//86D3
            str(0x07A0, a);//86D5
            lda(0x03);//86D8
            Label8808();//86DA
            Label874E(); //86DD
            return;
            Label86E0:
            lda(0x08);//86E0
            str(0x073C, a);//86E2
        }

        void Label86C7()
        {
            Label8808();//86C7
            Label88A5();//86CA
            lda(0x00);//86CD
            str(0x0774, a);//86CF
        }

        void Label86D3()
        {
            lda(0x12);//86D3
            str(0x07A0, a);//86D5
            lda(0x03);//86D8
            Label8808();//86DA
            Label874E(); //86DD
        }

        void Label86E0()
        {
            lda(0x08);//86E0
            str(0x073C, a);//86E2
        }

        void Label86E6()
        {
            inc(0x0774);//86E6
            Label86E9:
            Label92B0();//86E9
            ldam(0x071F);//86EC
            if (!z)
                goto Label86E9;//86EF
            dec(0x071E);//86F1
            if (!n)
                goto Label86F9;//86F4
            inc(0x073C);//86F6
            Label86F9:
            lda(0x06);//86F9
            str(0x0773, a);//86FB
        }

        void Label86FF()
        {
            ldam(0x0770);//86FF
            if (!z)
            {
                Label874E();//8702
                return;
            }
            lda(0x1E);//8704
            str(0x2006, a);//8706
            lda(0xC0);//8709
            str(0x2006, a);//870B
            lda(0x03);//870E
            str(0x01, a);//8710
            ldy(0x00);//8712
            str(0x00, y);//8714
            a = vram[0x0000];//8716
            Label8719:
            ldam(0x2007);//8719
            str(W(0x00) + y, a);//871C
            iny();//871E
            if (!z)
                goto Label8723;//871F
            inc(0x01);//8721
            Label8723:
            ldam(0x01);//8723
            cmp(a, 0x04);//8725
            if (!z)
                goto Label8719;//8727
            cmp(y, 0x3A);//8729
            if (!c)
                goto Label8719;//872B
            lda(0x05);//872D
            Label864C(); //872F
            return;
        }

        void Label8732()
        {
            ldam(0x0770);//8732
            if (!z)
            {
                Label874E();//8735
                return;
            }
            ldx(0x00);//8737
            Label8739:
            str(0x0300 + x, a);//8739
            str(0x0400 + x, a);//873C
            dex();//873F
            if (!z)
                goto Label8739;//8740
            Label8325();//8742
            inc(0x073C);//8745
        }

        void Label8745()
        {
            inc(0x073C);//8745
        }

        void Label8749()
        {
            lda(0xFA);//8749
            LabelBC36();//874B
            inc(0x0772);//874E
        }

        void Label874E()
        {
            inc(0x0772);//874E
        }

        void Label8808()
        {
            pha();//8808
            asl();//8809
            tay();//880A
            cmp(y, 0x04);//880B
            if (!c)
                goto Label881B;//880D
            cmp(y, 0x08);//880F
            if (!c)
                goto Label8815;//8811
            ldy(0x08);//8813
            Label8815:
            ldam(0x077A);//8815
            if (!z)
                goto Label881B;//8818
            iny();//881A
            Label881B:
            ldxm(0x87FE + y);//881B
            ldy(0x00);//881E
            Label8820:
            ldam(0x8752 + x);//8820
            cmp(a, 0xFF);//8823
            if (z)
                goto Label882E;//8825
            str(0x0301 + y, a);//8827
            inx();//882A
            iny();//882B
            if (!z)
                goto Label8820;//882C
            Label882E:
            lda(0x00);//882E
            str(0x0301 + y, a);//8830
            pla();//8833
            tax();//8834
            cmp(a, 0x04);//8835
            if (c)
                goto Label8882;//8837
            dex();//8839
            if (!z)
                goto Label885F;//883A
            ldam(0x075A);//883C
            c = false;//883F
            adc(0x01);//8840
            cmp(a, 0x0A);//8842
            if (!c)
                goto Label884D;//8844
            sbc(0x0A);//8846
            ldy(0x9F);//8848
            str(0x0308, y);//884A
            Label884D:
            str(0x0309, a);//884D
            ldym(0x075F);//8850
            iny();//8853
            str(0x0314, y);//8854
            ldym(0x075C);//8857
            iny();//885A
            str(0x0316, y);//885B
            return;
            Label885F:
            ldam(0x077A);//885F
            if (z)
                return;//8862
            ldam(0x0753);//8864
            dex();//8867
            if (!z)
                goto Label8873;//8868
            ldym(0x0770);//886A
            cmp(y, 0x03);//886D
            if (z)
                goto Label8873;//886F
            eor(0x01);//8871
            Label8873:
            lsr();//8873
            if (!c)
                return;//8874
            ldy(0x04);//8876
            Label8878:
            ldam(0x87ED + y);//8878
            str(0x0304 + y, a);//887B
            dey();//887E
            if (!n)
                goto Label8878;//887F
            return;
            Label8882:
            sbc(0x04);//8882
            asl();//8884
            asl();//8885
            tax();//8886
            ldy(0x00);//8887
            Label8889:
            ldam(0x87F2 + x);//8889
            str(0x031C + y, a);//888C
            inx();//888F
            iny();//8890
            iny();//8891
            iny();//8892
            iny();//8893
            cmp(y, 0x0C);//8894
            if (!c)
                goto Label8889;//8896
            lda(0x2C);//8898
            Label863F(); //889A
            return;
        }

        void Label889D()
        {
            ldam(0x07A0);//889D
            if (!z)
                return;//88A0
            Label8220();//88A2
            lda(0x07);//88A5
            str(0x07A0, a);//88A7
            inc(0x073C);//88AA
        }

        void Label88A5()
        {
            lda(0x07);//88A5
            str(0x07A0, a);//88A7
            inc(0x073C);//88AA
        }

        void Label88AE()
        {
            ldam(0x0726);//88AE
            and(0x01);//88B1
            str(0x05, a);//88B3
            ldym(0x0340);//88B5
            str(0x00, y);//88B8
            ldam(0x0721);//88BA
            str(0x0342 + y, a);//88BD
            ldam(0x0720);//88C0
            str(0x0341 + y, a);//88C3
            lda(0x9A);//88C6
            str(0x0343 + y, a);//88C8
            lda(0x00);//88CB
            str(0x04, a);//88CD
            tax();//88CF
            Label88D0:
            str(0x01, x);//88D0
            ldam(0x06A1 + x);//88D2
            and(0xC0);//88D5
            str(0x03, a);//88D7
            asl();//88D9
            rol();//88DA
            rol();//88DB
            tay();//88DC
            ldam(0x8B08 + y);//88DD
            str(0x06, a);//88E0
            ldam(0x8B0C + y);//88E2
            str(0x07, a);//88E5
            ldam(0x06A1 + x);//88E7
            asl();//88EA
            asl();//88EB
            str(0x02, a);//88EC
            ldam(0x071F);//88EE
            and(0x01);//88F1
            eor(0x01);//88F3
            asl();//88F5
            adc(ram[0x02]);//88F6
            tay();//88F8
            ldxm(0x00);//88F9
            ldam(W(0x06) + y);//88FB
            str(0x0344 + x, a);//88FD
            iny();//8900
            ldam(W(0x06) + y);//8901
            str(0x0345 + x, a);//8903
            ldym(0x04);//8906
            ldam(0x05);//8908
            if (!z)
                goto Label891A;//890A
            ldam(0x01);//890C
            lsr();//890E
            if (c)
                goto Label892A;//890F
            rolm(0x03);//8911
            rolm(0x03);//8913
            rolm(0x03);//8915
            Label8930(); //8917
            return;
            Label891A:
            ldam(0x01);//891A
            lsr();//891C
            if (c)
                goto Label892E;//891D
            lsrm(0x03);//891F
            lsrm(0x03);//8921
            lsrm(0x03);//8923
            lsrm(0x03);//8925
            Label8930(); //8927
            return;
            Label892A:
            lsrm(0x03);//892A
            lsrm(0x03);//892C
            Label892E:
            inc(0x04);//892E
            ldam(0x03F9 + y);//8930
            orm(0x03);//8933
            str(0x03F9 + y, a);//8935
            inc(0x00);//8938
            inc(0x00);//893A
            ldxm(0x01);//893C
            inx();//893E
            cmp(x, 0x0D);//893F
            if (!c)
                goto Label88D0;//8941
            ldym(0x00);//8943
            iny();//8945
            iny();//8946
            iny();//8947
            lda(0x00);//8948
            str(0x0341 + y, a);//894A
            str(0x0340, y);//894D
            inc(0x0721);//8950
            ldam(0x0721);//8953
            and(0x1F);//8956
            if (!z)
                goto Label8967;//8958
            lda(0x80);//895A
            str(0x0721, a);//895C
            ldam(0x0720);//895F
            eor(0x04);//8962
            str(0x0720, a);//8964
            Label8967:
            Label89BD(); //8967
            return;
        }

        void Label88D0()
        {
            Label88D0:
            str(0x01, x);//88D0
            ldam(0x06A1 + x);//88D2
            and(0xC0);//88D5
            str(0x03, a);//88D7
            asl();//88D9
            rol();//88DA
            rol();//88DB
            tay();//88DC
            ldam(0x8B08 + y);//88DD
            str(0x06, a);//88E0
            ldam(0x8B0C + y);//88E2
            str(0x07, a);//88E5
            ldam(0x06A1 + x);//88E7
            asl();//88EA
            asl();//88EB
            str(0x02, a);//88EC
            ldam(0x071F);//88EE
            and(0x01);//88F1
            eor(0x01);//88F3
            asl();//88F5
            adc(ram[0x02]);//88F6
            tay();//88F8
            ldxm(0x00);//88F9
            ldam(W(0x06) + y);//88FB
            str(0x0344 + x, a);//88FD
            iny();//8900
            ldam(W(0x06) + y);//8901
            str(0x0345 + x, a);//8903
            ldym(0x04);//8906
            ldam(0x05);//8908
            if (!z)
                goto Label891A;//890A
            ldam(0x01);//890C
            lsr();//890E
            if (c)
                goto Label892A;//890F
            rolm(0x03);//8911
            rolm(0x03);//8913
            rolm(0x03);//8915
            Label8930(); //8917
            return;
            Label891A:
            ldam(0x01);//891A
            lsr();//891C
            if (c)
                goto Label892E;//891D
            lsrm(0x03);//891F
            lsrm(0x03);//8921
            lsrm(0x03);//8923
            lsrm(0x03);//8925
            Label8930(); //8927
            return;
            Label892A:
            lsrm(0x03);//892A
            lsrm(0x03);//892C
            Label892E:
            inc(0x04);//892E
            ldam(0x03F9 + y);//8930
            orm(0x03);//8933
            str(0x03F9 + y, a);//8935
            inc(0x00);//8938
            inc(0x00);//893A
            ldxm(0x01);//893C
            inx();//893E
            cmp(x, 0x0D);//893F
            if (!c)
                goto Label88D0;//8941
            ldym(0x00);//8943
            iny();//8945
            iny();//8946
            iny();//8947
            lda(0x00);//8948
            str(0x0341 + y, a);//894A
            str(0x0340, y);//894D
            inc(0x0721);//8950
            ldam(0x0721);//8953
            and(0x1F);//8956
            if (!z)
                goto Label8967;//8958
            lda(0x80);//895A
            str(0x0721, a);//895C
            ldam(0x0720);//895F
            eor(0x04);//8962
            str(0x0720, a);//8964
            Label8967:
            Label89BD(); //8967
            return;
        }

        void Label8930()
        {
            ldam(0x03F9 + y);//8930
            orm(0x03);//8933
            str(0x03F9 + y, a);//8935
            inc(0x00);//8938
            inc(0x00);//893A
            ldxm(0x01);//893C
            inx();//893E
            cmp(x, 0x0D);//893F
            if (!c)
            {
                Label88D0();//8941
                return;
            }
            ldym(0x00);//8943
            iny();//8945
            iny();//8946
            iny();//8947
            lda(0x00);//8948
            str(0x0341 + y, a);//894A
            str(0x0340, y);//894D
            inc(0x0721);//8950
            ldam(0x0721);//8953
            and(0x1F);//8956
            if (!z)
                goto Label8967;//8958
            lda(0x80);//895A
            str(0x0721, a);//895C
            ldam(0x0720);//895F
            eor(0x04);//8962
            str(0x0720, a);//8964
            Label8967:
            Label89BD(); //8967
            return;
        }

        void Label896A()
        {
            ldam(0x0721);//896A
            and(0x1F);//896D
            c = true;//896F
            sbc(0x04);//8970
            and(0x1F);//8972
            str(0x01, a);//8974
            ldam(0x0720);//8976
            if (c)
                goto Label897D;//8979
            eor(0x04);//897B
            Label897D:
            and(0x04);//897D
            or(0x23);//897F
            str(0x00, a);//8981
            ldam(0x01);//8983
            lsr();//8985
            lsr();//8986
            adc(0xC0);//8987
            str(0x01, a);//8989
            ldx(0x00);//898B
            ldym(0x0340);//898D
            Label8990:
            ldam(0x00);//8990
            str(0x0341 + y, a);//8992
            ldam(0x01);//8995
            c = false;//8997
            adc(0x08);//8998
            str(0x0342 + y, a);//899A
            str(0x01, a);//899D
            ldam(0x03F9 + x);//899F
            str(0x0344 + y, a);//89A2
            lda(0x01);//89A5
            str(0x0343 + y, a);//89A7
            lsr();//89AA
            str(0x03F9 + x, a);//89AB
            iny();//89AE
            iny();//89AF
            iny();//89B0
            iny();//89B1
            inx();//89B2
            cmp(x, 0x07);//89B3
            if (!c)
                goto Label8990;//89B5
            str(0x0341 + y, a);//89B7
            str(0x0340, y);//89BA
            lda(0x06);//89BD
            str(0x0773, a);//89BF
        }

        void Label89BD()
        {
            lda(0x06);//89BD
            str(0x0773, a);//89BF
        }

        void Label89E1()
        {
            ldam(0x09);//89E1
            and(0x07);//89E3
            if (!z)
                return;//89E5
            ldxm(0x0300);//89E7
            cmp(x, 0x31);//89EA
            if (c)
                return;//89EC
            tay();//89EE
            Label89EF:
            ldam(0x89C9 + y);//89EF
            str(0x0301 + x, a);//89F2
            inx();//89F5
            iny();//89F6
            cmp(y, 0x08);//89F7
            if (!c)
                goto Label89EF;//89F9
            ldxm(0x0300);//89FB
            lda(0x03);//89FE
            str(0x00, a);//8A00
            ldam(0x074E);//8A02
            asl();//8A05
            asl();//8A06
            tay();//8A07
            Label8A08:
            ldam(0x89D1 + y);//8A08
            str(0x0304 + x, a);//8A0B
            iny();//8A0E
            inx();//8A0F
            dec(0x00);//8A10
            if (!n)
                goto Label8A08;//8A12
            ldxm(0x0300);//8A14
            ldym(0x06D4);//8A17
            ldam(0x89C3 + y);//8A1A
            str(0x0305 + x, a);//8A1D
            ldam(0x0300);//8A20
            c = false;//8A23
            adc(0x07);//8A24
            str(0x0300, a);//8A26
            inc(0x06D4);//8A29
            ldam(0x06D4);//8A2C
            cmp(a, 0x06);//8A2F
            if (!c)
                return;//8A31
            lda(0x00);//8A33
            str(0x06D4, a);//8A35
        }

        void Label8A4D()
        {
            ldy(0x41);//8A4D
            lda(0x03);//8A4F
            ldxm(0x074E);//8A51
            if (!z)
                goto Label8A58;//8A54
            lda(0x04);//8A56
            Label8A58:
            Label8A97();//8A58
            lda(0x06);//8A5B
            str(0x0773, a);//8A5D
        }

        void Label8A61()
        {
            Label8A6D();//8A61
            inc(0x03F0);//8A64
            dec(0x03EC + x);//8A67
        }

        void Label8A6B()
        {
            lda(0x00);//8A6B
            ldy(0x03);//8A6D
            cmp(a, 0x00);//8A6F
            if (z)
                goto Label8A87;//8A71
            ldy(0x00);//8A73
            cmp(a, 0x58);//8A75
            if (z)
                goto Label8A87;//8A77
            cmp(a, 0x51);//8A79
            if (z)
                goto Label8A87;//8A7B
            iny();//8A7D
            cmp(a, 0x5D);//8A7E
            if (z)
                goto Label8A87;//8A80
            cmp(a, 0x52);//8A82
            if (z)
                goto Label8A87;//8A84
            iny();//8A86
            Label8A87:
            tya();//8A87
            ldym(0x0300);//8A88
            iny();//8A8B
            Label8A97();//8A8C
            dey();//8A8F
            tya();//8A90
            c = false;//8A91
            adc(0x0A);//8A92
            Label863F(); //8A94
            return;
        }

        void Label8A6D()
        {
            ldy(0x03);//8A6D
            cmp(a, 0x00);//8A6F
            if (z)
                goto Label8A87;//8A71
            ldy(0x00);//8A73
            cmp(a, 0x58);//8A75
            if (z)
                goto Label8A87;//8A77
            cmp(a, 0x51);//8A79
            if (z)
                goto Label8A87;//8A7B
            iny();//8A7D
            cmp(a, 0x5D);//8A7E
            if (z)
                goto Label8A87;//8A80
            cmp(a, 0x52);//8A82
            if (z)
                goto Label8A87;//8A84
            iny();//8A86
            Label8A87:
            tya();//8A87
            ldym(0x0300);//8A88
            iny();//8A8B
            Label8A97();//8A8C
            dey();//8A8F
            tya();//8A90
            c = false;//8A91
            adc(0x0A);//8A92
            Label863F(); //8A94
            return;
        }

        void Label8A8F()
        {
            dey();//8A8F
            tya();//8A90
            c = false;//8A91
            adc(0x0A);//8A92
            Label863F(); //8A94
            return;
        }

        void Label8A97()
        {
            str(0x00, x);//8A97
            str(0x01, y);//8A99
            asl();//8A9B
            asl();//8A9C
            tax();//8A9D
            ldy(0x20);//8A9E
            ldam(0x06);//8AA0
            cmp(a, 0xD0);//8AA2
            if (!c)
                goto Label8AA8;//8AA4
            ldy(0x24);//8AA6
            Label8AA8:
            str(0x03, y);//8AA8
            and(0x0F);//8AAA
            asl();//8AAC
            str(0x04, a);//8AAD
            lda(0x00);//8AAF
            str(0x05, a);//8AB1
            ldam(0x02);//8AB3
            c = false;//8AB5
            adc(0x20);//8AB6
            asl();//8AB8
            rolm(0x05);//8AB9
            asl();//8ABB
            rolm(0x05);//8ABC
            adc(ram[0x04]);//8ABE
            str(0x04, a);//8AC0
            ldam(0x05);//8AC2
            adc(0x00);//8AC4
            c = false;//8AC6
            adc(ram[0x03]);//8AC7
            str(0x05, a);//8AC9
            ldym(0x01);//8ACB
            ldam(0x8A39 + x);//8ACD
            str(0x0303 + y, a);//8AD0
            ldam(0x8A3A + x);//8AD3
            str(0x0304 + y, a);//8AD6
            ldam(0x8A3B + x);//8AD9
            str(0x0308 + y, a);//8ADC
            ldam(0x8A3C + x);//8ADF
            str(0x0309 + y, a);//8AE2
            ldam(0x04);//8AE5
            str(0x0301 + y, a);//8AE7
            c = false;//8AEA
            adc(0x20);//8AEB
            str(0x0306 + y, a);//8AED
            ldam(0x05);//8AF0
            str(0x0300 + y, a);//8AF2
            str(0x0305 + y, a);//8AF5
            lda(0x02);//8AF8
            str(0x0302 + y, a);//8AFA
            str(0x0307 + y, a);//8AFD
            lda(0x00);//8B00
            str(0x030A + y, a);//8B02
            ldxm(0x00);//8B05
        }

        void Label8ACD()
        {
            ldam(0x8A39 + x);//8ACD
            str(0x0303 + y, a);//8AD0
            ldam(0x8A3A + x);//8AD3
            str(0x0304 + y, a);//8AD6
            ldam(0x8A3B + x);//8AD9
            str(0x0308 + y, a);//8ADC
            ldam(0x8A3C + x);//8ADF
            str(0x0309 + y, a);//8AE2
            ldam(0x04);//8AE5
            str(0x0301 + y, a);//8AE7
            c = false;//8AEA
            adc(0x20);//8AEB
            str(0x0306 + y, a);//8AED
            ldam(0x05);//8AF0
            str(0x0300 + y, a);//8AF2
            str(0x0305 + y, a);//8AF5
            lda(0x02);//8AF8
            str(0x0302 + y, a);//8AFA
            str(0x0307 + y, a);//8AFD
            lda(0x00);//8B00
            str(0x030A + y, a);//8B02
            ldxm(0x00);//8B05
        }

        void Label8E19()
        {
            ldam(0x2002);//8E19
            ldam(0x0778);//8E1C
            or(0x10);//8E1F
            and(0xF0);//8E21
            Label8EED();//8E23
            lda(0x24);//8E26
            Label8E2D();//8E28
            lda(0x20);//8E2B
            str(0x2006, a);//8E2D
            lda(0x00);//8E30
            str(0x2006, a);//8E32
            ldx(0x04);//8E35
            ldy(0xC0);//8E37
            lda(0x24);//8E39
            Label8E3B:
            str(0x2007, a);//8E3B
            dey();//8E3E
            if (!z)
                goto Label8E3B;//8E3F
            dex();//8E41
            if (!z)
                goto Label8E3B;//8E42
            ldy(0x40);//8E44
            txa();//8E46
            str(0x0300, a);//8E47
            str(0x0301, a);//8E4A
            Label8E4D:
            str(0x2007, a);//8E4D
            dey();//8E50
            if (!z)
                goto Label8E4D;//8E51
            str(0x073F, a);//8E53
            str(0x0740, a);//8E56
            Label8EE6(); //8E59
            return;
        }

        void Label8E2D()
        {
            str(0x2006, a);//8E2D
            lda(0x00);//8E30
            str(0x2006, a);//8E32
            ldx(0x04);//8E35
            ldy(0xC0);//8E37
            lda(0x24);//8E39
            Label8E3B:
            str(0x2007, a);//8E3B
            dey();//8E3E
            if (!z)
                goto Label8E3B;//8E3F
            dex();//8E41
            if (!z)
                goto Label8E3B;//8E42
            ldy(0x40);//8E44
            txa();//8E46
            str(0x0300, a);//8E47
            str(0x0301, a);//8E4A
            Label8E4D:
            str(0x2007, a);//8E4D
            dey();//8E50
            if (!z)
                goto Label8E4D;//8E51
            str(0x073F, a);//8E53
            str(0x0740, a);//8E56
            Label8EE6(); //8E59
            return;
        }

        void Label8E5C()
        {
            lda(0x01);//8E5C
            str(0x4016, a);//8E5E
            lsr();//8E61
            tax();//8E62
            str(0x4016, a);//8E63
            Label8E6A();//8E66
            inx();//8E69
            Label8E6A();
        }

        void Label8E6A()
        {
            ldy(0x08);//8E6A
            Label8E6C:
            pha();//8E6C
            ldam(0x4016 + x);//8E6D
            str(0x00, a);//8E70
            lsr();//8E72
            orm(0x00);//8E73
            lsr();//8E75
            pla();//8E76
            rol();//8E77
            dey();//8E78
            if (!z)
                goto Label8E6C;//8E79
            str(0x06FC + x, a);//8E7B
            pha();//8E7E
            and(0x30);//8E7F
            and(ram[0x074A + x]);//8E81
            if (z)
                goto Label8E8D;//8E84
            pla();//8E86
            and(0xCF);//8E87
            str(0x06FC + x, a);//8E89
            return;
            Label8E8D:
            pla();//8E8D
            str(0x074A + x, a);//8E8E
        }

        void Label8E92()
        {
            Label8E92:
            str(0x2006, a);//8E92
            iny();//8E95
            ldam(W(0x00) + y);//8E96
            str(0x2006, a);//8E98
            iny();//8E9B
            ldam(W(0x00) + y);//8E9C
            asl();//8E9E
            pha();//8E9F
            ldam(0x0778);//8EA0
            or(0x04);//8EA3
            if (c)
                goto Label8EA9;//8EA5
            and(0xFB);//8EA7
            Label8EA9:
            Label8EED();//8EA9
            pla();//8EAC
            asl();//8EAD
            if (!c)
                goto Label8EB3;//8EAE
            or(0x02);//8EB0
            iny();//8EB2
            Label8EB3:
            lsr();//8EB3
            lsr();//8EB4
            tax();//8EB5
            Label8EB6:
            if (c)
                goto Label8EB9;//8EB6
            iny();//8EB8
            Label8EB9:
            ldam(W(0x00) + y);//8EB9
            str(0x2007, a);//8EBB
            dex();//8EBE
            if (!z)
                goto Label8EB6;//8EBF
            c = true;//8EC1
            tya();//8EC2
            adc(ram[0x00]);//8EC3
            str(0x00, a);//8EC5
            lda(0x00);//8EC7
            adc(ram[0x01]);//8EC9
            str(0x01, a);//8ECB
            lda(0x3F);//8ECD
            str(0x2006, a);//8ECF
            lda(0x00);//8ED2
            str(0x2006, a);//8ED4
            str(0x2006, a);//8ED7
            str(0x2006, a);//8EDA
            ldxm(0x2002);//8EDD
            ldy(0x00);//8EE0
            ldam(W(0x00) + y);//8EE2
            if (!z)
                goto Label8E92;//8EE4
            str(0x2005, a);//8EE6
            str(0x2005, a);//8EE9
        }

        void Label8EDD()
        {
            ldxm(0x2002);//8EDD
            ldy(0x00);//8EE0
            ldam(W(0x00) + y);//8EE2
            if (!z)
                Label8E92();//8EE4
        }

        void Label8EE6()
        {
            str(0x2005, a);//8EE6
            str(0x2005, a);//8EE9
        }

        void Label8EED()
        {
            str(0x2000, a);//8EED
            str(0x0778, a);//8EF0
        }

        void Label8F06()
        {
            str(0x00, a);//8F06
            Label8F11();//8F08
            ldam(0x00);//8F0B
            lsr();//8F0D
            lsr();//8F0E
            lsr();//8F0F
            lsr();//8F10
            c = false;//8F11
            adc(0x01);//8F12
            and(0x0F);//8F14
            cmp(a, 0x06);//8F16
            if (c)
                return;//8F18
            pha();//8F1A
            asl();//8F1B
            tay();//8F1C
            ldxm(0x0300);//8F1D
            lda(0x20);//8F20
            cmp(y, 0x00);//8F22
            if (!z)
                goto Label8F28;//8F24
            lda(0x22);//8F26
            Label8F28:
            str(0x0301 + x, a);//8F28
            ldam(0x8EF4 + y);//8F2B
            str(0x0302 + x, a);//8F2E
            ldam(0x8EF5 + y);//8F31
            str(0x0303 + x, a);//8F34
            str(0x03, a);//8F37
            str(0x02, x);//8F39
            pla();//8F3B
            tax();//8F3C
            ldam(0x8F00 + x);//8F3D
            c = true;//8F40
            sbc(ram[0x8EF5 + y]);//8F41
            tay();//8F44
            ldxm(0x02);//8F45
            Label8F47:
            ldam(0x07D7 + y);//8F47
            str(0x0304 + x, a);//8F4A
            inx();//8F4D
            iny();//8F4E
            dec(0x03);//8F4F
            if (!z)
                goto Label8F47;//8F51
            lda(0x00);//8F53
            str(0x0304 + x, a);//8F55
            inx();//8F58
            inx();//8F59
            inx();//8F5A
            str(0x0300, x);//8F5B
        }

        void Label8F11()
        {
            c = false;//8F11
            adc(0x01);//8F12
            and(0x0F);//8F14
            cmp(a, 0x06);//8F16
            if (c)
                return;//8F18
            pha();//8F1A
            asl();//8F1B
            tay();//8F1C
            ldxm(0x0300);//8F1D
            lda(0x20);//8F20
            cmp(y, 0x00);//8F22
            if (!z)
                goto Label8F28;//8F24
            lda(0x22);//8F26
            Label8F28:
            str(0x0301 + x, a);//8F28
            ldam(0x8EF4 + y);//8F2B
            str(0x0302 + x, a);//8F2E
            ldam(0x8EF5 + y);//8F31
            str(0x0303 + x, a);//8F34
            str(0x03, a);//8F37
            str(0x02, x);//8F39
            pla();//8F3B
            tax();//8F3C
            ldam(0x8F00 + x);//8F3D
            c = true;//8F40
            sbc(ram[0x8EF5 + y]);//8F41
            tay();//8F44
            ldxm(0x02);//8F45
            Label8F47:
            ldam(0x07D7 + y);//8F47
            str(0x0304 + x, a);//8F4A
            inx();//8F4D
            iny();//8F4E
            dec(0x03);//8F4F
            if (!z)
                goto Label8F47;//8F51
            lda(0x00);//8F53
            str(0x0304 + x, a);//8F55
            inx();//8F58
            inx();//8F59
            inx();//8F5A
            str(0x0300, x);//8F5B
        }

        void Label8F5F()
        {
            ldam(0x0770);//8F5F
            cmp(a, 0x00);//8F62
            if (z)
                goto Label8F7C;//8F64
            ldx(0x05);//8F66
            Label8F68:
            ldam(0x0134 + x);//8F68
            c = false;//8F6B
            adc(ram[0x07D7 + y]);//8F6C
            if (n) //
                goto Label8F87;//8F6F
            cmp(a, 0x0A);//8F71
            if (c)
                goto Label8F8E;//8F73
            Label8F75:
            str(0x07D7 + y, a);//8F75
            dey();//8F78
            dex();//8F79
            if (!n)
                goto Label8F68;//8F7A
            Label8F7C:
            lda(0x00);//8F7C
            ldx(0x06);//8F7E
            Label8F80:
            str(0x0133 + x, a);//8F80
            dex();//8F83
            if (!n)
                goto Label8F80;//8F84
            return;
            Label8F87:
            dec(0x0133 + x);//8F87
            lda(0x09);//8F8A
            if (!z)
                goto Label8F75;//8F8C
            Label8F8E:
            c = true;//8F8E
            sbc(0x0A);//8F8F
            inc(0x0133 + x);//8F91
            goto Label8F75; //8F94
        }

        void Label8F68()
        {
            ldam(0x0134 + x);//8F68
            c = false;//8F6B
            adc(ram[0x07D7 + y]);//8F6C
            if (n) //
                Label8F87();//8F6F
        }

        void Label8F75()
        {
            str(0x07D7 + y, a);//8F75
            dey();//8F78
            dex();//8F79
            if (!n)
            {
                Label8F68();//8F7A
                return;
            }
            Label8F7C:
            lda(0x00);//8F7C
            ldx(0x06);//8F7E
            Label8F80:
            str(0x0133 + x, a);//8F80
            dex();//8F83
            if (!n)
                goto Label8F80;//8F84
        }

        void Label8F87()
        {
            dec(0x0133 + x);//8F87
            lda(0x09);//8F8A
            if (!z)
            {
                Label8F75();//8F8C
                return;
            }
            c = true;//8F8E
            sbc(0x0A);//8F8F
            inc(0x0133 + x);//8F91
            Label8F75(); //8F94}
        }

        void Label8F97()
        {
            ldx(0x05);//8F97
            Label8F9E();//8F99
            ldx(0x0B);//8F9C
            ldy(0x05);//8F9E
            c = true;//8FA0
            Label8FA1:
            ldam(0x07DD + x);//8FA1
            sbc(ram[0x07D7 + y]);//8FA4
            dex();//8FA7
            dey();//8FA8
            if (!n)
                goto Label8FA1;//8FA9
            if (!c)
                return;//8FAB
            inx();//8FAD
            iny();//8FAE
            Label8FAF:
            ldam(0x07DD + x);//8FAF
            str(0x07D7 + y, a);//8FB2
            inx();//8FB5
            iny();//8FB6
            cmp(y, 0x06);//8FB7
            if (!c)
                goto Label8FAF;//8FB9
        }

        void Label8F9E()
        {
            ldy(0x05);//8F9E
            c = true;//8FA0
            Label8FA1:
            ldam(0x07DD + x);//8FA1
            sbc(ram[0x07D7 + y]);//8FA4
            dex();//8FA7
            dey();//8FA8
            if (!n)
                goto Label8FA1;//8FA9
            if (!c)
                return;//8FAB
            inx();//8FAD
            iny();//8FAE
            Label8FAF:
            ldam(0x07DD + x);//8FAF
            str(0x07D7 + y, a);//8FB2
            inx();//8FB5
            iny();//8FB6
            cmp(y, 0x06);//8FB7
            if (!c)
                goto Label8FAF;//8FB9
        }

        void Label8FCF()
        {
            ldy(0x6F);//8FCF
            Label90CC();//8FD1
            ldy(0x1F);//8FD4
            Label8FD6:
            str(0x07B0 + y, a);//8FD6
            dey();//8FD9
            if (!n)
                goto Label8FD6;//8FDA
            lda(0x18);//8FDC
            str(0x07A2, a);//8FDE
            Label9C03();//8FE1
            ldy(0x4B);//8FE4
            Label90CC();//8FE6
            ldx(0x21);//8FE9
            lda(0x00);//8FEB
            Label8FED:
            str(0x0780 + x, a);//8FED
            dex();//8FF0
            if (!n)
                goto Label8FED;//8FF1
            ldam(0x075B);//8FF3
            ldym(0x0752);//8FF6
            if (z)
                goto Label8FFE;//8FF9
            ldam(0x0751);//8FFB
            Label8FFE:
            str(0x071A, a);//8FFE
            str(0x0725, a);//9001
            str(0x0728, a);//9004
            LabelB038();//9007
            ldy(0x20);//900A
            and(0x01);//900C
            if (z)
                goto Label9012;//900E
            ldy(0x24);//9010
            Label9012:
            str(0x0720, y);//9012
            ldy(0x80);//9015
            str(0x0721, y);//9017
            asl();//901A
            asl();//901B
            asl();//901C
            asl();//901D
            str(0x06A0, a);//901E
            dec(0x0730);//9021
            dec(0x0731);//9024
            dec(0x0732);//9027
            lda(0x0B);//902A
            str(0x071E, a);//902C
            Label9C22();//902F
            ldam(0x076A);//9032
            if (!z)
                goto Label9047;//9035
            ldam(0x075F);//9037
            cmp(a, 0x04);//903A
            if (!c)
                goto Label904A;//903C
            if (!z)
                goto Label9047;//903E
            ldam(0x075C);//9040
            cmp(a, 0x02);//9043
            if (!c)
                goto Label904A;//9045
            Label9047:
            inc(0x06CC);//9047
            Label904A:
            ldam(0x075B);//904A
            if (z)
                goto Label9054;//904D
            lda(0x02);//904F
            str(0x0710, a);//9051
            Label9054:
            lda(0x80);//9054
            str(0xFB, a);//9056
            lda(0x01);//9058
            str(0x0774, a);//905A
            inc(0x0772);//905D
        }

        void Label8FE4()
        {
            ldy(0x4B);//8FE4
            Label90CC();//8FE6
            ldx(0x21);//8FE9
            lda(0x00);//8FEB
            Label8FED:
            str(0x0780 + x, a);//8FED
            dex();//8FF0
            if (!n)
                goto Label8FED;//8FF1
            ldam(0x075B);//8FF3
            ldym(0x0752);//8FF6
            if (z)
                goto Label8FFE;//8FF9
            ldam(0x0751);//8FFB
            Label8FFE:
            str(0x071A, a);//8FFE
            str(0x0725, a);//9001
            str(0x0728, a);//9004
            LabelB038();//9007
            ldy(0x20);//900A
            and(0x01);//900C
            if (z)
                goto Label9012;//900E
            ldy(0x24);//9010
            Label9012:
            str(0x0720, y);//9012
            ldy(0x80);//9015
            str(0x0721, y);//9017
            asl();//901A
            asl();//901B
            asl();//901C
            asl();//901D
            str(0x06A0, a);//901E
            dec(0x0730);//9021
            dec(0x0731);//9024
            dec(0x0732);//9027
            lda(0x0B);//902A
            str(0x071E, a);//902C
            Label9C22();//902F
            ldam(0x076A);//9032
            if (!z)
                goto Label9047;//9035
            ldam(0x075F);//9037
            cmp(a, 0x04);//903A
            if (!c)
                goto Label904A;//903C
            if (!z)
                goto Label9047;//903E
            ldam(0x075C);//9040
            cmp(a, 0x02);//9043
            if (!c)
                goto Label904A;//9045
            Label9047:
            inc(0x06CC);//9047
            Label904A:
            ldam(0x075B);//904A
            if (z)
                goto Label9054;//904D
            lda(0x02);//904F
            str(0x0710, a);//9051
            Label9054:
            lda(0x80);//9054
            str(0xFB, a);//9056
            lda(0x01);//9058
            str(0x0774, a);//905A
            inc(0x0772);//905D
        }

        void Label9061()
        {
            lda(0x01);//9061
            str(0x0757, a);//9063
            str(0x0754, a);//9066
            lda(0x02);//9069
            str(0x075A, a);//906B
            str(0x0761, a);//906E
            lda(0x00);//9071
            str(0x0774, a);//9073
            tay();//9076
            Label9077:
            str(0x0300 + y, a);//9077
            iny();//907A
            if (!z)
                goto Label9077;//907B
            str(0x0759, a);//907D
            str(0x0769, a);//9080
            str(0x0728, a);//9083
            lda(0xFF);//9086
            str(0x03A0, a);//9088
            ldam(0x071A);//908B
            lsrm(0x0778);//908E
            and(0x01);//9091
            ror();//9093
            rolm(0x0778);//9094
            Label90ED();//9097
            lda(0x38);//909A
            str(0x06E3, a);//909C
            lda(0x48);//909F
            str(0x06E2, a);//90A1
            lda(0x58);//90A4
            str(0x06E1, a);//90A6
            ldx(0x0E);//90A9
            Label90AB:
            ldam(0x8FBC + x);//90AB
            str(0x06E4 + x, a);//90AE
            dex();//90B1
            if (!n)
                goto Label90AB;//90B2
            ldy(0x03);//90B4
            Label90B6:
            ldam(0x8FCB + y);//90B6
            str(0x0200 + y, a);//90B9
            dey();//90BC
            if (!n)
                goto Label90B6;//90BD
            Label92AF();//90BF
            Label92AA();//90C2
            inc(0x0722);//90C5
            inc(0x0772);//90C8
        }

        void Label9071()
        {
            lda(0x00);//9071
            str(0x0774, a);//9073
            tay();//9076
            Label9077:
            str(0x0300 + y, a);//9077
            iny();//907A
            if (!z)
                goto Label9077;//907B
            str(0x0759, a);//907D
            str(0x0769, a);//9080
            str(0x0728, a);//9083
            lda(0xFF);//9086
            str(0x03A0, a);//9088
            ldam(0x071A);//908B
            lsrm(0x0778);//908E
            and(0x01);//9091
            ror();//9093
            rolm(0x0778);//9094
            Label90ED();//9097
            lda(0x38);//909A
            str(0x06E3, a);//909C
            lda(0x48);//909F
            str(0x06E2, a);//90A1
            lda(0x58);//90A4
            str(0x06E1, a);//90A6
            ldx(0x0E);//90A9
            Label90AB:
            ldam(0x8FBC + x);//90AB
            str(0x06E4 + x, a);//90AE
            dex();//90B1
            if (!n)
                goto Label90AB;//90B2
            ldy(0x03);//90B4
            Label90B6:
            ldam(0x8FCB + y);//90B6
            str(0x0200 + y, a);//90B9
            dey();//90BC
            if (!n)
                goto Label90B6;//90BD
            Label92AF();//90BF
            Label92AA();//90C2
            inc(0x0722);//90C5
            inc(0x0772);//90C8
        }

        void Label90CC()
        {
            ldx(0x07);//90CC
            lda(0x00);//90CE
            str(0x06, a);//90D0
            Label90D2:
            str(0x07, x);//90D2
            Label90D4:
            cmp(x, 0x01);//90D4
            if (!z)
                goto Label90DC;//90D6
            cmp(y, 0x60);//90D8
            if (c)
                goto Label90DE;//90DA
            Label90DC:
            str(W(0x06) + y, a);//90DC
            Label90DE:
            dey();//90DE
            cmp(y, 0xFF);//90DF
            if (!z)
                goto Label90D4;//90E1
            dex();//90E3
            if (!n)
                goto Label90D2;//90E4
        }

        void Label90ED()
        {
            ldam(0x0770);//90ED
            if (z)
                return;//90F0
            ldam(0x0752);//90F2
            cmp(a, 0x02);//90F5
            if (z)
                goto Label9106;//90F7
            ldy(0x05);//90F9
            ldam(0x0710);//90FB
            cmp(a, 0x06);//90FE
            if (z)
                goto Label9110;//9100
            cmp(a, 0x07);//9102
            if (z)
                goto Label9110;//9104
            Label9106:
            ldym(0x074E);//9106
            ldam(0x0743);//9109
            if (z)
                goto Label9110;//910C
            ldy(0x04);//910E
            Label9110:
            ldam(0x90E7 + y);//9110
            str(0xFB, a);//9113
        }

        void Label9131()
        {
            ldam(0x071A);//9131
            str(0x6D, a);//9134
            lda(0x28);//9136
            str(0x070A, a);//9138
            lda(0x01);//913B
            str(0x33, a);//913D
            str(0xB5, a);//913F
            lda(0x00);//9141
            str(0x1D, a);//9143
            dec(0x0490);//9145
            ldy(0x00);//9148
            str(0x075B, y);//914A
            ldam(0x074E);//914D
            if (!z)
                goto Label9153;//9150
            iny();//9152
            Label9153:
            str(0x0704, y);//9153
            ldxm(0x0710);//9156
            ldym(0x0752);//9159
            if (z)
                goto Label9165;//915C
            cmp(y, 0x01);//915E
            if (z)
                goto Label9165;//9160
            ldxm(0x9118 + y);//9162
            Label9165:
            ldam(0x9116 + y);//9165
            str(0x86, a);//9168
            ldam(0x911C + x);//916A
            str(0xCE, a);//916D
            ldam(0x9125 + x);//916F
            str(0x03C4, a);//9172
            Label85F1();//9175
            ldym(0x0715);//9178
            if (z)
                goto Label9197;//917B
            ldam(0x0757);//917D
            if (z)
                goto Label9197;//9180
            ldam(0x912D + y);//9182
            str(0x07F8, a);//9185
            lda(0x01);//9188
            str(0x07FA, a);//918A
            lsr();//918D
            str(0x07F9, a);//918E
            str(0x0757, a);//9191
            str(0x079F, a);//9194
            Label9197:
            ldym(0x0758);//9197
            if (z)
                goto Label91B0;//919A
            lda(0x03);//919C
            str(0x1D, a);//919E
            ldx(0x00);//91A0
            LabelBD84();//91A2
            lda(0xF0);//91A5
            str(0xD7, a);//91A7
            ldx(0x05);//91A9
            ldy(0x00);//91AB
            LabelB91E();//91AD
            Label91B0:
            ldym(0x074E);//91B0
            if (!z)
                goto Label91B8;//91B3
            LabelB70B();//91B5
            Label91B8:
            lda(0x07);//91B8
            str(0x0E, a);//91BA
        }

        void Label91CD()
        {
            inc(0x0774);//91CD
            lda(0x00);//91D0
            str(0x0722, a);//91D2
            lda(0x80);//91D5
            str(0xFC, a);//91D7
            dec(0x075A);//91D9
            if (!n)
                goto Label91E9;//91DC
            lda(0x00);//91DE
            str(0x0772, a);//91E0
            lda(0x03);//91E3
            str(0x0770, a);//91E5
            return;
            Label91E9:
            ldam(0x075F);//91E9
            asl();//91EC
            tax();//91ED
            ldam(0x075C);//91EE
            and(0x02);//91F1
            if (z)
                goto Label91F6;//91F3
            inx();//91F5
            Label91F6:
            ldym(0x91BD + x);//91F6
            ldam(0x075C);//91F9
            lsr();//91FC
            tya();//91FD
            if (c)
                goto Label9204;//91FE
            lsr();//9200
            lsr();//9201
            lsr();//9202
            lsr();//9203
            Label9204:
            and(0x0F);//9204
            cmp(a, ram[0x071A]);//9206
            if (z)
                goto Label920F;//9209
            if (!c)
                goto Label920F;//920B
            lda(0x00);//920D
            Label920F:
            str(0x075B, a);//920F
            Label9282();//9212
            Label9264(); //9215
        }

        void Label9218()
        {
            ldam(0x0772);//9218
            switch (a)
            {
                case 0:
                    Label9224();
                    break;
                case 1:
                    Label8567();
                    break;
                case 2:
                    Label9237();
                    break;
            }
        }

        void Label9224()
        {
            lda(0x00);//9224
            str(0x073C, a);//9226
            str(0x0722, a);//9229
            lda(0x02);//922C
            str(0xFC, a);//922E
            inc(0x0774);//9230
            inc(0x0772);//9233
        }

        void Label9237()
        {
            lda(0x00);//9237
            str(0x0774, a);//9239
            ldam(0x06FC);//923C
            and(0x10);//923F
            if (!z)
                goto Label9248;//9241
            ldam(0x07A0);//9243
            if (!z)
                return;//9246
            Label9248:
            lda(0x80);//9248
            str(0xFC, a);//924A
            Label9282();//924C
            if (!c)
            {
                Label9264();//924F
                return;
            }
            ldam(0x075F);//9251
            str(0x07FD, a);//9254
            lda(0x00);//9257
            asl();//9259
            str(0x0772, a);//925A
            str(0x07A0, a);//925D
            str(0x0770, a);//9260
        }

        void Label9248()
        {
            lda(0x80);//9248
            str(0xFC, a);//924A
            Label9282();//924C
            if (!c)
            {
                Label9264();//924F
                return;
            }
            ldam(0x075F);//9251
            str(0x07FD, a);//9254
            lda(0x00);//9257
            asl();//9259
            str(0x0772, a);//925A
            str(0x07A0, a);//925D
            str(0x0770, a);//9260
        }

        void Label9264()
        {
            Label9C03();//9264
            lda(0x01);//9267
            str(0x0754, a);//9269
            inc(0x0757);//926C
            lda(0x00);//926F
            str(0x0747, a);//9271
            str(0x0756, a);//9274
            str(0x0E, a);//9277
            str(0x0772, a);//9279
            lda(0x01);//927C
            str(0x0770, a);//927E
        }

        void Label9282()
        {
            c = true;//9282
            ldam(0x077A);//9283
            if (z)
                return;//9286
            ldam(0x0761);//9288
            if (n) //
                return;//928B
            ldam(0x0753);//928D
            eor(0x01);//9290
            str(0x0753, a);//9292
            ldx(0x06);//9295
            Label9297:
            ldam(0x075A + x);//9297
            pha();//929A
            ldam(0x0761 + x);//929B
            str(0x075A + x, a);//929E
            pla();//92A1
            str(0x0761 + x, a);//92A2
            dex();//92A5
            if (!n)
                goto Label9297;//92A6
            c = false;//92A8
        }

        void Label92AA()
        {
            lda(0xFF);//92AA
            str(0x06C9, a);//92AC
        }

        void Label92AF()
        {

        }

        void Label92B0()
        {
            ldym(0x071F);//92B0
            if (!z)
                goto Label92BA;//92B3
            ldy(0x08);//92B5
            str(0x071F, y);//92B7
            Label92BA:
            dey();//92BA
            tya();//92BB
            Label92C8();//92BC
            dec(0x071F);//92BF
            if (!z)
                return;//92C2
            Label896A();//92C4
        }

        void Label92C8()
        {
            switch (a)
            {
                case 0:
                    Label92DB();
                    break;
                case 1:
                    Label88AE();
                    break;
                case 2:
                    Label88AE();
                    break;
                case 3:
                    Label93FC();
                    break;
                case 4:
                    Label92DB();
                    break;
                case 5:
                    Label88AE();
                    break;
                case 6:
                    Label88AE();
                    break;
                case 7:
                    Label93FC();
                    break;
            }
        }

        void Label92DB()
        {
            inc(0x0726);//92DB
            ldam(0x0726);//92DE
            and(0x0F);//92E1
            if (!z)
                goto Label92EB;//92E3
            str(0x0726, a);//92E5
            inc(0x0725);//92E8
            Label92EB:
            inc(0x06A0);//92EB
            ldam(0x06A0);//92EE
            and(0x1F);//92F1
            str(0x06A0, a);//92F3
        }

        void Label93FC()
        {
            ldam(0x0728);//93FC
            if (z)
                goto Label9404;//93FF
            Label9508();//9401
            Label9404:
            ldx(0x0C);//9404
            lda(0x00);//9406
            Label9408:
            str(0x06A1 + x, a);//9408
            dex();//940B
            if (!n)
                goto Label9408;//940C
            ldym(0x0742);//940E
            if (z)
                goto Label9455;//9411
            ldam(0x0725);//9413
            Label9416:
            cmp(a, 0x03);//9416
            if (n) //
                goto Label941F;//9418
            c = true;//941A
            sbc(0x03);//941B
            if (!n)
                goto Label9416;//941D
            Label941F:
            asl();//941F
            asl();//9420
            asl();//9421
            asl();//9422
            adc(ram[0x92F6 + y]);//9423
            adc(ram[0x0726]);//9426
            tax();//9429
            ldam(0x92FA + x);//942A
            if (z)
                goto Label9455;//942D
            pha();//942F
            and(0x0F);//9430
            c = true;//9432
            sbc(0x01);//9433
            str(0x00, a);//9435
            asl();//9437
            adc(ram[0x00]);//9438
            tax();//943A
            pla();//943B
            lsr();//943C
            lsr();//943D
            lsr();//943E
            lsr();//943F
            tay();//9440
            lda(0x03);//9441
            str(0x00, a);//9443
            Label9445:
            ldam(0x938A + x);//9445
            str(0x06A1 + y, a);//9448
            inx();//944B
            iny();//944C
            cmp(y, 0x0B);//944D
            if (z)
                goto Label9455;//944F
            dec(0x00);//9451
            if (!z)
                goto Label9445;//9453
            Label9455:
            ldxm(0x0741);//9455
            if (z)
                goto Label946D;//9458
            ldym(0x93AD + x);//945A
            ldx(0x00);//945D
            Label945F:
            ldam(0x93B1 + y);//945F
            if (z)
                goto Label9467;//9462
            str(0x06A1 + x, a);//9464
            Label9467:
            iny();//9467
            inx();//9468
            cmp(x, 0x0D);//9469
            if (!z)
                goto Label945F;//946B
            Label946D:
            ldym(0x074E);//946D
            if (!z)
                goto Label947E;//9470
            ldam(0x075F);//9472
            cmp(a, 0x07);//9475
            if (!z)
                goto Label947E;//9477
            lda(0x62);//9479
            goto Label9488; //947B;
            Label947E:
            ldam(0x93D8 + y);//947E
            ldym(0x0743);//9481
            if (z)
                goto Label9488;//9484
            lda(0x88);//9486
            Label9488:
            str(0x07, a);//9488
            ldx(0x00);//948A
            ldam(0x0727);//948C
            asl();//948F
            tay();//9490
            Label9491:
            ldam(0x93DC + y);//9491
            str(0x00, a);//9494
            iny();//9496
            str(0x01, y);//9497
            ldam(0x0743);//9499
            if (z)
                goto Label94A8;//949C
            cmp(x, 0x00);//949E
            if (z)
                goto Label94A8;//94A0
            ldam(0x00);//94A2
            and(0x08);//94A4
            str(0x00, a);//94A6
            Label94A8:
            ldy(0x00);//94A8
            Label94AA:
            ldam(0xC68A + y);//94AA
            bit(ram[0x00]);//94AD
            if (z)
                goto Label94B6;//94AF
            ldam(0x07);//94B1
            str(0x06A1 + x, a);//94B3
            Label94B6:
            inx();//94B6
            cmp(x, 0x0D);//94B7
            if (z)
                goto Label94D3;//94B9
            ldam(0x074E);//94BB
            cmp(a, 0x02);//94BE
            if (!z)
                goto Label94CA;//94C0
            cmp(x, 0x0B);//94C2
            if (!z)
                goto Label94CA;//94C4
            lda(0x54);//94C6
            str(0x07, a);//94C8
            Label94CA:
            iny();//94CA
            cmp(y, 0x08);//94CB
            if (!z)
                goto Label94AA;//94CD
            ldym(0x01);//94CF
            if (!z)
                goto Label9491;//94D1
            Label94D3:
            Label9508();//94D3
            ldam(0x06A0);//94D6
            Label9BE1();//94D9
            ldx(0x00);//94DC
            ldy(0x00);//94DE
            Label94E0:
            str(0x00, y);//94E0
            ldam(0x06A1 + x);//94E2
            and(0xC0);//94E5
            asl();//94E7
            rol();//94E8
            rol();//94E9
            tay();//94EA
            ldam(0x06A1 + x);//94EB
            cmp(a, ram[0x9504 + y]);//94EE
            if (c)
                goto Label94F5;//94F1
            lda(0x00);//94F3
            Label94F5:
            ldym(0x00);//94F5
            str(W(0x06) + y, a);//94F7
            tya();//94F9
            c = false;//94FA
            adc(0x10);//94FB
            tay();//94FD
            inx();//94FE
            cmp(x, 0x0D);//94FF
            if (!c)
                goto Label94E0;//9501
        }

        void Label9508()
        {
            Label9508:
            ldx(0x02);//9508
            Label950A:
            str(0x08, x);//950A
            lda(0x00);//950C
            str(0x0729, a);//950E
            ldym(0x072C);//9511
            ldam(W(0xE7) + y);//9514
            cmp(a, 0xFD);//9516
            if (z)
                goto Label9565;//9518
            ldam(0x0730 + x);//951A
            if (!n)
                goto Label9565;//951D
            iny();//951F
            ldam(W(0xE7) + y);//9520
            asl();//9522
            if (!c)
                goto Label9530;//9523
            ldam(0x072B);//9525
            if (!z)
                goto Label9530;//9528
            inc(0x072B);//952A
            inc(0x072A);//952D
            Label9530:
            dey();//9530
            ldam(W(0xE7) + y);//9531
            and(0x0F);//9533
            cmp(a, 0x0D);//9535
            if (!z)
                goto Label9554;//9537
            iny();//9539
            ldam(W(0xE7) + y);//953A
            dey();//953C
            and(0x40);//953D
            if (!z)
                goto Label955D;//953F
            ldam(0x072B);//9541
            if (!z)
                goto Label955D;//9544
            iny();//9546
            ldam(W(0xE7) + y);//9547
            and(0x1F);//9549
            str(0x072A, a);//954B
            inc(0x072B);//954E
            goto Label956E; //9551
            Label9554:
            cmp(a, 0x0E);//9554
            if (!z)
                goto Label955D;//9556
            ldam(0x0728);//9558
            if (!z)
                goto Label9565;//955B
            Label955D:
            ldam(0x072A);//955D
            cmp(a, ram[0x0725]);//9560
            if (!c)
                goto Label956B;//9563
            Label9565:
            Label9595();//9565
            Label9568:
            goto Label9571; //9568
            Label956B:
            inc(0x0729);//956B
            Label956E:
            Label9589();//956E
            Label9571:
            ldxm(0x08);//9571
            ldam(0x0730 + x);//9573
            if (n) //
                goto Label957B;//9576
            dec(0x0730 + x);//9578
            Label957B:
            dex();//957B
            if (!n)
                goto Label950A;//957C
            ldam(0x0729);//957E
            if (!z)
                goto Label9508;//9581
            ldam(0x0728);//9583
            if (!z)
                goto Label9508;//9586
        }

        void Label9589()
        {
            inc(0x072C);//9589
            inc(0x072C);//958C
            lda(0x00);//958F
            str(0x072B, a);//9591
        }

        void Label9595()
        {
            ldam(0x0730 + x);//9595
            if (n) //
                goto Label959D;//9598
            ldym(0x072D + x);//959A
            Label959D:
            ldx(0x10);//959D
            ldam(W(0xE7) + y);//959F
            cmp(a, 0xFD);//95A1
            if (z)
                return;//95A3
            and(0x0F);//95A5
            cmp(a, 0x0F);//95A7
            if (z)
                goto Label95B3;//95A9
            ldx(0x08);//95AB
            cmp(a, 0x0C);//95AD
            if (z)
                goto Label95B3;//95AF
            ldx(0x00);//95B1
            Label95B3:
            str(0x07, x);//95B3
            ldxm(0x08);//95B5
            cmp(a, 0x0E);//95B7
            if (!z)
                goto Label95C3;//95B9
            lda(0x00);//95BB
            str(0x07, a);//95BD
            lda(0x2E);//95BF
            if (!z)
                goto Label9616;//95C1
            Label95C3:
            cmp(a, 0x0D);//95C3
            if (!z)
                goto Label95E2;//95C5
            lda(0x22);//95C7
            str(0x07, a);//95C9
            iny();//95CB
            ldam(W(0xE7) + y);//95CC
            and(0x40);//95CE
            if (z)
                return;//95D0
            ldam(W(0xE7) + y);//95D2
            and(0x7F);//95D4
            cmp(a, 0x4B);//95D6
            if (!z)
                goto Label95DD;//95D8
            inc(0x0745);//95DA
            Label95DD:
            and(0x3F);//95DD
            goto Label9616; //95DF
            Label95E2:
            cmp(a, 0x0C);//95E2
            if (c)
                goto Label960D;//95E4
            iny();//95E6
            ldam(W(0xE7) + y);//95E7
            and(0x70);//95E9
            if (!z)
                goto Label95F8;//95EB
            lda(0x16);//95ED
            str(0x07, a);//95EF
            ldam(W(0xE7) + y);//95F1
            and(0x0F);//95F3
            goto Label9616; //95F5
            Label95F8:
            str(0x00, a);//95F8
            cmp(a, 0x70);//95FA
            if (!z)
                goto Label9608;//95FC
            ldam(W(0xE7) + y);//95FE
            Label9600:
            and(0x08);//9600
            if (z)
                goto Label9608;//9602
            lda(0x00);//9604
            str(0x00, a);//9606
            Label9608:
            ldam(0x00);//9608
            goto Label9612; //960A
            Label960D:
            iny();//960D
            Label960E:
            ldam(W(0xE7) + y);//960E
            and(0x70);//9610
            Label9612:
            lsr();//9612
            lsr();//9613
            lsr();//9614
            lsr();//9615
            Label9616:
            str(0x00, a);//9616
            ldam(0x0730 + x);//9618
            if (!n)
                goto Label965F;//961B
            ldam(0x072A);//961D
            cmp(a, ram[0x0725]);//9620
            if (z)
                goto Label9636;//9623
            ldym(0x072C);//9625
            ldam(W(0xE7) + y);//9628
            and(0x0F);//962A
            cmp(a, 0x0E);//962C
            if (!z)
                return;//962E
            ldam(0x0728);//9630
            if (!z)
                goto Label9656;//9633
            return;
            Label9636:
            ldam(0x0728);//9636
            if (z)
                goto Label9646;//9639
            lda(0x00);//963B
            str(0x0728, a);//963D
            str(0x0729, a);//9640
            str(0x08, a);//9643
            return;
            Label9646:
            ldym(0x072C);//9646
            ldam(W(0xE7) + y);//9649
            and(0xF0);//964B
            lsr();//964D
            lsr();//964E
            lsr();//964F
            lsr();//9650
            cmp(a, ram[0x0726]);//9651
            if (!z)
                return;//9654
            Label9656:
            ldam(0x072C);//9656
            str(0x072D + x, a);//9659
            Label9589();//965C
            Label965F:
            ldam(0x00);//965F
            c = false;//9661
            adc(ram[0x07]);//9662

            switch (a)
            {
                case 0:
                    Label98E5();
                    break;
                case 1:
                    Label9740();
                    break;
                case 2:
                    Label9A2E();
                    break;
                case 3:
                    Label9A3E();
                    break;
                case 4:
                    Label99F2();
                    break;
                case 5:
                    Label9A50();
                    break;
                case 6:
                    Label9A59();
                    break;
                case 7:
                    Label98E5();
                    break;
                case 8:
                    Label9B41();
                    break;
                case 9:
                    Label97BA();
                    break;
                case 10:
                    Label9979();
                    break;
                case 11:
                    Label997C();
                    break;
                case 12:
                    Label997F();
                    break;
                case 13:
                    Label9957();
                    break;
                case 14:
                    Label9968();
                    break;
                case 15:
                    Label996B();
                    break;
                case 16:
                    Label99D0();
                    break;
                case 17:
                    Label99D7();
                    break;
                case 18:
                    Label9806();
                    break;
                case 19:
                    Label9AB7();
                    break;
                case 20:
                    Label98AB();
                    break;
                case 21:
                    Label9994();
                    break;
                case 22:
                    Label9B0E();
                    break;
                case 23:
                    Label9B0E();
                    break;
                case 24:
                    Label9B0E();
                    break;
                case 25:
                    Label9B01();
                    break;
                case 26:
                    Label9B19();
                    break;
                case 27:
                    Label9B19();
                    break;
                case 28:
                    Label9B19();
                    break;
                case 29:
                    Label9B14();
                    break;
                case 30:
                    Label9B19();
                    break;
                case 31:
                    Label986F();
                    break;
                case 32:
                    Label9A19();
                    break;
                case 33:
                    Label9AD3();
                    break;
                case 34:
                    Label9882();
                    break;
                case 35:
                    Label999E();
                    break;
                case 36:
                    Label9A09();
                    break;
                case 37:
                    Label9A0E();
                    break;
                case 38:
                    Label9A01();
                    break;
                case 39:
                    Label96F2();
                    break;
                case 40:
                    Label970D();
                    break;
                case 41:
                    Label970D();
                    break;
                case 42:
                    Label972B();
                    break;
                case 43:
                    Label972B();
                    break;
                case 44:
                    Label972B();
                    break;
                case 45:
                    return;
                case 46:
                    Label96C5();
                    break;
            }
        }

        void Label96C5()
        {
            ldym(0x072D + x);//96C5
            iny();//96C8
            ldam(W(0xE7) + y);//96C9
            pha();//96CB
            and(0x40);//96CC
            if (!z)
                goto Label96E2;//96CE
            pla();//96D0
            pha();//96D1
            and(0x0F);//96D2
            str(0x0727, a);//96D4
            pla();//96D7
            and(0x30);//96D8
            lsr();//96DA
            lsr();//96DB
            lsr();//96DC
            lsr();//96DD
            str(0x0742, a);//96DE
            return;
            Label96E2:
            pla();//96E2
            and(0x07);//96E3
            cmp(a, 0x04);//96E5
            if (!c)
                goto Label96EE;//96E7
            str(0x0744, a);//96E9
            lda(0x00);//96EC
            Label96EE:
            str(0x0741, a);//96EE
        }

        void Label96F2()
        {
            ldx(0x04);//96F2
            ldam(0x075F);//96F4
            if (z)
                goto Label9701;//96F7
            inx();//96F9
            ldym(0x074E);//96FA
            dey();//96FD
            if (!z)
                goto Label9701;//96FE
            inx();//9700
            Label9701:
            txa();//9701
            str(0x06D6, a);//9702
            Label8808();//9705
            lda(0x0D);//9708
            Label9716();//970A
            ldam(0x0723);//970D
            eor(0x01);//9710
            str(0x0723, a);//9712
        }

        void Label970D()
        {
            ldam(0x0723);//970D
            eor(0x01);//9710
            str(0x0723, a);//9712
        }

        void Label9716()
        {
            str(0x00, a);//9716
            lda(0x00);//9718
            ldx(0x04);//971A
            Label971C:
            ldym(0x16 + x);//971C
            cmp(y, ram[0x00]);//971E
            if (!z)
                goto Label9724;//9720
            str(0x0F + x, a);//9722
            Label9724:
            dex();//9724
            if (!n)
                goto Label971C;//9725
        }

        void Label972B()
        {
            ldxm(0x00);//972B
            ldam(0x9720 + x);//972D
            ldy(0x05);//9730
            Label9732:
            dey();//9732
            if (n) //
                goto Label973C;//9733
            cmp(a, ram[0x0016 + y]);//9735
            if (!z)
                goto Label9732;//9738
            lda(0x00);//973A
            Label973C:
            str(0x06CD, a);//973C
        }

        void Label9740()
        {
            ldam(0x0733);//9740

            switch (a)
            {
                case 0:
                    Label974C();
                    break;
                case 1:
                    Label9778();
                    break;
                case 2:
                    Label9A69();
                    break;
            }
        }

        void Label974C()
        {
            Label9BBB();//974C
            ldam(0x0730 + x);//974F
            if (z)
                goto Label9773;//9752
            if (!n)
                goto Label9767;//9754
            tya();//9756
            str(0x0730 + x, a);//9757
            ldam(0x0725);//975A
            orm(0x0726);//975D
            if (z)
                goto Label9767;//9760
            lda(0x16);//9762
            Label97B0(); //9764
            return;
            Label9767:
            ldxm(0x07);//9767
            Label9769:
            lda(0x17);//9769
            str(0x06A1 + x, a);//976B
            lda(0x4C);//976E
            Label97AA(); //9770
            return;
            Label9773:
            lda(0x18);//9773
            Label9775:
            Label97B0(); //9775
            return;
        }

        void Label9778()
        {
            Label9BAC();//9778
            str(0x06, y);//977B
            if (!c)
                goto Label978B;//977D
            ldam(0x0730 + x);//977F
            lsr();//9782
            str(0x0736 + x, a);//9783
            lda(0x19);//9786
            Label97B0(); //9788
            return;
            Label978B:
            lda(0x1B);//978B
            ldym(0x0730 + x);//978D
            if (z)
                goto Label97B0;//9790
            ldam(0x0736 + x);//9792
            str(0x06, a);//9795
            ldxm(0x07);//9797
            lda(0x1A);//9799
            str(0x06A1 + x, a);//979B
            cmp(y, ram[0x06]);//979E
            if (!z)
                return;//97A0
            inx();//97A2
            lda(0x4F);//97A3
            str(0x06A1 + x, a);//97A5
            lda(0x50);//97A8
            inx();//97AA
            ldy(0x0F);//97AB
            Label9B7D(); //97AD
            return;
            Label97B0:
            ldxm(0x07);//97B0
            ldy(0x00);//97B2
            Label9B7D(); //97B4
            return;
        }

        void Label97AA()
        {
            inx();//97AA
            ldy(0x0F);//97AB
            Label9B7D(); //97AD
            return;
            //Label97B0:
            //ldxm(0x07);//97B0
            //ldy(0x00);//97B2
            //Label9B7D(); //97B4
            //return;
        }

        void Label97B0()
        {
            ldxm(0x07);//97B0
            ldy(0x00);//97B2
            Label9B7D(); //97B4
        }

        void Label97BA()
        {
            Label9BAC();//97BA
            ldy(0x00);//97BD
            if (c)
                goto Label97C8;//97BF
            iny();//97C1
            ldam(0x0730 + x);//97C2
            if (!z)
                goto Label97C8;//97C5
            iny();//97C7
            Label97C8:
            ldam(0x97B7 + y);//97C8
            str(0x06A1, a);//97CB
        }

        void Label9806()
        {
            Label9BBB();//9806
            str(0x07, y);//9809
            ldy(0x04);//980B
            Label9BAF();//980D
            txa();//9810
            pha();//9811
            ldym(0x0730 + x);//9812
            ldxm(0x07);//9815
            lda(0x0B);//9817
            str(0x06, a);//9819
            Label981B:
            ldam(0x97CF + y);//981B
            str(0x06A1 + x, a);//981E
            inx();//9821
            ldam(0x06);//9822
            if (z)
                goto Label982D;//9824
            iny();//9826
            iny();//9827
            iny();//9828
            iny();//9829
            iny();//982A
            dec(0x06);//982B
            Label982D:
            cmp(x, 0x0B);//982D
            if (!z)
                goto Label981B;//982F
            pla();//9831
            tax();//9832
            ldam(0x0725);//9833
            if (z)
                return;//9836
            ldam(0x0730 + x);//9838
            cmp(a, 0x01);//983B
            if (z)
                goto Label9869;//983D
            ldym(0x07);//983F
            if (!z)
                goto Label9847;//9841
            cmp(a, 0x03);//9843
            if (z)
                goto Label9869;//9845
            Label9847:
            cmp(a, 0x02);//9847
            if (!z)
                return;//9849
            Label9BCB();//984B
            pha();//984E
            Label994A();//984F
            pla();//9852
            str(0x87 + x, a);//9853
            ldam(0x0725);//9855
            str(0x6E + x, a);//9858
            lda(0x01);//985A
            str(0xB6 + x, a);//985C
            str(0x0F + x, a);//985E
            lda(0x90);//9860
            str(0xCF + x, a);//9862
            lda(0x31);//9864
            str(0x16 + x, a);//9866
            return;
            Label9869:
            ldy(0x52);//9869
            str(0x06AB, y);//986B
        }

        void Label986F()
        {
            Label9BBB();//986F
            ldym(0x0730 + x);//9872
            ldxm(0x07);//9875
            lda(0x6B);//9877
            str(0x06A1 + x, a);//9879
            lda(0x6C);//987C
            str(0x06A2 + x, a);//987E
        }

        void Label9882()
        {
            ldy(0x03);//9882
            Label9BAF();//9884
            ldy(0x0A);//9887
            Label98B3();//9889
            if (c)
                return;//988C
            ldx(0x06);//988E
            Label9890:
            lda(0x00);//9890
            str(0x06A1 + x, a);//9892
            dex();//9895
            if (!n)
                goto Label9890;//9896
            ldam(0x98DD + y);//9898
            str(0x06A8, a);//989B
        }

        void Label98AB()
        {
            ldy(0x03);//98AB
            Label9BAF();//98AD
            Label9BBB();//98B0
            dey();//98B3
            dey();//98B4
            str(0x05, y);//98B5
            ldym(0x0730 + x);//98B7
            str(0x06, y);//98BA
            ldxm(0x05);//98BC
            inx();//98BE
            ldam(0x989F + y);//98BF
            cmp(a, 0x00);//98C2
            if (z)
                goto Label98CE;//98C4
            ldx(0x00);//98C6
            ldym(0x05);//98C8
            Label9B7D();//98CA
            c = false;//98CD
            Label98CE:
            ldym(0x06);//98CE
            ldam(0x98A3 + y);//98D0
            str(0x06A1 + x, a);//98D3
            ldam(0x98A7 + y);//98D6
            str(0x06A2 + x, a);//98D9
        }

        void Label98B3()
        {
            dey();//98B3
            dey();//98B4
            str(0x05, y);//98B5
            ldym(0x0730 + x);//98B7
            str(0x06, y);//98BA
            ldxm(0x05);//98BC
            inx();//98BE
            ldam(0x989F + y);//98BF
            cmp(a, 0x00);//98C2
            if (z)
                goto Label98CE;//98C4
            ldx(0x00);//98C6
            ldym(0x05);//98C8
            Label9B7D();//98CA
            c = false;//98CD
            Label98CE:
            ldym(0x06);//98CE
            ldam(0x98A3 + y);//98D0
            str(0x06A1 + x, a);//98D3
            ldam(0x98A7 + y);//98D6
            str(0x06A2 + x, a);//98D9
        }

        void Label98E5()
        {
            Label9939();//98E5
            ldam(0x00);//98E8
            if (z)
                goto Label98F0;//98EA
            iny();//98EC
            iny();//98ED
            iny();//98EE
            iny();//98EF
            Label98F0:
            tya();//98F0
            pha();//98F1
            ldam(0x0760);//98F2
            orm(0x075F);//98F5
            if (z)
                goto Label9925;//98F8
            ldym(0x0730 + x);//98FA
            if (z)
                goto Label9925;//98FD
            Label994A();//98FF
            if (c)
                goto Label9925;//9902
            Label9BCB();//9904
            c = false;//9907
            adc(0x08);//9908
            str(0x87 + x, a);//990A
            ldam(0x0725);//990C
            adc(0x00);//990F
            str(0x6E + x, a);//9911
            lda(0x01);//9913
            str(0xB6 + x, a);//9915
            str(0x0F + x, a);//9917
            Label9BD3();//9919
            str(0xCF + x, a);//991C
            lda(0x0D);//991E
            str(0x16 + x, a);//9920
            LabelC787();//9922
            Label9925:
            pla();//9925
            tay();//9926
            ldxm(0x07);//9927
            ldam(0x98DD + y);//9929
            str(0x06A1 + x, a);//992C
            inx();//992F
            ldam(0x98DF + y);//9930
            ldym(0x06);//9933
            dey();//9935
            Label9B7D(); //9936
            return;
        }

        void Label9939()
        {
            ldy(0x01);//9939
            Label9BAF();//993B
            Label9BBB();//993E
            tya();//9941
            and(0x07);//9942
            str(0x06, a);//9944
            ldym(0x0730 + x);//9946
        }

        void Label994A()
        {
            ldx(0x00);//994A
            Label994C:
            c = false;//994C
            ldam(0x0F + x);//994D
            if (z)
                return;//994F
            inx();//9951
            cmp(x, 0x05);//9952
            if (!z)
                goto Label994C;//9954
        }

        void Label9957()
        {
            Label9BAC();//9957
            lda(0x86);//995A
            str(0x06AB, a);//995C
            ldx(0x0B);//995F
            ldy(0x01);//9961
            lda(0x87);//9963
            Label9B7D(); //9965
            return;
        }

        void Label9968()
        {
            lda(0x03);//9968
            lda(0x07);//996B
            pha();//996D
            Label9BAC();//996E
            pla();//9971
            tax();//9972
            lda(0xC0);//9973
            str(0x06A1 + x, a);//9975
        }

        void Label996B()
        {
            lda(0x07);//996B
            pha();//996D
            Label9BAC();//996E
            pla();//9971
            tax();//9972
            lda(0xC0);//9973
            str(0x06A1 + x, a);//9975
        }

        void Label9979()
        {
            lda(0x06);//997C
            Label9981();
        }

        void Label997C()
        {
            lda(0x07);//997C
            Label9981();
        }

        void Label997F()
        {
            lda(0x09);//997F
            Label9981();
        }

        void Label9981()
        {
            pha();//9981
            Label9BAC();//9982
            pla();//9985
            tax();//9986
            Label9987:
            lda(0x0B);//9987
            str(0x06A1 + x, a);//9989
            inx();//998C
            ldy(0x00);//998D
            lda(0x63);//998F
            Label9B7D(); //9991
            return;
        }

        void Label9994()
        {
            Label9BBB();//9994
            ldx(0x02);//9997
            lda(0x6D);//9999
            Label9B7D(); //999B
            return;
        }

        void Label999E()
        {
            lda(0x24);//999E
            str(0x06A1, a);//99A0
            ldx(0x01);//99A3
            ldy(0x08);//99A5
            lda(0x25);//99A7
            Label9B7D();//99A9
            lda(0x61);//99AC
            str(0x06AB, a);//99AE
            Label9BCB();//99B1
            c = true;//99B4
            sbc(0x08);//99B5
            str(0x8C, a);//99B7
            ldam(0x0725);//99B9
            sbc(0x00);//99BC
            str(0x73, a);//99BE
            lda(0x30);//99C0
            str(0xD4, a);//99C2
            lda(0xB0);//99C4
            str(0x010D, a);//99C6
            lda(0x30);//99C9
            str(0x1B, a);//99CB
            inc(0x14);//99CD
        }

        void Label99D0()
        {
            ldx(0x00);//99D0
            ldy(0x0F);//99D2
            Label99E9(); //99D4
            return;
        }

        void Label99D7()
        {
            txa();//99D7
            pha();//99D8
            ldx(0x01);//99D9
            ldy(0x0F);//99DB
            lda(0x44);//99DD
            Label9B7D();//99DF
            pla();//99E2
            tax();//99E3
            Label9BBB();//99E4
            ldx(0x01);//99E7
            lda(0x40);//99E9
            Label9B7D(); //99EB
            return;
        }

        void Label99E9()
        {
            lda(0x40);//99E9
            Label9B7D(); //99EB
        }

        void Label99F2()
        {
            ldym(0x074E);//99F2
            ldam(0x99EE + y);//99F5
            Label9A44(); //99F8
            return;
        }

        void Label9A01()
        {
            ldy(0x0C);//9A01
            Label9BAF();//9A03
            Label9A0E(); //9A06
            return;
        }

        void Label9A09()
        {
            lda(0x08);//9A09
            str(0x0773, a);//9A0B
            ldym(0x00);//9A0E
            ldxm(0x99F9 + y);//9A10
            ldam(0x99FC + y);//9A13
            Label9A20(); //9A16
            return;
        }

        void Label9A0E()
        {
            ldym(0x00);//9A0E
            ldxm(0x99F9 + y);//9A10
            ldam(0x99FC + y);//9A13
            Label9A20(); //9A16
            return;
        }

        void Label9A19()
        {
            Label9BBB();//9A19
            ldxm(0x07);//9A1C
            lda(0xC4);//9A1E
            ldy(0x00);//9A20
            Label9B7D(); //9A22
            return;
        }

        void Label9A20()
        {
            ldy(0x00);//9A20
            Label9B7D(); //9A22
            return;
        }

        void Label9A2E()
        {
            ldym(0x074E);//9A2E
            ldam(0x0743);//9A31
            if (z)
                goto Label9A38;//9A34
            ldy(0x04);//9A36
            Label9A38:
            ldam(0x9A29 + y);//9A38
            Label9A44(); //9A3B
            return;
        }

        void Label9A3E()
        {
            ldym(0x074E);//9A3E
            ldam(0x9A25 + y);//9A41
            pha();//9A44
            Label9BAC();//9A45
            ldxm(0x07);//9A48
            ldy(0x00);//9A4A
            pla();//9A4C
            Label9B7D(); //9A4D
            return;
        }

        void Label9A44()
        {
            pha();//9A44
            Label9BAC();//9A45
            ldxm(0x07);//9A48
            ldy(0x00);//9A4A
            pla();//9A4C
            Label9B7D(); //9A4D
            return;
        }

        void Label9A48()
        {
            ldxm(0x07);//9A48
            ldy(0x00);//9A4A
            pla();//9A4C
            Label9B7D(); //9A4D
            return;
        }

        void Label9A50()
        {
            ldym(0x074E);//9A50
            ldam(0x9A29 + y);//9A53
            Label9A5F(); //9A56
            return;
        }

        void Label9A59()
        {
            ldym(0x074E);//9A59
            ldam(0x9A25 + y);//9A5C
            pha();//9A5F
            Label9BBB();//9A60
            pla();//9A63
            ldxm(0x07);//9A64
            Label9B7D(); //9A66
            return;
        }

        void Label9A5F()
        {
            pha();//9A5F
            Label9BBB();//9A60
            pla();//9A63
            ldxm(0x07);//9A64
            Label9B7D(); //9A66
            return;
        }

        void Label9A69()
        {
            Label9BBB();//9A69
            ldxm(0x07);//9A6C
            lda(0x64);//9A6E
            str(0x06A1 + x, a);//9A70
            inx();//9A73
            dey();//9A74
            if (n) //
                goto Label9A85;//9A75
            lda(0x65);//9A77
            str(0x06A1 + x, a);//9A79
            inx();//9A7C
            dey();//9A7D
            if (n) //
                goto Label9A85;//9A7E
            lda(0x66);//9A80
            Label9B7D();//9A82
            Label9A85:
            ldxm(0x046A);//9A85
            Label9BD3();//9A88
            str(0x0477 + x, a);//9A8B
            ldam(0x0725);//9A8E
            str(0x046B + x, a);//9A91
            Label9BCB();//9A94
            str(0x0471 + x, a);//9A97
            inx();//9A9A
            cmp(x, 0x06);//9A9B
            if (!c)
                goto Label9AA1;//9A9D
            ldx(0x00);//9A9F
            Label9AA1:
            str(0x046A, x);//9AA1
        }

        void Label9AB7()
        {
            Label9BAC();//9AB7
            if (!c)
                goto Label9AC1;//9ABA
            lda(0x09);//9ABC
            str(0x0734, a);//9ABE
            Label9AC1:
            dec(0x0734);//9AC1
            ldym(0x0734);//9AC4
            ldxm(0x9AAE + y);//9AC7
            ldam(0x9AA5 + y);//9ACA
            tay();//9ACD
            lda(0x61);//9ACE
            Label9B7D(); //9AD0
            return;
        }

        void Label9AD3()
        {
            Label9BBB();//9AD3
            Label994A();//9AD6
            Label9BCB();//9AD9
            str(0x87 + x, a);//9ADC
            ldam(0x0725);//9ADE
            str(0x6E + x, a);//9AE1
            Label9BD3();//9AE3
            str(0xCF + x, a);//9AE6
            str(0x58 + x, a);//9AE8
            lda(0x32);//9AEA
            str(0x16 + x, a);//9AEC
            ldy(0x01);//9AEE
            str(0xB6 + x, y);//9AF0
            inc(0x0F + x);//9AF2
            ldxm(0x07);//9AF4
            lda(0x67);//9AF6
            str(0x06A1 + x, a);//9AF8
            lda(0x68);//9AFB
            str(0x06A2 + x, a);//9AFD
        }

        void Label9B01()
        {
            ldam(0x075D);//9B01
            if (z)
                return;//9B04
            lda(0x00);//9B06
            str(0x075D, a);//9B08
            Label9B19(); //9B0B
            return;
        }

        void Label9B0E()
        {
            Label9B36();//9B0E
            Label9B2C(); //9B11
            return;
        }

        void Label9B14()
        {
            lda(0x00);//9B14
            str(0x06BC, a);//9B16
            Label9B36();//9B19
            str(0x07, y);//9B1C
            lda(0x00);//9B1E
            ldym(0x074E);//9B20
            dey();//9B23
            if (z)
                goto Label9B28;//9B24
            lda(0x05);//9B26
            Label9B28:
            c = false;//9B28
            adc(ram[0x07]);//9B29
            tay();//9B2B
            ldam(0xBDE8 + y);//9B2C
            pha();//9B2F
            Label9BBB();//9B30
            Label9A48(); //9B33
            return;
        }

        void Label9B19()
        {
            Label9B36();//9B19
            str(0x07, y);//9B1C
            lda(0x00);//9B1E
            ldym(0x074E);//9B20
            dey();//9B23
            if (z)
                goto Label9B28;//9B24
            lda(0x05);//9B26
            Label9B28:
            c = false;//9B28
            adc(ram[0x07]);//9B29
            tay();//9B2B
            ldam(0xBDE8 + y);//9B2C
            pha();//9B2F
            Label9BBB();//9B30
            Label9A48(); //9B33
            return;
        }

        void Label9B2C()
        {
            ldam(0xBDE8 + y);//9B2C
            pha();//9B2F
            Label9BBB();//9B30
            Label9A48(); //9B33
            return;
        }

        void Label9B36()
        {
            ldam(0x00);//9B36
            c = true;//9B38
            sbc(0x00);//9B39
            tay();//9B3B
        }

        void Label9B41()
        {
            Label9BAC();//9B41
            if (!c)
                goto Label9B73;//9B44
            ldam(0x074E);//9B46
            if (!z)
                goto Label9B73;//9B49
            ldxm(0x046A);//9B4B
            Label9BCB();//9B4E
            c = true;//9B51
            sbc(0x10);//9B52
            str(0x0471 + x, a);//9B54
            ldam(0x0725);//9B57
            sbc(0x00);//9B5A
            str(0x046B + x, a);//9B5C
            iny();//9B5F
            iny();//9B60
            tya();//9B61
            asl();//9B62
            asl();//9B63
            asl();//9B64
            asl();//9B65
            str(0x0477 + x, a);//9B66
            inx();//9B69
            cmp(x, 0x05);//9B6A
            if (!c)
                goto Label9B70;//9B6C
            ldx(0x00);//9B6E
            Label9B70:
            str(0x046A, x);//9B70
            Label9B73:
            ldxm(0x074E);//9B73
            ldam(0x9B3D + x);//9B76
            ldx(0x08);//9B79
            ldy(0x0F);//9B7B
            Label9B7D:
            str(0x0735, y);//9B7D
            ldym(0x06A1 + x);//9B80
            if (z)
                goto Label9B9D;//9B83
            cmp(y, 0x17);//9B85
            if (z)
                goto Label9BA0;//9B87
            cmp(y, 0x1A);//9B89
            if (z)
                goto Label9BA0;//9B8B
            cmp(y, 0xC0);//9B8D
            if (z)
                goto Label9B9D;//9B8F
            cmp(y, 0xC0);//9B91
            if (c)
                goto Label9BA0;//9B93
            cmp(y, 0x54);//9B95
            if (!z)
                goto Label9B9D;//9B97
            cmp(a, 0x50);//9B99
            if (z)
                goto Label9BA0;//9B9B
            Label9B9D:
            str(0x06A1 + x, a);//9B9D
            Label9BA0:
            inx();//9BA0
            cmp(x, 0x0D);//9BA1
            if (c)
                return;//9BA3
            ldym(0x0735);//9BA5
            dey();//9BA8
            if (!n)
                goto Label9B7D;//9BA9
        }

        void Label9B7D()
        {
            Label9B7D:
            str(0x0735, y);//9B7D
            ldym(0x06A1 + x);//9B80
            if (z)
                goto Label9B9D;//9B83
            cmp(y, 0x17);//9B85
            if (z)
                goto Label9BA0;//9B87
            cmp(y, 0x1A);//9B89
            if (z)
                goto Label9BA0;//9B8B
            cmp(y, 0xC0);//9B8D
            if (z)
                goto Label9B9D;//9B8F
            cmp(y, 0xC0);//9B91
            if (c)
                goto Label9BA0;//9B93
            cmp(y, 0x54);//9B95
            if (!z)
                goto Label9B9D;//9B97
            cmp(a, 0x50);//9B99
            if (z)
                goto Label9BA0;//9B9B
            Label9B9D:
            str(0x06A1 + x, a);//9B9D
            Label9BA0:
            inx();//9BA0
            cmp(x, 0x0D);//9BA1
            if (c)
                return;//9BA3
            ldym(0x0735);//9BA5
            dey();//9BA8
            if (!n)
                goto Label9B7D;//9BA9
        }

        void Label9BAC()
        {
            Label9BBB();//9BAC
            ldam(0x0730 + x);//9BAF
            c = false;//9BB2
            if (!n)
                return;//9BB3
            tya();//9BB5
            str(0x0730 + x, a);//9BB6
            c = true;//9BB9
        }

        void Label9BAF()
        {
            ldam(0x0730 + x);//9BAF
            c = false;//9BB2
            if (!n)
                return;//9BB3
            tya();//9BB5
            str(0x0730 + x, a);//9BB6
            c = true;//9BB9
        }

        void Label9BBB()
        {
            ldym(0x072D + x);//9BBB
            ldam(W(0xE7) + y);//9BBE
            and(0x0F);//9BC0
            str(0x07, a);//9BC2
            iny();//9BC4
            ldam(W(0xE7) + y);//9BC5
            and(0x0F);//9BC7
            tay();//9BC9
        }

        void Label9BCB()
        {
            ldam(0x0726);//9BCB
            asl();//9BCE
            asl();//9BCF
            asl();//9BD0
            asl();//9BD1
        }

        void Label9BD3()
        {
            ldam(0x07);//9BD3
            asl();//9BD5
            asl();//9BD6
            asl();//9BD7
            asl();//9BD8
            c = false;//9BD9
            adc(0x20);//9BDA
        }

        void Label9BE1()
        {
            pha();//9BE1
            lsr();//9BE2
            lsr();//9BE3
            lsr();//9BE4
            lsr();//9BE5
            tay();//9BE6
            ldam(0x9BDF + y);//9BE7
            str(0x07, a);//9BEA
            pla();//9BEC
            and(0x0F);//9BED
            c = false;//9BEF
            adc(ram[0x9BDD + y]);//9BF0
            str(0x06, a);//9BF3
        }

        void Label9C03()
        {
            Label9C13();//9C03
            str(0x0750, a);//9C06
            and(0x60);//9C09
            asl();//9C0B
            rol();//9C0C
            rol();//9C0D
            rol();//9C0E
            str(0x074E, a);//9C0F
        }

        void Label9C09()
        {
            and(0x60);//9C09
            asl();//9C0B
            rol();//9C0C
            rol();//9C0D
            rol();//9C0E
            str(0x074E, a);//9C0F
        }

        void Label9C13()
        {
            ldym(0x075F);//9C13
            ldam(0x9CB4 + y);//9C16
            c = false;//9C19
            adc(ram[0x0760]);//9C1A
            tay();//9C1D
            ldam(0x9CBC + y);//9C1E
        }

        void Label9C22()
        {
            ldam(0x0750);//9C22
            Label9C09();//9C25
            tay();//9C28
            ldam(0x0750);//9C29
            and(0x1F);//9C2C
            str(0x074F, a);//9C2E
            ldam(0x9CE0 + y);//9C31
            c = false;//9C34
            adc(ram[0x074F]);//9C35
            tay();//9C38
            ldam(0x9CE4 + y);//9C39
            str(0xE9, a);//9C3C
            ldam(0x9D06 + y);//9C3E
            str(0xEA, a);//9C41
            ldym(0x074E);//9C43
            ldam(0x9D28 + y);//9C46
            c = false;//9C49
            adc(ram[0x074F]);//9C4A
            tay();//9C4D
            ldam(0x9D2C + y);//9C4E
            str(0xE7, a);//9C51
            ldam(0x9D4E + y);//9C53
            str(0xE8, a);//9C56
            ldy(0x00);//9C58
            ldam(W(0xE7) + y);//9C5A
            pha();//9C5C
            and(0x07);//9C5D
            cmp(a, 0x04);//9C5F
            if (!c)
                goto Label9C68;//9C61
            str(0x0744, a);//9C63
            lda(0x00);//9C66
            Label9C68:
            str(0x0741, a);//9C68
            pla();//9C6B
            pha();//9C6C
            and(0x38);//9C6D
            lsr();//9C6F
            lsr();//9C70
            lsr();//9C71
            str(0x0710, a);//9C72
            pla();//9C75
            and(0xC0);//9C76
            c = false;//9C78
            rol();//9C79
            rol();//9C7A
            rol();//9C7B
            str(0x0715, a);//9C7C
            iny();//9C7F
            ldam(W(0xE7) + y);//9C80
            pha();//9C82
            and(0x0F);//9C83
            str(0x0727, a);//9C85
            pla();//9C88
            pha();//9C89
            and(0x30);//9C8A
            lsr();//9C8C
            lsr();//9C8D
            lsr();//9C8E
            lsr();//9C8F
            str(0x0742, a);//9C90
            pla();//9C93
            and(0xC0);//9C94
            c = false;//9C96
            rol();//9C97
            rol();//9C98
            rol();//9C99
            cmp(a, 0x03);//9C9A
            if (!z)
                goto Label9CA3;//9C9C
            str(0x0743, a);//9C9E
            lda(0x00);//9CA1
            Label9CA3:
            str(0x0733, a);//9CA3
            ldam(0xE7);//9CA6
            c = false;//9CA8
            adc(0x02);//9CA9
            str(0xE7, a);//9CAB
            ldam(0xE8);//9CAD
            adc(0x00);//9CAF
            str(0xE8, a);//9CB1
        }

        void LabelAEDC()
        {
            ldam(0x0772);//AEDC
            switch (a)
            {
                case 0:
                    Label8FE4();
                    break;
                case 1:
                    Label8567();
                    break;
                case 2:
                    Label9071();
                    break;
                case 3:
                    LabelAEEA();
                    break;
            }
        }

        void LabelAEEA()
        {
            ldxm(0x0753);//AEEA
            ldam(0x06FC + x);//AEED
            str(0x06FC, a);//AEF0
            LabelB04A();//AEF3
            ldam(0x0772);//AEF6
            cmp(a, 0x03);//AEF9
            if (c)
                goto LabelAEFE;//AEFB
            return;
            LabelAEFE:
            LabelB624();//AEFE
            ldx(0x00);//AF01
            LabelAF03:
            str(0x08, x);//AF03
            LabelC047();//AF05
            Label84C3();//AF08
            inx();//AF0B
            cmp(x, 0x06);//AF0C
            if (!z)
                goto LabelAF03;//AF0E
            LabelF180();//AF10
            LabelF12A();//AF13
            LabelEEE9();//AF16
            LabelBED4();//AF19
            ldx(0x01);//AF1C
            str(0x08, x);//AF1E
            LabelBE70();//AF20
            dex();//AF23
            str(0x08, x);//AF24
            LabelBE70();//AF26
            LabelBB96();//AF29
            LabelB9BC();//AF2C
            LabelB7B8();//AF2F
            LabelB855();//AF32
            LabelB74F();//AF35
            Label89E1();//AF38
            ldam(0xB5);//AF3B
            cmp(a, 0x02);//AF3D
            if (!n)
                goto LabelAF52;//AF3F
            ldam(0x079F);//AF41
            if (z)
                goto LabelAF64;//AF44
            cmp(a, 0x04);//AF46
            if (!z)
                goto LabelAF52;//AF48
            ldam(0x077F);//AF4A
            if (!z)
                goto LabelAF52;//AF4D
            Label90ED();//AF4F
            LabelAF52:
            ldym(0x079F);//AF52
            ldam(0x09);//AF55
            cmp(y, 0x08);//AF57
            if (c)
                goto LabelAF5D;//AF59
            lsr();//AF5B
            lsr();//AF5C
            LabelAF5D:
            lsr();//AF5D
            LabelB288();//AF5E
            LabelAF67(); //AF61
            return;
            LabelAF64:
            LabelB29A();//AF64
            ldam(0x0A);//AF67
            str(0x0D, a);//AF69
            lda(0x00);//AF6B
            str(0x0C, a);//AF6D
            ldam(0x0773);//AF6F
            cmp(a, 0x06);//AF72
            if (z)
                return;//AF74
            ldam(0x071F);//AF76
            if (!z)
                goto LabelAF8F;//AF79
            ldam(0x073D);//AF7B
            cmp(a, 0x20);//AF7E
            if (n) //
                return;//AF80
            ldam(0x073D);//AF82
            sbc(0x20);//AF85
            str(0x073D, a);//AF87
            lda(0x00);//AF8A
            str(0x0340, a);//AF8C
            LabelAF8F:
            Label92B0();//AF8F
        }

        void LabelAF67()
        {
            ldam(0x0A);//AF67
            str(0x0D, a);//AF69
            lda(0x00);//AF6B
            str(0x0C, a);//AF6D
            ldam(0x0773);//AF6F
            cmp(a, 0x06);//AF72
            if (z)
                return;//AF74
            ldam(0x071F);//AF76
            if (!z)
                goto LabelAF8F;//AF79
            ldam(0x073D);//AF7B
            cmp(a, 0x20);//AF7E
            if (n) //
                return;//AF80
            ldam(0x073D);//AF82
            sbc(0x20);//AF85
            str(0x073D, a);//AF87
            lda(0x00);//AF8A
            str(0x0340, a);//AF8C
            LabelAF8F:
            Label92B0();//AF8F
        }

        void LabelAF6F()
        {
            ldam(0x0773);//AF6F
            cmp(a, 0x06);//AF72
            if (z)
                return;//AF74
            ldam(0x071F);//AF76
            if (!z)
                goto LabelAF8F;//AF79
            ldam(0x073D);//AF7B
            cmp(a, 0x20);//AF7E
            if (n) //
                return;//AF80
            ldam(0x073D);//AF82
            sbc(0x20);//AF85
            str(0x073D, a);//AF87
            lda(0x00);//AF8A
            str(0x0340, a);//AF8C
            LabelAF8F:
            Label92B0();//AF8F
        }

        void LabelAF93()
        {
            ldam(0x06FF);//AF93
            c = false;//AF96
            adc(ram[0x03A1]);//AF97
            str(0x06FF, a);//AF9A
            ldam(0x0723);//AF9D
            if (!z)
                goto LabelAFFB;//AFA0
            ldam(0x0755);//AFA2
            cmp(a, 0x50);//AFA5
            if (!c)
                goto LabelAFFB;//AFA7
            ldam(0x0785);//AFA9
            if (!z)
                goto LabelAFFB;//AFAC
            ldym(0x06FF);//AFAE
            dey();//AFB1
            if (n) //
                goto LabelAFFB;//AFB2
            iny();//AFB4
            cmp(y, 0x02);//AFB5
            if (!c)
                goto LabelAFBA;//AFB7
            dey();//AFB9
            LabelAFBA:
            ldam(0x0755);//AFBA
            cmp(a, 0x70);//AFBD
            if (!c)
                goto LabelAFC4;//AFBF
            ldym(0x06FF);//AFC1
            LabelAFC4:
            tya();//AFC4
            str(0x0775, a);//AFC5
            c = false;//AFC8
            adc(ram[0x073D]);//AFC9
            str(0x073D, a);//AFCC
            tya();//AFCF
            c = false;//AFD0
            adc(ram[0x071C]);//AFD1
            str(0x071C, a);//AFD4
            str(0x073F, a);//AFD7
            ldam(0x071A);//AFDA
            adc(0x00);//AFDD
            str(0x071A, a);//AFDF
            and(0x01);//AFE2
            str(0x00, a);//AFE4
            ldam(0x0778);//AFE6
            and(0xFE);//AFE9
            orm(0x00);//AFEB
            str(0x0778, a);//AFED
            LabelB038();//AFF0
            lda(0x08);//AFF3
            str(0x0795, a);//AFF5
            goto LabelB000; //AFF8
            LabelAFFB:
            lda(0x00);//AFFB
            str(0x0775, a);//AFFD
            LabelB000:
            ldx(0x00);//B000
            LabelF1F6();//B002
            str(0x00, a);//B005
            ldy(0x00);//B007
            asl();//B009
            if (c)
                goto LabelB013;//B00A
            iny();//B00C
            ldam(0x00);//B00D
            and(0x20);//B00F
            if (z)
                goto LabelB02E;//B011
            LabelB013:
            ldam(0x071C + y);//B013
            c = true;//B016
            sbc(ram[0xB034 + y]);//B017
            str(0x86, a);//B01A
            ldam(0x071A + y);//B01C
            sbc(0x00);//B01F
            str(0x6D, a);//B021
            ldam(0x0C);//B023
            cmp(a, ram[0xB036 + y]);//B025
            if (z)
                goto LabelB02E;//B028
            lda(0x00);//B02A
            str(0x57, a);//B02C
            LabelB02E:
            lda(0x00);//B02E
            str(0x03A1, a);//B030
        }

        void LabelAFC4()
        {
            tya();//AFC4
            str(0x0775, a);//AFC5
            c = false;//AFC8
            adc(ram[0x073D]);//AFC9
            str(0x073D, a);//AFCC
            tya();//AFCF
            c = false;//AFD0
            adc(ram[0x071C]);//AFD1
            str(0x071C, a);//AFD4
            str(0x073F, a);//AFD7
            ldam(0x071A);//AFDA
            adc(0x00);//AFDD
            str(0x071A, a);//AFDF
            and(0x01);//AFE2
            str(0x00, a);//AFE4
            ldam(0x0778);//AFE6
            and(0xFE);//AFE9
            orm(0x00);//AFEB
            str(0x0778, a);//AFED
            LabelB038();//AFF0
            lda(0x08);//AFF3
            str(0x0795, a);//AFF5
            goto LabelB000; //AFF8
            //lda(0x00);//AFFB
            //str(0x0775, a);//AFFD
            LabelB000:
            ldx(0x00);//B000
            LabelF1F6();//B002
            str(0x00, a);//B005
            ldy(0x00);//B007
            asl();//B009
            if (c)
                goto LabelB013;//B00A
            iny();//B00C
            ldam(0x00);//B00D
            and(0x20);//B00F
            if (z)
                goto LabelB02E;//B011
            LabelB013:
            ldam(0x071C + y);//B013
            c = true;//B016
            sbc(ram[0xB034 + y]);//B017
            str(0x86, a);//B01A
            ldam(0x071A + y);//B01C
            sbc(0x00);//B01F
            str(0x6D, a);//B021
            ldam(0x0C);//B023
            cmp(a, ram[0xB036 + y]);//B025
            if (z)
                goto LabelB02E;//B028
            lda(0x00);//B02A
            str(0x57, a);//B02C
            LabelB02E:
            lda(0x00);//B02E
            str(0x03A1, a);//B030
        }

        void LabelB038()
        {
            ldam(0x071C);//B038
            c = false;//B03B
            adc(0xFF);//B03C
            str(0x071D, a);//B03E
            ldam(0x071A);//B041
            adc(0x00);//B044
            str(0x071B, a);//B046
        }

        void LabelB04A()
        {
            ldam(0x0E);//B04A
            switch (a)
            {
                case 0:
                    Label9131();
                    break;
                case 1:
                    LabelB1C7();
                    break;
                case 2:
                    LabelB206();
                    break;
                case 3:
                    LabelB1E5();
                    break;
                case 4:
                    LabelB2A4();
                    break;
                case 5:
                    LabelB2CA();
                    break;
                case 6:
                    Label91CD();
                    break;
                case 7:
                    LabelB069();
                    break;
                case 8:
                    LabelB0E9();
                    break;
                case 9:
                    LabelB233();
                    break;
                case 10:
                    LabelB245();
                    break;
                case 11:
                    LabelB269();
                    break;
                case 12:
                    LabelB27D();
                    break;
            }
        }

        void LabelB069()
        {
            ldam(0x0752);//B069
            cmp(a, 0x02);//B06C
            if (z)
                goto LabelB09B;//B06E
            lda(0x00);//B070
            ldym(0xCE);//B072
            cmp(y, 0x30);//B074
            if (!c)
            {
                LabelB0E6();//B076
                return;
            }
            ldam(0x0710);//B078
            cmp(a, 0x06);//B07B
            if (z)
                goto LabelB083;//B07D
            cmp(a, 0x07);//B07F
            if (!z)
                goto LabelB0D3;//B081
            LabelB083:
            ldam(0x03C4);//B083
            if (!z)
                goto LabelB08D;//B086
            lda(0x01);//B088
            LabelB0E6(); //B08A
            return;
            LabelB08D:
            LabelB21F();//B08D
            dec(0x06DE);//B090
            if (!z)
                return;//B093
            inc(0x0769);//B095
            LabelB315(); //B098
            return;

            LabelB09B:
            ldam(0x0758);//B09B
            if (!z)
                goto LabelB0AC;//B09E
            lda(0xFF);//B0A0
            LabelB200();//B0A2
            ldam(0xCE);//B0A5
            cmp(a, 0x91);//B0A7
            if (!c)
                goto LabelB0D3;//B0A9
            return;
            LabelB0AC:
            ldam(0x0399);//B0AC
            cmp(a, 0x60);//B0AF
            if (!z)
                return;//B0B1
            ldam(0xCE);//B0B3
            cmp(a, 0x99);//B0B5
            ldy(0x00);//B0B7
            lda(0x01);//B0B9
            if (!c)
                goto LabelB0C7;//B0BB
            lda(0x03);//B0BD
            str(0x1D, a);//B0BF
            iny();//B0C1
            lda(0x08);//B0C2
            str(0x05B4, a);//B0C4
            LabelB0C7:
            str(0x0716, y);//B0C7
            LabelB0E6();//B0CA
            ldam(0x86);//B0CD
            cmp(a, 0x48);//B0CF
            if (!c)
                return;//B0D1
            LabelB0D3:
            lda(0x08);//B0D3
            str(0x0E, a);//B0D5
            lda(0x01);//B0D7
            str(0x33, a);//B0D9
            lsr();//B0DB
            str(0x0752, a);//B0DC
            str(0x0716, a);//B0DF
            str(0x0758, a);//B0E2
        }

        void LabelB0E6()
        {
            str(0x06FC, a);//B0E6
            LabelB0E9();
        }

        void LabelB0E9()
        {
            ldam(0x0E);//B0E9
            cmp(a, 0x0B);//B0EB
            if (z)
                goto LabelB12B;//B0ED
            ldam(0x074E);//B0EF
            if (!z)
                goto LabelB104;//B0F2
            ldym(0xB5);//B0F4
            dey();//B0F6
            if (!z)
                goto LabelB0FF;//B0F7
            ldam(0xCE);//B0F9
            cmp(a, 0xD0);//B0FB
            if (!c)
                goto LabelB104;//B0FD
            LabelB0FF:
            lda(0x00);//B0FF
            str(0x06FC, a);//B101
            LabelB104:
            ldam(0x06FC);//B104
            and(0xC0);//B107
            str(0x0A, a);//B109
            ldam(0x06FC);//B10B
            and(0x03);//B10E
            str(0x0C, a);//B110
            ldam(0x06FC);//B112
            and(0x0C);//B115
            str(0x0B, a);//B117
            and(0x04);//B119
            if (z)
                goto LabelB12B;//B11B
            ldam(0x1D);//B11D
            if (!z)
                goto LabelB12B;//B11F
            ldym(0x0C);//B121
            if (z)
                goto LabelB12B;//B123
            lda(0x00);//B125
            str(0x0C, a);//B127
            str(0x0B, a);//B129
            LabelB12B:
            LabelB329();//B12B
            ldy(0x01);//B12E
            ldam(0x0754);//B130
            if (!z)
                goto LabelB13E;//B133
            ldy(0x00);//B135
            ldam(0x0714);//B137
            if (z)
                goto LabelB13E;//B13A
            ldy(0x02);//B13C
            LabelB13E:
            str(0x0499, y);//B13E
            lda(0x01);//B141
            ldym(0x57);//B143
            if (z)
                goto LabelB14C;//B145
            if (!n)
                goto LabelB14A;//B147
            asl();//B149
            LabelB14A:
            str(0x45, a);//B14A
            LabelB14C:
            LabelAF93();//B14C
            LabelF180();//B14F
            LabelF12A();//B152
            ldx(0x00);//B155
            LabelE29C();//B157
            LabelDC64();//B15A
            ldam(0xCE);//B15D
            cmp(a, 0x40);//B15F
            if (!c)
                goto LabelB179;//B161
            ldam(0x0E);//B163
            cmp(a, 0x05);//B165
            if (z)
                goto LabelB179;//B167
            cmp(a, 0x07);//B169
            if (z)
                goto LabelB179;//B16B
            cmp(a, 0x04);//B16D
            if (!c)
                goto LabelB179;//B16F
            ldam(0x03C4);//B171
            and(0xDF);//B174
            str(0x03C4, a);//B176
            LabelB179:
            ldam(0xB5);//B179
            cmp(a, 0x02);//B17B
            if (n) //
                return;//B17D
            ldx(0x01);//B17F
            str(0x0723, x);//B181
            ldy(0x04);//B184
            str(0x07, y);//B186
            ldx(0x00);//B188
            ldym(0x0759);//B18A
            if (!z)
                goto LabelB194;//B18D
            ldym(0x0743);//B18F
            if (!z)
                goto LabelB1AA;//B192
            LabelB194:
            inx();//B194
            ldym(0x0E);//B195
            cmp(y, 0x0B);//B197
            if (z)
                goto LabelB1AA;//B199
            ldym(0x0712);//B19B
            if (!z)
                goto LabelB1A6;//B19E
            iny();//B1A0
            str(0xFC, y);//B1A1
            str(0x0712, y);//B1A3
            LabelB1A6:
            ldy(0x06);//B1A6
            str(0x07, y);//B1A8
            LabelB1AA:
            cmp(a, ram[0x07]);//B1AA
            if (n) //
                return;//B1AC
            dex();//B1AE
            if (n) //
                goto LabelB1BB;//B1AF
            ldym(0x07B1);//B1B1
            if (!z)
                return;//B1B4
            lda(0x06);//B1B6
            str(0x0E, a);//B1B8
            return;
            LabelB1BB:
            lda(0x00);//B1BB
            str(0x0758, a);//B1BD
            LabelB1DD();//B1C0
            inc(0x0752);//B1C3
        }

        void LabelB1C7()
        {
            ldam(0xB5);//B1C7
            if (!z)
                goto LabelB1D1;//B1C9
            ldam(0xCE);//B1CB
            cmp(a, 0xE4);//B1CD
            if (!c)
                goto LabelB1DD;//B1CF
            LabelB1D1:
            lda(0x08);//B1D1
            str(0x0758, a);//B1D3
            ldy(0x03);//B1D6
            str(0x1D, y);//B1D8
            LabelB0E6(); //B1DA
            return;
            LabelB1DD:
            lda(0x02);//B1DD
            str(0x0752, a);//B1DF
            LabelB213(); //B1E2
            return;
        }

        void LabelB1DD()
        {
            lda(0x02);//B1DD
            str(0x0752, a);//B1DF
            LabelB213(); //B1E2
            return;
        }

        void LabelB1E5()
        {
            lda(0x01);//B1E5
            LabelB200();//B1E7
            LabelAF93();//B1EA
            ldy(0x00);//B1ED
            ldam(0x06D6);//B1EF
            if (!z)
            {
                LabelB20B();//B1F2
                return;
            }
            iny();//B1F4
            ldam(0x074E);//B1F5
            cmp(a, 0x03);//B1F8
            if (!z)
            {
                LabelB20B();//B1FA
                return;
            }
            iny();//B1FC
            LabelB20B(); //B1FD
            return;
        }

        void LabelB200()
        {
            c = false;//B200
            adc(ram[0xCE]);//B201
            str(0xCE, a);//B203
        }

        void LabelB206()
        {
            LabelB21F();//B206
            ldy(0x02);//B209
            dec(0x06DE);//B20B
            if (!z)
                return;//B20E
            str(0x0752, y);//B210
            inc(0x0774);//B213
            lda(0x00);//B216
            str(0x0772, a);//B218
            str(0x0722, a);//B21B
        }

        void LabelB20B()
        {
            dec(0x06DE);//B20B
            if (!z)
                return;//B20E
            str(0x0752, y);//B210
            inc(0x0774);//B213
            lda(0x00);//B216
            str(0x0772, a);//B218
            str(0x0722, a);//B21B
        }

        void LabelB213()
        {
            inc(0x0774);//B213
            lda(0x00);//B216
            str(0x0772, a);//B218
            str(0x0722, a);//B21B
        }

        void LabelB21F()
        {
            lda(0x08);//B21F
            str(0x57, a);//B221
            ldy(0x01);//B223
            ldam(0x86);//B225
            and(0x0F);//B227
            if (!z)
                goto LabelB22E;//B229
            str(0x57, a);//B22B
            tay();//B22D
            LabelB22E:
            tya();//B22E
            LabelB0E6();//B22F
        }

        void LabelB233()
        {
            ldam(0x0747);//B233
            cmp(a, 0xF8);//B236
            if (!z)
                goto LabelB23D;//B238
            LabelB255(); //B23A
            return;
            LabelB23D:
            cmp(a, 0xC4);//B23D
            if (!z)
                return;//B23F
            LabelB273();//B241
        }

        void LabelB245()
        {
            ldam(0x0747);//B245
            cmp(a, 0xF0);//B248
            if (c)
                goto LabelB253;//B24A
            cmp(a, 0xC8);//B24C
            if (z)
            {
                LabelB273();//B24E
                return;
            }
            LabelB0E9(); //B250
            return;
            LabelB253:
            if (!z)
                return;//B253
            ldym(0x070B);//B255
            if (!z)
                return;//B258
            str(0x070D, y);//B25A
            inc(0x070B);//B25D
            ldam(0x0754);//B260
            eor(0x01);//B263
            str(0x0754, a);//B265
        }

        void LabelB255()
        {
            ldym(0x070B);//B255
            if (!z)
                return;//B258
            str(0x070D, y);//B25A
            inc(0x070B);//B25D
            ldam(0x0754);//B260
            eor(0x01);//B263
            str(0x0754, a);//B265
        }

        void LabelB269()
        {
            ldam(0x0747);//B269
            cmp(a, 0xF0);//B26C
            if (c)
                return;//B26E
            LabelB0E9(); //B270
            return;
        }

        void LabelB273()
        {
            lda(0x00);//B273
            str(0x0747, a);//B275
            lda(0x08);//B278
            str(0x0E, a);//B27A
        }

        void LabelB27D()
        {
            ldam(0x0747);//B27D
            cmp(a, 0xC0);//B280
            if (z)
                goto LabelB297;//B282
            ldam(0x09);//B284
            lsr();//B286
            lsr();//B287
            and(0x03);//B288
            str(0x00, a);//B28A
            ldam(0x03C4);//B28C
            and(0xFC);//B28F
            orm(0x00);//B291
            str(0x03C4, a);//B293
            return;
            LabelB297:
            LabelB273();//B297
            ldam(0x03C4);//B29A
            and(0xFC);//B29D
            str(0x03C4, a);//B29F
        }

        void LabelB288()
        {
            and(0x03);//B288
            str(0x00, a);//B28A
            ldam(0x03C4);//B28C
            and(0xFC);//B28F
            orm(0x00);//B291
            str(0x03C4, a);//B293
            return;
        }

        void LabelB29A()
        {
            ldam(0x03C4);//B29A
            and(0xFC);//B29D
            str(0x03C4, a);//B29F
        }

        void LabelB2A4()
        {
            ldam(0x1B);//B2A4
            cmp(a, 0x30);//B2A6
            if (!z)
                goto LabelB2BF;//B2A8
            ldam(0x0713);//B2AA
            str(0xFF, a);//B2AD
            lda(0x00);//B2AF
            str(0x0713, a);//B2B1
            ldym(0xCE);//B2B4
            cmp(y, 0x9E);//B2B6
            if (c)
                goto LabelB2BC;//B2B8
            lda(0x04);//B2BA
            LabelB2BC:
            LabelB0E6(); //B2BC
            return;
            LabelB2BF:
            inc(0x0E);//B2BF
        }

        void LabelB2CA()
        {
            lda(0x01);//B2CA
            LabelB0E6();//B2CC
            ldam(0xCE);//B2CF
            cmp(a, 0xAE);//B2D1
            if (!c)
                goto LabelB2E3;//B2D3
            ldam(0x0723);//B2D5
            if (z)
                goto LabelB2E3;//B2D8
            lda(0x20);//B2DA
            str(0xFC, a);//B2DC
            lda(0x00);//B2DE
            str(0x0723, a);//B2E0
            LabelB2E3:
            ldam(0x0490);//B2E3
            lsr();//B2E6
            if (c)
                goto LabelB2F6;//B2E7
            ldam(0x0746);//B2E9
            if (!z)
                goto LabelB2F1;//B2EC
            inc(0x0746);//B2EE
            LabelB2F1:
            lda(0x20);//B2F1
            str(0x03C4, a);//B2F3
            LabelB2F6:
            ldam(0x0746);//B2F6
            cmp(a, 0x05);//B2F9
            if (!z)
                return;//B2FB
            inc(0x075C);//B2FD
            ldam(0x075C);//B300
            cmp(a, 0x03);//B303
            if (!z)
                goto LabelB315;//B305
            ldym(0x075F);//B307
            ldam(0x0748);//B30A
            cmp(a, ram[0xB2C2 + y]);//B30D
            if (!c)
                goto LabelB315;//B310
            inc(0x075D);//B312
            LabelB315:
            inc(0x0760);//B315
            Label9C03();//B318
            inc(0x0757);//B31B
            LabelB213();//B31E
            str(0x075B, a);//B321
            lda(0x80);//B324
            str(0xFC, a);//B326
        }

        void LabelB315()
        {
            inc(0x0760);//B315
            Label9C03();//B318
            inc(0x0757);//B31B
            LabelB213();//B31E
            str(0x075B, a);//B321
            lda(0x80);//B324
            str(0xFC, a);//B326
        }

        void LabelB329()
        {
            lda(0x00);//B329
            ldym(0x0754);//B32B
            if (!z)
                goto LabelB338;//B32E
            ldam(0x1D);//B330
            if (!z)
                goto LabelB33B;//B332
            ldam(0x0B);//B334
            and(0x04);//B336
            LabelB338:
            str(0x0714, a);//B338
            LabelB33B:
            LabelB450();//B33B
            ldam(0x070B);//B33E
            if (!z)
                return;//B341
            ldam(0x1D);//B343
            cmp(a, 0x03);//B345
            if (z)
                goto LabelB34E;//B347
            ldy(0x18);//B349
            str(0x0789, y);//B34B
            LabelB34E:
            switch (a)
            {
                case 0:
                    LabelB35A();
                    break;
                case 1:
                    LabelB376();
                    break;
                case 2:
                    LabelB36D();
                    break;
                case 3:
                    LabelB3CF();
                    break;
            }
        }

        void LabelB35A()
        {
            LabelB58F();//B35A
            ldam(0x0C);//B35D
            if (z)
                goto LabelB363;//B35F
            str(0x33, a);//B361
            LabelB363:
            LabelB5CC();//B363
            LabelBF09();//B366
            str(0x06FF, a);//B369
        }

        void LabelB36D()
        {
            ldam(0x070A);//B36D
            str(0x0709, a);//B370
            LabelB3AC(); //B373
            return;
        }

        void LabelB376()
        {
            ldym(0x9F);//B376
            if (!n)
                goto LabelB38D;//B378
            ldam(0x0A);//B37A
            and(0x80);//B37C
            and(ram[0x0D]);//B37E
            if (!z)
                goto LabelB393;//B380
            ldam(0x0708);//B382
            c = true;//B385
            sbc(ram[0xCE]);//B386
            cmp(a, ram[0x0706]);//B388
            if (!c)
                goto LabelB393;//B38B
            LabelB38D:
            ldam(0x070A);//B38D
            str(0x0709, a);//B390
            LabelB393:
            ldam(0x0704);//B393
            if (z)
                goto LabelB3AC;//B396
            LabelB58F();//B398
            ldam(0xCE);//B39B
            cmp(a, 0x14);//B39D
            if (c)
                goto LabelB3A6;//B39F
            lda(0x18);//B3A1
            str(0x0709, a);//B3A3
            LabelB3A6:
            ldam(0x0C);//B3A6
            if (z)
                goto LabelB3AC;//B3A8
            str(0x33, a);//B3AA
            LabelB3AC:
            ldam(0x0C);//B3AC
            if (z)
                goto LabelB3B3;//B3AE
            LabelB5CC();//B3B0
            LabelB3B3:
            LabelBF09();//B3B3
            str(0x06FF, a);//B3B6
            ldam(0x0E);//B3B9
            cmp(a, 0x0B);//B3BB
            if (!z)
                goto LabelB3C4;//B3BD
            lda(0x28);//B3BF
            str(0x0709, a);//B3C1
            LabelB3C4:
            LabelBF4D(); //B3C4
            return;
        }

        void LabelB3AC()
        {
            ldam(0x0C);//B3AC
            if (z)
                goto LabelB3B3;//B3AE
            LabelB5CC();//B3B0
            LabelB3B3:
            LabelBF09();//B3B3
            str(0x06FF, a);//B3B6
            ldam(0x0E);//B3B9
            cmp(a, 0x0B);//B3BB
            if (!z)
                goto LabelB3C4;//B3BD
            lda(0x28);//B3BF
            str(0x0709, a);//B3C1
            LabelB3C4:
            LabelBF4D(); //B3C4
            return;
        }

        void LabelB3CF()
        {
            ldam(0x0416);//B3CF
            c = false;//B3D2
            adc(ram[0x0433]);//B3D3
            str(0x0416, a);//B3D6
            ldy(0x00);//B3D9
            ldam(0x9F);//B3DB
            if (!n)
                goto LabelB3E0;//B3DD
            dey();//B3DF
            LabelB3E0:
            str(0x00, y);//B3E0
            adc(ram[0xCE]);//B3E2
            str(0xCE, a);//B3E4
            ldam(0xB5);//B3E6
            adc(ram[0x00]);//B3E8
            str(0xB5, a);//B3EA
            ldam(0x0C);//B3EC
            and(ram[0x0490]);//B3EE
            if (z)
                goto LabelB420;//B3F1
            ldym(0x0789);//B3F3
            if (!z)
                return;//B3F6
            ldy(0x18);//B3F8
            str(0x0789, y);//B3FA
            ldx(0x00);//B3FD
            ldym(0x33);//B3FF
            lsr();//B401
            if (c)
                goto LabelB406;//B402
            inx();//B404
            inx();//B405
            LabelB406:
            dey();//B406
            if (z)
                goto LabelB40A;//B407
            inx();//B409
            LabelB40A:
            ldam(0x86);//B40A
            c = false;//B40C
            adc(ram[0xB3C7 + x]);//B40D
            str(0x86, a);//B410
            ldam(0x6D);//B412
            adc(ram[0xB3CB + x]);//B414
            str(0x6D, a);//B417
            ldam(0x0C);//B419
            eor(0x03);//B41B
            str(0x33, a);//B41D
            return;
            LabelB420:
            str(0x0789, a);//B420
        }

        void LabelB450()
        {
            ldam(0x1D);//B450
            cmp(a, 0x03);//B452
            if (!z)
                goto LabelB479;//B454
            ldy(0x00);//B456
            ldam(0x0B);//B458
            and(ram[0x0490]);//B45A
            if (z)
                goto LabelB465;//B45D
            iny();//B45F
            and(0x08);//B460
            if (!z)
                goto LabelB465;//B462
            iny();//B464
            LabelB465:
            ldxm(0xB44D + y);//B465
            str(0x0433, x);//B468
            lda(0x08);//B46B
            ldxm(0xB44A + y);//B46D
            str(0x9F, x);//B470
            if (n) //
                goto LabelB475;//B472
            lsr();//B474
            LabelB475:
            str(0x070C, a);//B475
            return;
            LabelB479:
            ldam(0x070E);//B479
            if (!z)
                goto LabelB488;//B47C
            ldam(0x0A);//B47E
            and(0x80);//B480
            if (z)
                goto LabelB488;//B482
            and(ram[0x0D]);//B484
            if (z)
                goto LabelB48B;//B486
            LabelB488:
            goto LabelB51C; //B488
            LabelB48B:
            ldam(0x1D);//B48B
            if (z)
                goto LabelB4A0;//B48D
            ldam(0x0704);//B48F
            if (z)
                goto LabelB488;//B492
            ldam(0x0782);//B494
            if (!z)
                goto LabelB4A0;//B497
            ldam(0x9F);//B499
            if (!n)
                goto LabelB4A0;//B49B
            goto LabelB51C; //B49D
            LabelB4A0:
            lda(0x20);//B4A0
            str(0x0782, a);//B4A2
            ldy(0x00);//B4A5
            str(0x0416, y);//B4A7
            str(0x0433, y);//B4AA
            ldam(0xB5);//B4AD
            str(0x0707, a);//B4AF
            ldam(0xCE);//B4B2
            str(0x0708, a);//B4B4
            lda(0x01);//B4B7
            str(0x1D, a);//B4B9
            ldam(0x0700);//B4BB
            cmp(a, 0x09);//B4BE
            if (!c)
                goto LabelB4D2;//B4C0
            iny();//B4C2
            cmp(a, 0x10);//B4C3
            if (!c)
                goto LabelB4D2;//B4C5
            iny();//B4C7
            cmp(a, 0x19);//B4C8
            if (!c)
                goto LabelB4D2;//B4CA
            iny();//B4CC
            cmp(a, 0x1C);//B4CD
            if (!c)
                goto LabelB4D2;//B4CF
            iny();//B4D1
            LabelB4D2:
            lda(0x01);//B4D2
            str(0x0706, a);//B4D4
            ldam(0x0704);//B4D7
            if (z)
                goto LabelB4E4;//B4DA
            ldy(0x05);//B4DC
            ldam(0x047D);//B4DE
            if (z)
                goto LabelB4E4;//B4E1
            iny();//B4E3
            LabelB4E4:
            ldam(0xB424 + y);//B4E4
            str(0x0709, a);//B4E7
            ldam(0xB42B + y);//B4EA
            str(0x070A, a);//B4ED
            ldam(0xB439 + y);//B4F0
            str(0x0433, a);//B4F3
            ldam(0xB432 + y);//B4F6
            str(0x9F, a);//B4F9
            ldam(0x0704);//B4FB
            if (z)
                goto LabelB511;//B4FE
            lda(0x04);//B500
            str(0xFF, a);//B502
            ldam(0xCE);//B504
            cmp(a, 0x14);//B506
            if (c)
                goto LabelB51C;//B508
            lda(0x00);//B50A
            str(0x9F, a);//B50C
            goto LabelB51C; //B50E
            LabelB511:
            lda(0x01);//B511
            ldym(0x0754);//B513
            if (z)
                goto LabelB51A;//B516
            lda(0x80);//B518
            LabelB51A:
            str(0xFF, a);//B51A
            LabelB51C:
            ldy(0x00);//B51C
            str(0x00, y);//B51E
            LabelB520:
            ldam(0x1D);//B520
            if (z)
                goto LabelB52D;//B522
            ldam(0x0700);//B524
            cmp(a, 0x19);//B527
            if (c)
                goto LabelB55E;//B529
            if (!c)
                goto LabelB545;//B52B
            LabelB52D:
            iny();//B52D
            ldam(0x074E);//B52E
            if (z)
                goto LabelB545;//B531
            dey();//B533
            ldam(0x0C);//B534
            cmp(a, ram[0x45]);//B536
            if (!z)
                goto LabelB545;//B538
            ldam(0x0A);//B53A
            and(0x40);//B53C
            if (!z)
                goto LabelB559;//B53E
            ldam(0x0783);//B540
            if (!z)
                goto LabelB55E;//B543
            LabelB545:
            iny();//B545
            inc(0x00);//B546
            ldam(0x0703);//B548
            if (!z)
                goto LabelB554;//B54B
            ldam(0x0700);//B54D
            cmp(a, 0x21);//B550
            if (!c)
                goto LabelB55E;//B552
            LabelB554:
            inc(0x00);//B554
            goto LabelB55E; //B556
            LabelB559:
            lda(0x0A);//B559
            str(0x0783, a);//B55B
            LabelB55E:
            ldam(0xB440 + y);//B55E
            str(0x0450, a);//B561
            ldam(0x0E);//B564
            cmp(a, 0x07);//B566
            if (!z)
                goto LabelB56C;//B568
            ldy(0x03);//B56A
            LabelB56C:
            ldam(0xB443 + y);//B56C
            str(0x0456, a);//B56F
            ldym(0x00);//B572
            ldam(0xB447 + y);//B574
            str(0x0702, a);//B577
            lda(0x00);//B57A
            str(0x0701, a);//B57C
            ldam(0x33);//B57F
            cmp(a, ram[0x45]);//B581
            if (z)
                return;//B583
            aslm(0x0702);//B585
            rolm(0x0701);//B588
        }

        void LabelB58F()
        {
            ldy(0x00);//B58F
            ldam(0x0700);//B591
            cmp(a, 0x1C);//B594
            if (c)
                goto LabelB5AD;//B596
            iny();//B598
            cmp(a, 0x0E);//B599
            if (c)
                goto LabelB59E;//B59B
            iny();//B59D
            LabelB59E:
            ldam(0x06FC);//B59E
            and(0x7F);//B5A1
            if (z)
                goto LabelB5C5;//B5A3
            and(0x03);//B5A5
            cmp(a, ram[0x45]);//B5A7
            if (!z)
                goto LabelB5B3;//B5A9
            lda(0x00);//B5AB
            LabelB5AD:
            str(0x0703, a);//B5AD
            goto LabelB5C5; //B5B0
            LabelB5B3:
            ldam(0x0700);//B5B3
            cmp(a, 0x0B);//B5B6
            if (c)
                goto LabelB5C5;//B5B8
            ldam(0x33);//B5BA
            str(0x45, a);//B5BC
            lda(0x00);//B5BE
            str(0x57, a);//B5C0
            str(0x0705, a);//B5C2
            LabelB5C5:
            ldam(0xB58C + y);//B5C5
            str(0x070C, a);//B5C8
        }

        void LabelB5CC()
        {
            and(ram[0x0490]);//B5CC
            cmp(a, 0x00);//B5CF
            if (!z)
                goto LabelB5DB;//B5D1
            ldam(0x57);//B5D3
            if (z)
                goto LabelB620;//B5D5
            if (!n)
                goto LabelB5FC;//B5D7
            if (n) //
                goto LabelB5DE;//B5D9
            LabelB5DB:
            lsr();//B5DB
            if (!c)
                goto LabelB5FC;//B5DC
            LabelB5DE:
            ldam(0x0705);//B5DE
            c = false;//B5E1
            adc(ram[0x0702]);//B5E2
            str(0x0705, a);//B5E5
            ldam(0x57);//B5E8
            adc(ram[0x0701]);//B5EA
            str(0x57, a);//B5ED
            cmp(a, ram[0x0456]);//B5EF
            if (n) //
                goto LabelB617;//B5F2
            ldam(0x0456);//B5F4
            str(0x57, a);//B5F7
            goto LabelB620;//B5F9
            LabelB5FC:
            ldam(0x0705);//B5FC
            c = true;//B5FF
            sbc(ram[0x0702]);//B600
            str(0x0705, a);//B603
            ldam(0x57);//B606
            sbc(ram[0x0701]);//B608
            str(0x57, a);//B60B
            cmp(a, ram[0x0450]);//B60D
            if (!n)
                goto LabelB617;//B610
            ldam(0x0450);//B612
            str(0x57, a);//B615
            LabelB617:
            cmp(a, 0x00);//B617
            if (!n)
                goto LabelB620;//B619
            eor(0xFF);//B61B
            c = false;//B61D
            adc(0x01);//B61E
            LabelB620:
            str(0x0700, a);//B620
        }

        void LabelB624()
        {
            ldam(0x0756);//B624
            cmp(a, 0x02);//B627
            if (!c)
                goto LabelB66E;//B629
            ldam(0x0A);//B62B
            and(0x40);//B62D
            if (z)
                goto LabelB664;//B62F
            and(ram[0x0D]);//B631
            if (!z)
                goto LabelB664;//B633
            ldam(0x06CE);//B635
            and(0x01);//B638
            tax();//B63A
            ldam(0x24 + x);//B63B
            if (!z)
                goto LabelB664;//B63D
            ldym(0xB5);//B63F
            dey();//B641
            if (!z)
                goto LabelB664;//B642
            ldam(0x0714);//B644
            if (!z)
                goto LabelB664;//B647
            ldam(0x1D);//B649
            cmp(a, 0x03);//B64B
            if (z)
                goto LabelB664;//B64D
            lda(0x20);//B64F
            str(0xFF, a);//B651
            lda(0x02);//B653
            str(0x24 + x, a);//B655
            ldym(0x070C);//B657
            str(0x0711, y);//B65A
            dey();//B65D
            str(0x0781, y);//B65E
            inc(0x06CE);//B661
            LabelB664:
            ldx(0x00);//B664
            LabelB689();//B666
            ldx(0x01);//B669
            LabelB689();//B66B
            LabelB66E:
            ldam(0x074E);//B66E
            if (!z)
                return;//B671
            ldx(0x02);//B673
            LabelB675:
            str(0x08, x);//B675
            LabelB6F9();//B677
            LabelF131();//B67A
            LabelF191();//B67D
            LabelEDE1();//B680
            dex();//B683
            if (!n)
                goto LabelB675;//B684
        }

        void LabelB689()
        {
            str(0x08, x);//B689
            ldam(0x24 + x);//B68B
            asl();//B68D
            if (c)
                goto LabelB6F3;//B68E
            ldym(0x24 + x);//B690
            if (z)
                return;//B692
            dey();//B694
            if (z)
                goto LabelB6BE;//B695
            ldam(0x86);//B697
            adc(0x04);//B699
            str(0x8D + x, a);//B69B
            ldam(0x6D);//B69D
            adc(0x00);//B69F
            str(0x74 + x, a);//B6A1
            ldam(0xCE);//B6A3
            str(0xD5 + x, a);//B6A5
            lda(0x01);//B6A7
            str(0xBC + x, a);//B6A9
            ldym(0x33);//B6AB
            dey();//B6AD
            ldam(0xB687 + y);//B6AE
            str(0x5E + x, a);//B6B1
            lda(0x04);//B6B3
            str(0xA6 + x, a);//B6B5
            lda(0x07);//B6B7
            str(0x04A0 + x, a);//B6B9
            dec(0x24 + x);//B6BC
            LabelB6BE:
            txa();//B6BE
            c = false;//B6BF
            adc(0x07);//B6C0
            tax();//B6C2
            lda(0x50);//B6C3
            str(0x00, a);//B6C5
            lda(0x03);//B6C7
            str(0x02, a);//B6C9
            lda(0x00);//B6CB
            LabelBFD7();//B6CD
            LabelBF0F();//B6D0
            ldxm(0x08);//B6D3
            LabelF13B();//B6D5
            LabelF187();//B6D8
            LabelE22D();//B6DB
            LabelE1C8();//B6DE
            ldam(0x03D2);//B6E1
            and(0xCC);//B6E4
            if (!z)
                goto LabelB6EE;//B6E6
            LabelD6D9();//B6E8
            LabelECDE(); //B6EB
            return;
            LabelB6EE:
            lda(0x00);//B6EE
            str(0x24 + x, a);//B6F0
            return;
            LabelB6F3:
            LabelF13B();//B6F3
            LabelED09(); //B6F6
            return;
        }

        void LabelB6F9()
        {
            ldam(0x07A8 + x);//B6F9
            and(0x01);//B6FC
            str(0x07, a);//B6FE
            ldam(0xE4 + x);//B700
            cmp(a, 0xF8);//B702
            if (!z)
                goto LabelB732;//B704
            ldam(0x0792);//B706
            if (!z)
                return;//B709
            ldy(0x00);//B70B
            ldam(0x33);//B70D
            lsr();//B70F
            if (!c)
                goto LabelB714;//B710
            ldy(0x08);//B712
            LabelB714:
            tya();//B714
            adc(ram[0x86]);//B715
            str(0x9C + x, a);//B717
            ldam(0x6D);//B719
            adc(0x00);//B71B
            str(0x83 + x, a);//B71D
            ldam(0xCE);//B71F
            c = false;//B721
            adc(0x08);//B722
            str(0xE4 + x, a);//B724
            lda(0x01);//B726
            str(0xCB + x, a);//B728
            ldym(0x07);//B72A
            ldam(0xB74D + y);//B72C
            str(0x0792, a);//B72F
            LabelB732:
            ldym(0x07);//B732
            ldam(0x042C + x);//B734
            c = true;//B737
            sbc(ram[0xB74B + y]);//B738
            str(0x042C + x, a);//B73B
            ldam(0xE4 + x);//B73E
            sbc(0x00);//B740
            cmp(a, 0x20);//B742
            if (c)
                goto LabelB748;//B744
            lda(0xF8);//B746
            LabelB748:
            str(0xE4 + x, a);//B748
        }

        void LabelB70B()
        {
            ldy(0x00);//B70B
            ldam(0x33);//B70D
            lsr();//B70F
            if (!c)
                goto LabelB714;//B710
            ldy(0x08);//B712
            LabelB714:
            tya();//B714
            adc(ram[0x86]);//B715
            str(0x9C + x, a);//B717
            ldam(0x6D);//B719
            adc(0x00);//B71B
            str(0x83 + x, a);//B71D
            ldam(0xCE);//B71F
            c = false;//B721
            adc(0x08);//B722
            str(0xE4 + x, a);//B724
            lda(0x01);//B726
            str(0xCB + x, a);//B728
            ldym(0x07);//B72A
            ldam(0xB74D + y);//B72C
            str(0x0792, a);//B72F
            ldym(0x07);//B732
            ldam(0x042C + x);//B734
            c = true;//B737
            sbc(ram[0xB74B + y]);//B738
            str(0x042C + x, a);//B73B
            ldam(0xE4 + x);//B73E
            sbc(0x00);//B740
            cmp(a, 0x20);//B742
            if (c)
                goto LabelB748;//B744
            lda(0xF8);//B746
            LabelB748:
            str(0xE4 + x, a);//B748
        }

        void LabelB74F()
        {
            ldam(0x0770);//B74F
            if (z)
                return;//B752
            ldam(0x0E);//B754
            cmp(a, 0x08);//B756
            if (!c)
                return;//B758
            cmp(a, 0x0B);//B75A
            if (z)
                return;//B75C
            ldam(0xB5);//B75E
            cmp(a, 0x02);//B760
            if (c)
                return;//B762
            ldam(0x0787);//B764
            if (!z)
                return;//B767
            ldam(0x07F8);//B769
            orm(0x07F9);//B76C
            orm(0x07FA);//B76F
            if (z)
                goto LabelB79A;//B772
            ldym(0x07F8);//B774
            dey();//B777
            if (!z)
                goto LabelB786;//B778
            ldam(0x07F9);//B77A
            orm(0x07FA);//B77D
            if (!z)
                goto LabelB786;//B780
            lda(0x40);//B782
            str(0xFC, a);//B784
            LabelB786:
            lda(0x18);//B786
            str(0x0787, a);//B788
            ldy(0x23);//B78B
            lda(0xFF);//B78D
            str(0x0139, a);//B78F
            Label8F5F();//B792
            lda(0xA4);//B795
            Label8F06(); //B797
            return;
            LabelB79A:
            str(0x0756, a);//B79A
            LabelD931();//B79D
            inc(0x0759);//B7A0
        }

        void LabelB7A4()
        {
            ldam(0x0723);//B7A4
            if (z)
                return;//B7A7
            LabelB7A9:
            ldam(0xCE);//B7A9
            and(ram[0xB5]);//B7AB
            if (!z)
                return;//B7AD
            str(0x0723, a);//B7AF
            inc(0x06D6);//B7B2
            LabelC998(); //B7B5
            return;
        }

        void LabelB7B8()
        {
            ldam(0x074E);//B7B8
            if (!z)
                return;//B7BB
            str(0x047D, a);//B7BD
            ldam(0x0747);//B7C0
            if (!z)
                return;//B7C3
            ldy(0x04);//B7C5
            LabelB7C7:
            ldam(0x0471 + y);//B7C7
            c = false;//B7CA
            adc(ram[0x0477 + y]);//B7CB
            str(0x02, a);//B7CE
            ldam(0x046B + y);//B7D0
            if (z)
                goto LabelB7F1;//B7D3
            adc(0x00);//B7D5
            str(0x01, a);//B7D7
            ldam(0x86);//B7D9
            c = true;//B7DB
            sbc(ram[0x0471 + y]);//B7DC
            ldam(0x6D);//B7DF
            sbc(ram[0x046B + y]);//B7E1
            if (n) //
                goto LabelB7F1;//B7E4
            ldam(0x02);//B7E6
            c = true;//B7E8
            sbc(ram[0x86]);//B7E9
            ldam(0x01);//B7EB
            sbc(ram[0x6D]);//B7ED
            if (!n)
                goto LabelB7F5;//B7EF
            LabelB7F1:
            dey();//B7F1
            if (!n)
                goto LabelB7C7;//B7F2
            return;
            LabelB7F5:
            ldam(0x0477 + y);//B7F5
            lsr();//B7F8
            str(0x00, a);//B7F9
            ldam(0x0471 + y);//B7FB
            c = false;//B7FE
            adc(ram[0x00]);//B7FF
            str(0x01, a);//B801
            ldam(0x046B + y);//B803
            adc(0x00);//B806
            str(0x00, a);//B808
            ldam(0x09);//B80A
            lsr();//B80C
            if (!c)
                goto LabelB83B;//B80D
            ldam(0x01);//B80F
            c = true;//B811
            sbc(ram[0x86]);//B812
            ldam(0x00);//B814
            sbc(ram[0x6D]);//B816
            if (!n)
                goto LabelB828;//B818
            ldam(0x86);//B81A
            c = true;//B81C
            sbc(0x01);//B81D
            str(0x86, a);//B81F
            ldam(0x6D);//B821
            sbc(0x00);//B823
            goto LabelB839; //B825
            LabelB828:
            ldam(0x0490);//B828
            lsr();//B82B
            if (!c)
                goto LabelB83B;//B82C
            ldam(0x86);//B82E
            c = false;//B830
            adc(0x01);//B831
            LabelB833:
            str(0x86, a);//B833
            ldam(0x6D);//B835
            adc(0x00);//B837
            LabelB839:
            str(0x6D, a);//B839
            LabelB83B:
            lda(0x10);//B83B
            str(0x00, a);//B83D
            lda(0x01);//B83F
            str(0x047D, a);//B841
            str(0x02, a);//B844
            lsr();//B846
            tax();//B847
            LabelBFD7(); //B848
            return;
        }

        void LabelB855()
        {
            ldx(0x05);//B855
            str(0x08, x);//B857
            ldam(0x16 + x);//B859
            cmp(a, 0x30);//B85B
            if (!z)
                return;//B85D
            ldam(0x0E);//B85F
            cmp(a, 0x04);//B861
            if (!z)
                goto LabelB896;//B863
            ldam(0x1D);//B865
            cmp(a, 0x03);//B867
            if (!z)
                goto LabelB896;//B869
            ldam(0xCF + x);//B86B
            cmp(a, 0xAA);//B86D
            if (c)
                goto LabelB899;//B86F
            ldam(0xCE);//B871
            cmp(a, 0xA2);//B873
            if (c)
                goto LabelB899;//B875
            ldam(0x0417 + x);//B877
            adc(0xFF);//B87A
            str(0x0417 + x, a);//B87C
            ldam(0xCF + x);//B87F
            adc(0x01);//B881
            str(0xCF + x, a);//B883
            ldam(0x010E);//B885
            c = true;//B888
            sbc(0xFF);//B889
            str(0x010E, a);//B88B
            ldam(0x010D);//B88E
            sbc(0x01);//B891
            str(0x010D, a);//B893
            LabelB896:
            goto LabelB8AC; //B896
            LabelB899:
            ldym(0x010F);//B899
            ldam(0xB84B + y);//B89C
            ldxm(0xB850 + y);//B89F
            str(0x0134 + x, a);//B8A2
            LabelBC27();//B8A5
            lda(0x05);//B8A8
            str(0x0E, a);//B8AA
            LabelB8AC:
            LabelF1AF();//B8AC
            LabelF152();//B8AF
            LabelE54B();//B8B2
        }

        void LabelB8BA()
        {
            LabelF1AF();//B8BA
            ldam(0x0747);//B8BD
            if (!z)
                goto LabelB902;//B8C0
            ldam(0x070E);//B8C2
            if (z)
                goto LabelB902;//B8C5
            tay();//B8C7
            dey();//B8C8
            tya();//B8C9
            and(0x02);//B8CA
            if (!z)
                goto LabelB8D5;//B8CC
            inc(0xCE);//B8CE
            inc(0xCE);//B8D0
            goto LabelB8D9; //B8D2
            LabelB8D5:
            dec(0xCE);//B8D5
            dec(0xCE);//B8D7
            LabelB8D9:
            ldam(0x58 + x);//B8D9
            c = false;//B8DB
            adc(ram[0xB8B6 + y]);//B8DC
            str(0xCF + x, a);//B8DF
            cmp(y, 0x01);//B8E1
            if (!c)
                goto LabelB8F4;//B8E3
            ldam(0x0A);//B8E5
            and(0x80);//B8E7
            if (z)
                goto LabelB8F4;//B8E9
            and(ram[0x0D]);//B8EB
            if (!z)
                goto LabelB8F4;//B8ED
            lda(0xF4);//B8EF
            str(0x06DB, a);//B8F1
            LabelB8F4:
            cmp(y, 0x03);//B8F4
            if (!z)
                goto LabelB902;//B8F6
            ldam(0x06DB);//B8F8
            str(0x9F, a);//B8FB
            lda(0x00);//B8FD
            str(0x070E, a);//B8FF
            LabelB902:
            LabelF152();//B902
            LabelE87D();//B905
            LabelD67A();//B908
            ldam(0x070E);//B90B
            if (z)
                return;//B90E
            ldam(0x0786);//B910
            if (!z)
                return;//B913
            lda(0x04);//B915
            str(0x0786, a);//B917
            inc(0x070E);//B91A
        }

        void LabelB91E()
        {
            lda(0x2F);//B91E
            str(0x16 + x, a);//B920
            lda(0x01);//B922
            str(0x0F + x, a);//B924
            ldam(0x0076 + y);//B926
            str(0x6E + x, a);//B929
            ldam(0x008F + y);//B92B
            str(0x87 + x, a);//B92E
            ldam(0x00D7 + y);//B930
            str(0xCF + x, a);//B933
            ldym(0x0398);//B935
            if (!z)
                goto LabelB93D;//B938
            str(0x039D, a);//B93A
            LabelB93D:
            txa();//B93D
            str(0x039A + y, a);//B93E
            inc(0x0398);//B941
            lda(0x04);//B944
            str(0xFE, a);//B946
        }

        void LabelB94B()
        {
            cmp(x, 0x05);//B94B
            if (!z)
                goto LabelB9B7;//B94D
            ldym(0x0398);//B94F
            dey();//B952
            ldam(0x0399);//B953
            cmp(a, ram[0xB949 + y]);//B956
            if (z)
                goto LabelB96A;//B959
            ldam(0x09);//B95B
            lsr();//B95D
            lsr();//B95E
            if (!c)
                goto LabelB96A;//B95F
            ldam(0xD4);//B961
            sbc(0x01);//B963
            str(0xD4, a);//B965
            inc(0x0399);//B967
            LabelB96A:
            ldam(0x0399);//B96A
            cmp(a, 0x08);//B96D
            if (!c)
                goto LabelB9B7;//B96F
            LabelF152();//B971
            LabelF1AF();//B974
            ldy(0x00);//B977
            LabelB979:
            LabelE435();//B979
            iny();//B97C
            cmp(y, ram[0x0398]);//B97D
            if (!z)
                goto LabelB979;//B980
            ldam(0x03D1);//B982
            and(0x0C);//B985
            if (z)
                goto LabelB999;//B987
            dey();//B989
            LabelB98A:
            ldxm(0x039A + y);//B98A
            LabelC998();//B98D
            dey();//B990
            if (!n)
                goto LabelB98A;//B991
            str(0x0398, a);//B993
            str(0x0399, a);//B996
            LabelB999:
            ldam(0x0399);//B999
            cmp(a, 0x20);//B99C
            if (!c)
                goto LabelB9B7;//B99E
            ldx(0x06);//B9A0
            lda(0x01);//B9A2
            ldy(0x1B);//B9A4
            LabelE3F0();//B9A6
            ldym(0x02);//B9A9
            cmp(y, 0xD0);//B9AB
            if (c)
                goto LabelB9B7;//B9AD
            ldam(W(0x06) + y);//B9AF
            if (!z)
                goto LabelB9B7;//B9B1
            lda(0x26);//B9B3
            str(W(0x06) + y, a);//B9B5
            LabelB9B7:
            ldxm(0x08);//B9B7
        }

        void LabelB9BC()
        {
            ldam(0x074E);//B9BC
            if (z)
                return;//B9BF
            ldx(0x02);//B9C1
            LabelB9C3:
            str(0x08, x);//B9C3
            ldam(0x0F + x);//B9C5
            if (!z)
                goto LabelBA1A;//B9C7
            ldam(0x07A8 + x);//B9C9
            ldym(0x06CC);//B9CC
            and(ram[0xB9BA + y]);//B9CF
            cmp(a, 0x06);//B9D2
            if (c)
                goto LabelBA1A;//B9D4
            tay();//B9D6
            ldam(0x046B + y);//B9D7
            if (z)
                goto LabelBA1A;//B9DA
            ldam(0x047D + y);//B9DC
            if (z)
                goto LabelB9E9;//B9DF
            sbc(0x00);//B9E1
            str(0x047D + y, a);//B9E3
            goto LabelBA1A; //B9E6
            LabelB9E9:
            ldam(0x0747);//B9E9
            if (!z)
                goto LabelBA1A;//B9EC
            LabelB9EE:
            lda(0x0E);//B9EE
            str(0x047D + y, a);//B9F0
            ldam(0x046B + y);//B9F3
            str(0x6E + x, a);//B9F6
            ldam(0x0471 + y);//B9F8
            str(0x87 + x, a);//B9FB
            ldam(0x0477 + y);//B9FD
            c = true;//BA00
            sbc(0x08);//BA01
            str(0xCF + x, a);//BA03
            lda(0x01);//BA05
            str(0xB6 + x, a);//BA07
            str(0x0F + x, a);//BA09
            lsr();//BA0B
            str(0x1E + x, a);//BA0C
            lda(0x09);//BA0E
            str(0x049A + x, a);//BA10
            lda(0x33);//BA13
            str(0x16 + x, a);//BA15
            goto LabelBA2D; //BA17
            LabelBA1A:
            ldam(0x16 + x);//BA1A
            cmp(a, 0x33);//BA1C
            if (!z)
                goto LabelBA2D;//BA1E
            LabelD67A();//BA20
            ldam(0x0F + x);//BA23
            if (z)
                goto LabelBA2D;//BA25
            LabelF1AF();//BA27
            LabelBA33();//BA2A
            LabelBA2D:
            dex();//BA2D
            if (!n)
                goto LabelB9C3;//BA2E
        }

        void LabelB9C3()
        {
            LabelB9C3:
            str(0x08, x);//B9C3
            ldam(0x0F + x);//B9C5
            if (!z)
                goto LabelBA1A;//B9C7
            ldam(0x07A8 + x);//B9C9
            ldym(0x06CC);//B9CC
            and(ram[0xB9BA + y]);//B9CF
            cmp(a, 0x06);//B9D2
            if (c)
                goto LabelBA1A;//B9D4
            tay();//B9D6
            ldam(0x046B + y);//B9D7
            if (z)
                goto LabelBA1A;//B9DA
            ldam(0x047D + y);//B9DC
            if (z)
                goto LabelB9E9;//B9DF
            sbc(0x00);//B9E1
            str(0x047D + y, a);//B9E3
            goto LabelBA1A; //B9E6
            LabelB9E9:
            ldam(0x0747);//B9E9
            if (!z)
                goto LabelBA1A;//B9EC
            LabelB9EE:
            lda(0x0E);//B9EE
            str(0x047D + y, a);//B9F0
            ldam(0x046B + y);//B9F3
            str(0x6E + x, a);//B9F6
            ldam(0x0471 + y);//B9F8
            str(0x87 + x, a);//B9FB
            ldam(0x0477 + y);//B9FD
            c = true;//BA00
            sbc(0x08);//BA01
            str(0xCF + x, a);//BA03
            lda(0x01);//BA05
            str(0xB6 + x, a);//BA07
            str(0x0F + x, a);//BA09
            lsr();//BA0B
            str(0x1E + x, a);//BA0C
            lda(0x09);//BA0E
            str(0x049A + x, a);//BA10
            lda(0x33);//BA13
            str(0x16 + x, a);//BA15
            goto LabelBA2D; //BA17
            LabelBA1A:
            ldam(0x16 + x);//BA1A
            cmp(a, 0x33);//BA1C
            if (!z)
                goto LabelBA2D;//BA1E
            LabelD67A();//BA20
            ldam(0x0F + x);//BA23
            if (z)
                goto LabelBA2D;//BA25
            LabelF1AF();//BA27
            LabelBA33();//BA2A
            LabelBA2D:
            dex();//BA2D
            if (!n)
                goto LabelB9C3;//BA2E
        }

        void LabelBA33()
        {
            ldam(0x0747);//BA33
            if (!z)
                goto LabelBA76;//BA36
            ldam(0x1E + x);//BA38
            if (!z)
                goto LabelBA6A;//BA3A
            ldam(0x03D1);//BA3C
            and(0x0C);//BA3F
            cmp(a, 0x0C);//BA41
            if (z)
                goto LabelBA85;//BA43
            ldy(0x01);//BA45
            LabelE143();//BA47
            if (n) //
                goto LabelBA4D;//BA4A
            iny();//BA4C
            LabelBA4D:
            str(0x46 + x, y);//BA4D
            dey();//BA4F
            ldam(0xBA31 + y);//BA50
            str(0x58 + x, a);//BA53
            ldam(0x00);//BA55
            adc(0x28);//BA57
            cmp(a, 0x50);//BA59
            if (!c)
                goto LabelBA85;//BA5B
            lda(0x01);//BA5D
            str(0x1E + x, a);//BA5F
            lda(0x0A);//BA61
            str(0x078A + x, a);//BA63
            lda(0x08);//BA66
            str(0xFE, a);//BA68
            LabelBA6A:
            ldam(0x1E + x);//BA6A
            and(0x20);//BA6C
            if (z)
                goto LabelBA73;//BA6E
            LabelBF63();//BA70
            LabelBA73:
            LabelBF02();//BA73
            LabelBA76:
            LabelF1AF();//BA76
            LabelF152();//BA79
            LabelE243();//BA7C
            LabelD853();//BA7F
            LabelE87D(); //BA82
            return;
            LabelBA85:
            LabelC998();//BA85
        }

        void LabelBA94()
        {
            ldam(0x07A8);//BA94
            and(0x07);//BA97
            if (!z)
                goto LabelBAA0;//BA99
            ldam(0x07A8);//BA9B
            and(0x08);//BA9E
            LabelBAA0:
            tay();//BAA0
            ldam(0x002A + y);//BAA1
            if (!z)
                goto LabelBABF;//BAA4
            ldxm(0xBA89 + y);//BAA6
            ldam(0x0F + x);//BAA9
            if (!z)
                goto LabelBABF;//BAAB
            ldxm(0x08);//BAAD
            txa();//BAAF
            str(0x06AE + y, a);//BAB0
            lda(0x90);//BAB3
            str(0x002A + y, a);//BAB5
            lda(0x07);//BAB8
            str(0x04A2 + y, a);//BABA
            c = true;//BABD
            return;
            LabelBABF:
            ldxm(0x08);//BABF
            c = false;//BAC1
        }

        void LabelBAC3()
        {
            ldam(0x0747);//BAC3
            if (!z)
                goto LabelBB2B;//BAC6
            ldam(0x2A + x);//BAC8
            and(0x7F);//BACA
            ldym(0x06AE + x);//BACC
            cmp(a, 0x02);//BACF
            if (z)
                goto LabelBAF3;//BAD1
            if (c)
                goto LabelBB09;//BAD3
            txa();//BAD5
            c = false;//BAD6
            adc(0x0D);//BAD7
            tax();//BAD9
            lda(0x10);//BADA
            str(0x00, a);//BADC
            lda(0x0F);//BADE
            str(0x01, a);//BAE0
            lda(0x04);//BAE2
            str(0x02, a);//BAE4
            lda(0x00);//BAE6
            LabelBFD7();//BAE8
            LabelBF0F();//BAEB
            ldxm(0x08);//BAEE
            goto LabelBB28; //BAF0
            LabelBAF3:
            lda(0xFE);//BAF3
            str(0xAC + x, a);//BAF5
            ldam(0x001E + y);//BAF7
            and(0xF7);//BAFA
            str(0x001E + y, a);//BAFC
            ldxm(0x46 + y);//BAFF
            dex();//BB01
            ldam(0xBA92 + x);//BB02
            ldxm(0x08);//BB05
            str(0x64 + x, a);//BB07
            LabelBB09:
            dec(0x2A + x);//BB09
            ldam(0x0087 + y);//BB0B
            c = false;//BB0E
            adc(0x02);//BB0F
            str(0x93 + x, a);//BB11
            ldam(0x006E + y);//BB13
            adc(0x00);//BB16
            str(0x7A + x, a);//BB18
            ldam(0x00CF + y);//BB1A
            c = true;//BB1D
            sbc(0x0A);//BB1E
            str(0xDB + x, a);//BB20
            lda(0x01);//BB22
            LabelBB24:
            str(0xC2 + x, a);//BB24
            if (!z)
                goto LabelBB2B;//BB26
            LabelBB28:
            LabelD7C4();//BB28
            LabelBB2B:
            LabelF19B();//BB2B
            LabelF148();//BB2E
            LabelE236();//BB31
            LabelE4DC();//BB34
        }

        void LabelBB38()
        {
            LabelBB84();//BB38
            ldam(0x76 + x);//BB3B
            str(0x007A + y, a);//BB3D
            ldam(0x8F + x);//BB40
            or(0x05);//BB42
            str(0x0093 + y, a);//BB44
            ldam(0xD7 + x);//BB47
            sbc(0x10);//BB49
            str(0x00DB + y, a);//BB4B
            LabelBB6C(); //BB4E
            return;
        }

        void LabelBB51()
        {
            LabelBB84();//BB51
            ldam(0x03EA + x);//BB54
            str(0x007A + y, a);//BB57
            ldam(0x06);//BB5A
            asl();//BB5C
            asl();//BB5D
            asl();//BB5E
            asl();//BB5F
            or(0x05);//BB60
            str(0x0093 + y, a);//BB62
            ldam(0x02);//BB65
            adc(0x20);//BB67
            str(0x00DB + y, a);//BB69
            lda(0xFB);//BB6C
            str(0x00AC + y, a);//BB6E
            lda(0x01);//BB71
            str(0x00C2 + y, a);//BB73
            str(0x002A + y, a);//BB76
            str(0xFE, a);//BB79
            str(0x08, x);//BB7B
            LabelBBFE();//BB7D
            inc(0x0748);//BB80
        }

        void LabelBB6C()
        {
            lda(0xFB);//BB6C
            str(0x00AC + y, a);//BB6E
            lda(0x01);//BB71
            str(0x00C2 + y, a);//BB73
            str(0x002A + y, a);//BB76
            str(0xFE, a);//BB79
            str(0x08, x);//BB7B
            LabelBBFE();//BB7D
            inc(0x0748);//BB80
        }

        void LabelBB84()
        {
            ldy(0x08);//BB84
            LabelBB86:
            ldam(0x002A + y);//BB86
            if (z)
                goto LabelBB92;//BB89
            dey();//BB8B
            cmp(y, 0x05);//BB8C
            if (!z)
                goto LabelBB86;//BB8E
            ldy(0x08);//BB90
            LabelBB92:
            str(0x06B7, y);//BB92
        }

        void LabelBB96()
        {
            ldx(0x08);//BB96
            str(0x08, x);//BB98
            ldam(0x2A + x);//BB9A
            if (z)
            {
                LabelBBF4();//BB9C
                return;
            }
            asl();//BB9E
            if (!c)
            {
                LabelBBA7();//BB9F
                return;
            }
            LabelBAC3();//BBA1
            LabelBBF4(); //BBA4
            return;
        }

        void LabelBB98()
        {
            str(0x08, x);//BB98
            ldam(0x2A + x);//BB9A
            if (z)
            {
                LabelBBF4();//BB9C
                return;
            }
            asl();//BB9E
            if (!c)
            {
                LabelBBA7();//BB9F
                return;
            }
            LabelBAC3();//BBA1
            LabelBBF4(); //BBA4
            return;
        }

        void LabelBBA7()
        {
            ldym(0x2A + x);//BBA7
            dey();//BBA9
            if (z)
                goto LabelBBC9;//BBAA
            inc(0x2A + x);//BBAC
            ldam(0x93 + x);//BBAE
            c = false;//BBB0
            adc(ram[0x0775]);//BBB1
            str(0x93 + x, a);//BBB4
            ldam(0x7A + x);//BBB6
            adc(0x00);//BBB8
            str(0x7A + x, a);//BBBA
            ldam(0x2A + x);//BBBC
            cmp(a, 0x30);//BBBE
            if (!z)
                goto LabelBBE8;//BBC0
            lda(0x00);//BBC2
            str(0x2A + x, a);//BBC4
            LabelBBF4(); //BBC6
            return;
            LabelBBC9:
            txa();//BBC9
            c = false;//BBCA
            adc(0x0D);//BBCB
            tax();//BBCD
            lda(0x50);//BBCE
            str(0x00, a);//BBD0
            lda(0x06);//BBD2
            str(0x02, a);//BBD4
            lsr();//BBD6
            str(0x01, a);//BBD7
            lda(0x00);//BBD9
            LabelBFD7();//BBDB
            ldxm(0x08);//BBDE
            ldam(0xAC + x);//BBE0
            cmp(a, 0x05);//BBE2
            if (!z)
                goto LabelBBE8;//BBE4
            inc(0x2A + x);//BBE6
            LabelBBE8:
            LabelF148();//BBE8
            LabelF19B();//BBEB
            LabelE236();//BBEE
            LabelE686();//BBF1
            dex();//BBF4
            if (!n)
                LabelBB98();//BBF5
        }

        void LabelBBF4()
        {
            dex();//BBF4
            if (!n)
                LabelBB98();//BBF5
        }

        void LabelBBFE()
        {
            lda(0x01);//BBFE
            str(0x0139, a);//BC00
            ldxm(0x0753);//BC03
            ldym(0xBBF8 + x);//BC06
            Label8F5F();//BC09
            inc(0x075E);//BC0C
            ldam(0x075E);//BC0F
            cmp(a, 0x64);//BC12
            if (!z)
                goto LabelBC22;//BC14
            lda(0x00);//BC16
            str(0x075E, a);//BC18
            inc(0x075A);//BC1B
            lda(0x40);//BC1E
            str(0xFE, a);//BC20
            LabelBC22:
            lda(0x02);//BC22
            str(0x0138, a);//BC24
            ldxm(0x0753);//BC27
            ldym(0xBBFA + x);//BC2A
            Label8F5F();//BC2D
            ldym(0x0753);//BC30
            ldam(0xBBFC + y);//BC33
            Label8F06();//BC36
            ldym(0x0300);//BC39
            ldam(0x02FB + y);//BC3C
            if (!z)
                goto LabelBC46;//BC3F
            lda(0x24);//BC41
            str(0x02FB + y, a);//BC43
            LabelBC46:
            ldxm(0x08);//BC46
        }

        void LabelBC27()
        {
            ldxm(0x0753);//BC27
            ldym(0xBBFA + x);//BC2A
            Label8F5F();//BC2D
            ldym(0x0753);//BC30
            ldam(0xBBFC + y);//BC33
            Label8F06();//BC36
            ldym(0x0300);//BC39
            ldam(0x02FB + y);//BC3C
            if (!z)
                goto LabelBC46;//BC3F
            lda(0x24);//BC41
            str(0x02FB + y, a);//BC43
            LabelBC46:
            ldxm(0x08);//BC46
        }

        void LabelBC30()
        {
            ldym(0x0753);//BC30
            ldam(0xBBFC + y);//BC33
            Label8F06();//BC36
            ldym(0x0300);//BC39
            ldam(0x02FB + y);//BC3C
            if (!z)
                goto LabelBC46;//BC3F
            lda(0x24);//BC41
            str(0x02FB + y, a);//BC43
            LabelBC46:
            ldxm(0x08);//BC46
        }

        void LabelBC36()
        {
            Label8F06();//BC36
            ldym(0x0300);//BC39
            ldam(0x02FB + y);//BC3C
            if (!z)
                goto LabelBC46;//BC3F
            lda(0x24);//BC41
            str(0x02FB + y, a);//BC43
            LabelBC46:
            ldxm(0x08);//BC46
        }

        void LabelBC49()
        {
            lda(0x2E);//BC49
            str(0x1B, a);//BC4B
            ldam(0x76 + x);//BC4D
            str(0x73, a);//BC4F
            ldam(0x8F + x);//BC51
            str(0x8C, a);//BC53
            lda(0x01);//BC55
            str(0xBB, a);//BC57
            ldam(0xD7 + x);//BC59
            c = true;//BC5B
            sbc(0x08);//BC5C
            str(0xD4, a);//BC5E
            lda(0x01);//BC60
            str(0x23, a);//BC62
            str(0x14, a);//BC64
            lda(0x03);//BC66
            str(0x049F, a);//BC68
            ldam(0x39);//BC6B
            cmp(a, 0x02);//BC6D
            if (c)
                goto LabelBC7B;//BC6F
            ldam(0x0756);//BC71
            cmp(a, 0x02);//BC74
            if (!c)
                goto LabelBC79;//BC76
            lsr();//BC78
            LabelBC79:
            str(0x39, a);//BC79
            LabelBC7B:
            lda(0x20);//BC7B
            str(0x03CA, a);//BC7D
            lda(0x02);//BC80
            str(0xFE, a);//BC82
        }

        void LabelBC60()
        {
            lda(0x01);//BC60
            str(0x23, a);//BC62
            str(0x14, a);//BC64
            lda(0x03);//BC66
            str(0x049F, a);//BC68
            ldam(0x39);//BC6B
            cmp(a, 0x02);//BC6D
            if (c)
                goto LabelBC7B;//BC6F
            ldam(0x0756);//BC71
            cmp(a, 0x02);//BC74
            if (!c)
                goto LabelBC79;//BC76
            lsr();//BC78
            LabelBC79:
            str(0x39, a);//BC79
            LabelBC7B:
            lda(0x20);//BC7B
            str(0x03CA, a);//BC7D
            lda(0x02);//BC80
            str(0xFE, a);//BC82
        }

        void LabelBC85()
        {
            ldx(0x05);//BC85
            str(0x08, x);//BC87
            ldam(0x23);//BC89
            if (z)
                return;//BC8B
            asl();//BC8D
            if (!c)
                goto LabelBCB3;//BC8E
            ldam(0x0747);//BC90
            if (!z)
                goto LabelBCD8;//BC93
            ldam(0x39);//BC95
            if (z)
                goto LabelBCAA;//BC97
            cmp(a, 0x03);//BC99
            if (z)
                goto LabelBCAA;//BC9B
            cmp(a, 0x02);//BC9D
            if (!z)
                goto LabelBCD8;//BC9F
            LabelCAF9();//BCA1
            LabelE163();//BCA4
            goto LabelBCD8; //BCA7
            LabelBCAA:
            LabelCA77();//BCAA
            LabelDFC1();//BCAD
            goto LabelBCD8; //BCB0
            LabelBCB3:
            ldam(0x09);//BCB3
            and(0x03);//BCB5
            if (!z)
                goto LabelBCD2;//BCB7
            dec(0xD4);//BCB9
            ldam(0x23);//BCBB
            inc(0x23);//BCBD
            cmp(a, 0x11);//BCBF
            if (!c)
                goto LabelBCD2;//BCC1
            lda(0x10);//BCC3
            str(0x58 + x, a);//BCC5
            lda(0x80);//BCC7
            str(0x23, a);//BCC9
            asl();//BCCB
            str(0x03CA, a);//BCCC
            rol();//BCCF
            str(0x46 + x, a);//BCD0
            LabelBCD2:
            ldam(0x23);//BCD2
            cmp(a, 0x06);//BCD4
            if (!c)
                return;//BCD6
            LabelBCD8:
            LabelF152();//BCD8
            LabelF1AF();//BCDB
            LabelBCDE:
            LabelE243();//BCDE
            LabelE6D2();//BCE1
            LabelD853();//BCE4
            LabelBCE7:
            LabelD67A();//BCE7
        }

        void LabelBCED()
        {
            pha();//BCED
            lda(0x11);//BCEE
            ldxm(0x03EE);//BCF0
            ldym(0x0754);//BCF3
            if (!z)
                goto LabelBCFA;//BCF6
            lda(0x12);//BCF8
            LabelBCFA:
            str(0x26 + x, a);//BCFA
            Label8A6B();//BCFC
            ldxm(0x03EE);//BCFF
            ldam(0x02);//BD02
            str(0x03E4 + x, a);//BD04
            tay();//BD07
            ldam(0x06);//BD08
            str(0x03E6 + x, a);//BD0A
            ldam(W(0x06) + y);//BD0D
            LabelBDF6();//BD0F
            str(0x00, a);//BD12
            ldym(0x0754);//BD14
            if (!z)
                goto LabelBD1A;//BD17
            tya();//BD19
            LabelBD1A:
            if (!c)
                goto LabelBD41;//BD1A
            ldy(0x11);//BD1C
            str(0x26 + x, y);//BD1E
            lda(0xC4);//BD20
            ldym(0x00);//BD22
            cmp(y, 0x58);//BD24
            if (z)
                goto LabelBD2C;//BD26
            cmp(y, 0x5D);//BD28
            if (!z)
                goto LabelBD41;//BD2A
            LabelBD2C:
            ldam(0x06BC);//BD2C
            if (!z)
                goto LabelBD39;//BD2F
            lda(0x0B);//BD31
            str(0x079D, a);//BD33
            inc(0x06BC);//BD36
            LabelBD39:
            ldam(0x079D);//BD39
            if (!z)
                goto LabelBD40;//BD3C
            ldy(0xC4);//BD3E
            LabelBD40:
            tya();//BD40
            LabelBD41:
            str(0x03E8 + x, a);//BD41
            LabelBD84();//BD44
            ldym(0x02);//BD47
            lda(0x23);//BD49
            str(W(0x06) + y, a);//BD4B
            lda(0x10);//BD4D
            str(0x0784, a);//BD4F
            pla();//BD52
            str(0x05, a);//BD53
            ldy(0x00);//BD55
            ldam(0x0714);//BD57
            if (!z)
                goto LabelBD61;//BD5A
            ldam(0x0754);//BD5C
            if (z)
                goto LabelBD62;//BD5F
            LabelBD61:
            iny();//BD61
            LabelBD62:
            ldam(0xCE);//BD62
            c = false;//BD64
            adc(ram[0xBCEB + y]);//BD65
            and(0xF0);//BD68
            str(0xD7 + x, a);//BD6A
            ldym(0x26 + x);//BD6C
            cmp(y, 0x11);//BD6E
            if (z)
                goto LabelBD78;//BD70
            LabelBE02();//BD72
            goto LabelBD7B; //BD75
            LabelBD78:
            LabelBD9B();//BD78
            LabelBD7B:
            ldam(0x03EE);//BD7B
            eor(0x01);//BD7E
            str(0x03EE, a);//BD80
        }

        void LabelBD84()
        {
            ldam(0x86);//BD84
            c = false;//BD86
            adc(0x08);//BD87
            and(0xF0);//BD89
            str(0x8F + x, a);//BD8B
            ldam(0x6D);//BD8D
            adc(0x00);//BD8F
            str(0x76 + x, a);//BD91
            str(0x03EA + x, a);//BD93
            ldam(0xB5);//BD96
            str(0xBE + x, a);//BD98
        }

        void LabelBD9B()
        {
            LabelBE1F();//BD9B
            lda(0x02);//BD9E
            str(0xFF, a);//BDA0
            lda(0x00);//BDA2
            str(0x60 + x, a);//BDA4
            str(0x043C + x, a);//BDA6
            str(0x9F, a);//BDA9
            lda(0xFE);//BDAB
            str(0xA8 + x, a);//BDAD
            ldam(0x05);//BDAF
            LabelBDF6();//BDB1
            if (!c)
                return;//BDB4
            tya();//BDB6
            cmp(a, 0x09);//BDB7
            if (!c)
                goto LabelBDBD;//BDB9
            sbc(0x05);//BDBB

            LabelBDBD:
            switch (a)
            {
                case 0:
                    LabelBDD2();
                    break;
                case 1:
                    LabelBB38();
                    break;
                case 2:
                    LabelBB38();
                    break;
                case 3:
                    LabelBDD8();
                    break;
                case 4:
                    LabelBDD2();
                    break;
                case 5:
                    LabelBDDF();
                    break;
                case 6:
                    LabelBDD5();
                    break;
                case 7:
                    LabelBB38();
                    break;
                case 8:
                    LabelBDD8();
                    break;
            }
        }

        void LabelBDD2()
        {
            lda(0x00);//BDD2
            LabelBDDA();
        }

        void LabelBDD5()
        {
            lda(0x02);//BDD5
            LabelBDDA();
        }

        void LabelBDD8()
        {
            lda(0x03);//BDD8
            LabelBDDA();
        }
        void LabelBDDA()
        {
            str(0x39, a);//BDDA
            LabelBC49(); //BDDC
            return;
        }

        void LabelBDDF()
        {
            ldx(0x05);//BDDF
            ldym(0x03EE);//BDE1
            LabelB91E();//BDE4
        }

        void LabelBDF6()
        {
            ldy(0x0D);//BDF6
            LabelBDF8:
            cmp(a, ram[0xBDE8 + y]);//BDF8
            if (z)
                return;//BDFB
            dey();//BDFD
            if (!n)
                goto LabelBDF8;//BDFE
            c = false;//BE00
        }

        void LabelBE02()
        {
            LabelBE1F();//BE02
            lda(0x01);//BE05
            str(0x03EC + x, a);//BE07
            str(0xFD, a);//BE0A
            LabelBE41();//BE0C
            lda(0xFE);//BE0F
            str(0x9F, a);//BE11
            lda(0x05);//BE13
            str(0x0139, a);//BE15
            LabelBC27();//BE18
            ldxm(0x03EE);//BE1B
        }

        void LabelBE1F()
        {
            ldxm(0x03EE);//BE1F
            ldym(0x02);//BE22
            if (z)
                return;//BE24
            tya();//BE26
            c = true;//BE27
            sbc(0x10);//BE28
            str(0x02, a);//BE2A
            tay();//BE2C
            ldam(W(0x06) + y);//BE2D
            cmp(a, 0xC2);//BE2F
            if (!z)
                return;//BE31
            lda(0x00);//BE33
            str(W(0x06) + y, a);//BE35
            Label8A4D();//BE37
            ldxm(0x03EE);//BE3A
            LabelBB51();//BE3D
        }

        void LabelBE41()
        {
            ldam(0x8F + x);//BE41
            str(0x03F1 + x, a);//BE43
            lda(0xF0);//BE46
            str(0x60 + x, a);//BE48
            str(0x62 + x, a);//BE4A
            lda(0xFA);//BE4C
            str(0xA8 + x, a);//BE4E
            lda(0xFC);//BE50
            str(0xAA + x, a);//BE52
            lda(0x00);//BE54
            str(0x043C + x, a);//BE56
            str(0x043E + x, a);//BE59
            ldam(0x76 + x);//BE5C
            str(0x78 + x, a);//BE5E
            ldam(0x8F + x);//BE60
            str(0x91 + x, a);//BE62
            ldam(0xD7 + x);//BE64
            c = false;//BE66
            adc(0x08);//BE67
            str(0xD9 + x, a);//BE69
            lda(0xFA);//BE6B
            str(0xA8 + x, a);//BE6D
        }

        void LabelBE70()
        {
            ldam(0x26 + x);//BE70
            if (z)
                goto LabelBED1;//BE72
            and(0x0F);//BE74
            pha();//BE76
            tay();//BE77
            txa();//BE78
            c = false;//BE79
            adc(0x09);//BE7A
            tax();//BE7C
            dey();//BE7D
            if (z)
                goto LabelBEB3;//BE7E
            LabelBFA4();//BE80
            LabelBF0F();//BE83
            txa();//BE86
            c = false;//BE87
            adc(0x02);//BE88
            tax();//BE8A
            LabelBFA4();//BE8B
            LabelBF0F();//BE8E
            ldxm(0x08);//BE91
            LabelF159();//BE93
            LabelF1B6();//BE96
            LabelEC53();//BE99
            pla();//BE9C
            ldym(0xBE + x);//BE9D
            if (z)
                goto LabelBED1;//BE9F
            pha();//BEA1
            lda(0xF0);//BEA2
            cmp(a, ram[0xD9 + x]);//BEA4
            if (c)
                goto LabelBEAA;//BEA6
            str(0xD9 + x, a);//BEA8
            LabelBEAA:
            ldam(0xD7 + x);//BEAA
            cmp(a, 0xF0);//BEAC
            pla();//BEAE
            if (!c)
                goto LabelBED1;//BEAF
            if (c)
                goto LabelBECF;//BEB1
            LabelBEB3:
            LabelBFA4();//BEB3
            ldxm(0x08);//BEB6
            LabelF159();//BEB8
            LabelF1B6();//BEBB
            LabelEBD1();//BEBE
            ldam(0xD7 + x);//BEC1
            and(0x0F);//BEC3
            cmp(a, 0x05);//BEC5
            pla();//BEC7
            if (c)
                goto LabelBED1;//BEC8
            lda(0x01);//BECA
            str(0x03EC + x, a);//BECC
            LabelBECF:
            lda(0x00);//BECF
            LabelBED1:
            str(0x26 + x, a);//BED1
        }

        void LabelBED4()
        {
            ldx(0x01);//BED4
            LabelBED6:
            str(0x08, x);//BED6
            ldam(0x0301);//BED8
            if (!z)
                goto LabelBEFE;//BEDB
            ldam(0x03EC + x);//BEDD
            if (z)
                goto LabelBEFE;//BEE0
            ldam(0x03E6 + x);//BEE2
            str(0x06, a);//BEE5
            lda(0x05);//BEE7
            str(0x07, a);//BEE9
            ldam(0x03E4 + x);//BEEB
            str(0x02, a);//BEEE
            tay();//BEF0
            ldam(0x03E8 + x);//BEF1
            str(W(0x06) + y, a);//BEF4
            Label8A61();//BEF6
            lda(0x00);//BEF9
            str(0x03EC + x, a);//BEFB
            LabelBEFE:
            dex();//BEFE
            if (!n)
                goto LabelBED6;//BEFF
        }

        void LabelBF02()
        {
            inx();//BF02
            LabelBF0F();//BF03
            ldxm(0x08);//BF06
        }

        void LabelBF09()
        {
            ldam(0x070E);//BF09
            if (!z)
                return;//BF0C
            tax();//BF0E
            ldam(0x57 + x);//BF0F
            asl();//BF11
            asl();//BF12
            asl();//BF13
            asl();//BF14
            str(0x01, a);//BF15
            ldam(0x57 + x);//BF17
            lsr();//BF19
            lsr();//BF1A
            lsr();//BF1B
            lsr();//BF1C
            cmp(a, 0x08);//BF1D
            if (!c)
                goto LabelBF23;//BF1F
            or(0xF0);//BF21
            LabelBF23:
            str(0x00, a);//BF23
            ldy(0x00);//BF25
            cmp(a, 0x00);//BF27
            if (!n)
                goto LabelBF2C;//BF29
            dey();//BF2B
            LabelBF2C:
            str(0x02, y);//BF2C
            ldam(0x0400 + x);//BF2E
            c = false;//BF31
            adc(ram[0x01]);//BF32
            str(0x0400 + x, a);//BF34
            lda(0x00);//BF37
            rol();//BF39
            pha();//BF3A
            ror();//BF3B
            ldam(0x86 + x);//BF3C
            adc(ram[0x00]);//BF3E
            str(0x86 + x, a);//BF40
            ldam(0x6D + x);//BF42
            adc(ram[0x02]);//BF44
            str(0x6D + x, a);//BF46
            pla();//BF48
            c = false;//BF49
            adc(ram[0x00]);//BF4A
        }

        void LabelBF0F()
        {
            ldam(0x57 + x);//BF0F
            asl();//BF11
            asl();//BF12
            asl();//BF13
            asl();//BF14
            str(0x01, a);//BF15
            ldam(0x57 + x);//BF17
            lsr();//BF19
            lsr();//BF1A
            lsr();//BF1B
            lsr();//BF1C
            cmp(a, 0x08);//BF1D
            if (!c)
                goto LabelBF23;//BF1F
            or(0xF0);//BF21
            LabelBF23:
            str(0x00, a);//BF23
            ldy(0x00);//BF25
            cmp(a, 0x00);//BF27
            if (!n)
                goto LabelBF2C;//BF29
            dey();//BF2B
            LabelBF2C:
            str(0x02, y);//BF2C
            ldam(0x0400 + x);//BF2E
            c = false;//BF31
            adc(ram[0x01]);//BF32
            str(0x0400 + x, a);//BF34
            lda(0x00);//BF37
            rol();//BF39
            pha();//BF3A
            ror();//BF3B
            ldam(0x86 + x);//BF3C
            adc(ram[0x00]);//BF3E
            str(0x86 + x, a);//BF40
            ldam(0x6D + x);//BF42
            adc(ram[0x02]);//BF44
            str(0x6D + x, a);//BF46
            pla();//BF48
            c = false;//BF49
            adc(ram[0x00]);//BF4A
        }

        void LabelBF4D()
        {
            ldx(0x00);//BF4D
            ldam(0x0747);//BF4F
            if (!z)
                goto LabelBF59;//BF52
            ldam(0x070E);//BF54
            if (!z)
                return;//BF57
            LabelBF59:
            ldam(0x0709);//BF59
            str(0x00, a);//BF5C
            lda(0x04);//BF5E
            LabelBFAD(); //BF60
            return;
        }

        void LabelBF63()
        {
            ldy(0x3D);//BF63
            ldam(0x1E + x);//BF65
            cmp(a, 0x05);//BF67
            if (!z)
                goto LabelBF6D;//BF69
            ldy(0x20);//BF6B
            LabelBF6D:
            LabelBF94(); //BF6D
            return;
        }

        void LabelBF6B()
        {
            ldy(0x20);//BF6B
            LabelBF6D:
            LabelBF94(); //BF6D
            return;
        }

        void LabelBF70()
        {
            ldy(0x00);//BF70
            LabelBF77(); //BF72
            return;
        }

        void LabelBF75()
        {
            ldy(0x01);//BF75
            inx();//BF77
            lda(0x03);//BF78
            str(0x00, a);//BF7A
            lda(0x06);//BF7C
            str(0x01, a);//BF7E
            lda(0x02);//BF80
            str(0x02, a);//BF82
            tya();//BF84
            LabelBFD1(); //BF85
            return;
        }

        void LabelBF77()
        {
            inx();//BF77
            lda(0x03);//BF78
            str(0x00, a);//BF7A
            lda(0x06);//BF7C
            str(0x01, a);//BF7E
            lda(0x02);//BF80
            str(0x02, a);//BF82
            tya();//BF84
            LabelBFD1(); //BF85
            return;
        }

        void LabelBF88()
        {
            ldy(0x7F);//BF88
            if (!z)
                goto LabelBF8E;//BF8A
            ldy(0x0F);//BF8C
            LabelBF8E:
            lda(0x02);//BF8E
            if (!z)
                goto LabelBF96;//BF90
            ldy(0x1C);//BF92
            lda(0x03);//BF94
            LabelBF96:
            str(0x00, y);//BF96
            inx();//BF98
            LabelBFAD();//BF99
            ldxm(0x08);//BF9C
        }

        void LabelBF96()
        {
            str(0x00, y);//BF96
            inx();//BF98
            LabelBFAD();//BF99
            ldxm(0x08);//BF9C
        }

        void LabelBF8C()
        {
            ldy(0x0F);//BF8C
            lda(0x02);//BF8E
            if (!z)
                goto LabelBF96;//BF90
            ldy(0x1C);//BF92
            lda(0x03);//BF94
            LabelBF96:
            str(0x00, y);//BF96
            inx();//BF98
            LabelBFAD();//BF99
            ldxm(0x08);//BF9C
        }

        void LabelBF92()
        {
            ldy(0x1C);//BF92
            lda(0x03);//BF94
            str(0x00, y);//BF96
            inx();//BF98
            LabelBFAD();//BF99
            ldxm(0x08);//BF9C
        }

        void LabelBF94()
        {
            lda(0x03);//BF94
            str(0x00, y);//BF96
            inx();//BF98
            LabelBFAD();//BF99
            ldxm(0x08);//BF9C
        }

        void LabelBFA1()
        {
            ldy(0x00);//BFA1
            ldy(0x01);//BFA4
            lda(0x50);//BFA6
            str(0x00, a);//BFA8
            ldam(0xBF9F + y);//BFAA
            str(0x02, a);//BFAD
            lda(0x00);//BFAF
            LabelBFD7(); //BFB1
            return;
        }

        void LabelBFA4()
        {
            ldy(0x01);//BFA4
            lda(0x50);//BFA6
            str(0x00, a);//BFA8
            ldam(0xBF9F + y);//BFAA
            str(0x02, a);//BFAD
            lda(0x00);//BFAF
            LabelBFD7(); //BFB1
            return;
        }

        void LabelBFAD()
        {
            str(0x02, a);//BFAD
            lda(0x00);//BFAF
            LabelBFD7(); //BFB1
            return;
        }

        void LabelBFB4()
        {
            lda(0x00);//BFB4
            LabelBFB9();
        }
        void LabelBFB7()
        {
            lda(0x01);//BFB7
            LabelBFB9();
        }

        void LabelBFB9()
        { 
            pha();//BFB9
            ldym(0x16 + x);//BFBA
            inx();//BFBC
            lda(0x05);//BFBD
            cmp(y, 0x29);//BFBF
            if (!z)
                goto LabelBFC5;//BFC1
            lda(0x09);//BFC3
            LabelBFC5:
            str(0x00, a);//BFC5
            lda(0x0A);//BFC7
            str(0x01, a);//BFC9
            lda(0x03);//BFCB
            str(0x02, a);//BFCD
            pla();//BFCF
            tay();//BFD0
            LabelBFD7();//BFD1
            ldxm(0x08);//BFD4
        }

        void LabelBFD1()
        {
            LabelBFD7();//BFD1
            ldxm(0x08);//BFD4
        }

        void LabelBFD7()
        {
            pha();//BFD7
            ldam(0x0416 + x);//BFD8
            c = false;//BFDB
            adc(ram[0x0433 + x]);//BFDC
            str(0x0416 + x, a);//BFDF
            ldy(0x00);//BFE2
            ldam(0x9F + x);//BFE4
            if (!n)
                goto LabelBFE9;//BFE6
            dey();//BFE8
            LabelBFE9:
            str(0x07, y);//BFE9
            adc(ram[0xCE + x]);//BFEB
            str(0xCE + x, a);//BFED
            ldam(0xB5 + x);//BFEF
            adc(ram[0x07]);//BFF1
            str(0xB5 + x, a);//BFF3
            ldam(0x0433 + x);//BFF5
            c = false;//BFF8
            adc(ram[0x00]);//BFF9
            str(0x0433 + x, a);//BFFB
            ldam(0x9F + x);//BFFE
            adc(0x00);//C000
            str(0x9F + x, a);//C002
            cmp(a, ram[0x02]);//C004
            if (n) //
                goto LabelC018;//C006
            ldam(0x0433 + x);//C008
            cmp(a, 0x80);//C00B
            if (!c)
                goto LabelC018;//C00D
            ldam(0x02);//C00F
            str(0x9F + x, a);//C011
            lda(0x00);//C013
            str(0x0433 + x, a);//C015
            LabelC018:
            pla();//C018
            if (z)
                return;//C019
            ldam(0x02);//C01B
            eor(0xFF);//C01D
            tay();//C01F
            iny();//C020
            str(0x07, y);//C021
            ldam(0x0433 + x);//C023
            c = true;//C026
            sbc(ram[0x01]);//C027
            str(0x0433 + x, a);//C029
            ldam(0x9F + x);//C02C
            sbc(0x00);//C02E
            str(0x9F + x, a);//C030
            cmp(a, ram[0x07]);//C032
            if (!n)
                return;//C034
            ldam(0x0433 + x);//C036
            cmp(a, 0x80);//C039
            if (c)
                return;//C03B
            ldam(0x07);//C03D
            str(0x9F + x, a);//C03F
            lda(0xFF);//C041
            str(0x0433 + x, a);//C043
        }

        void LabelC047()
        {
            ldam(0x0F + x);//C047
            pha();//C049
            asl();//C04A
            if (c)
                goto LabelC05F;//C04B
            pla();//C04D
            if (z)
                goto LabelC053;//C04E
            LabelC882(); //C050
            return;
            LabelC053:
            ldam(0x071F);//C053
            and(0x07);//C056
            cmp(a, 0x07);//C058
            if (z)
                return;//C05A
            LabelC0CC(); //C05C
            return;
            LabelC05F:
            pla();//C05F
            and(0x0F);//C060
            tay();//C062
            ldam(0x000F + y);//C063
            if (!z)
                return;//C066
            str(0x0F + x, a);//C068
        }

        void LabelC08C()
        {
            ldam(0x6D);//C08C
            c = true;//C08E
            sbc(0x04);//C08F
            str(0x6D, a);//C091
            ldam(0x0725);//C093
            c = true;//C096
            sbc(0x04);//C097
            str(0x0725, a);//C099
            ldam(0x071A);//C09C
            c = true;//C09F
            sbc(0x04);//C0A0
            str(0x071A, a);//C0A2
            ldam(0x071B);//C0A5
            c = true;//C0A8
            sbc(0x04);//C0A9
            str(0x071B, a);//C0AB
            ldam(0x072A);//C0AE
            c = true;//C0B1
            sbc(0x04);//C0B2
            str(0x072A, a);//C0B4
            lda(0x00);//C0B7
            str(0x073B, a);//C0B9
            str(0x072B, a);//C0BC
            str(0x0739, a);//C0BF
            str(0x073A, a);//C0C2
            ldam(0x9BF8 + y);//C0C5
            str(0x072C, a);//C0C8
        }

        void LabelC0CC()
        {
            ldam(0x0745);//C0CC
            if (z)
                goto LabelC12F;//C0CF
            ldam(0x0726);//C0D1
            if (!z)
                goto LabelC12F;//C0D4
            ldy(0x0B);//C0D6
            LabelC0D8:
            dey();//C0D8
            if (n) //
                goto LabelC12F;//C0D9
            ldam(0x075F);//C0DB
            cmp(a, ram[0xC06B + y]);//C0DE
            if (!z)
                goto LabelC0D8;//C0E1
            ldam(0x0725);//C0E3
            cmp(a, ram[0xC076 + y]);//C0E6
            if (!z)
                goto LabelC0D8;//C0E9
            ldam(0xCE);//C0EB
            cmp(a, ram[0xC081 + y]);//C0ED
            if (!z)
                goto LabelC115;//C0F0
            ldam(0x1D);//C0F2
            cmp(a, 0x00);//C0F4
            if (!z)
                goto LabelC115;//C0F6
            ldam(0x075F);//C0F8
            cmp(a, 0x06);//C0FB
            if (!z)
                goto LabelC122;//C0FD
            inc(0x06D9);//C0FF
            LabelC102:
            inc(0x06DA);//C102
            ldam(0x06DA);//C105
            cmp(a, 0x03);//C108
            if (!z)
                goto LabelC12A;//C10A
            ldam(0x06D9);//C10C
            cmp(a, 0x03);//C10F
            if (z)
                goto LabelC122;//C111
            if (!z)
                goto LabelC11C;//C113
            LabelC115:
            ldam(0x075F);//C115
            cmp(a, 0x06);//C118
            if (z)
                goto LabelC102;//C11A
            LabelC11C:
            LabelC08C();//C11C
            LabelD071();//C11F
            LabelC122:
            lda(0x00);//C122
            str(0x06DA, a);//C124
            str(0x06D9, a);//C127
            LabelC12A:
            lda(0x00);//C12A
            str(0x0745, a);//C12C
            LabelC12F:
            ldam(0x06CD);//C12F
            if (z)
                goto LabelC144;//C132
            str(0x16 + x, a);//C134
            lda(0x01);//C136
            str(0x0F + x, a);//C138
            lda(0x00);//C13A
            str(0x1E + x, a);//C13C
            str(0x06CD, a);//C13E
            LabelC226(); //C141
            return;
            LabelC144:
            ldym(0x0739);//C144
            ldam(W(0xE9) + y);//C147
            cmp(a, 0xFF);//C149
            if (!z)
                goto LabelC150;//C14B
            goto LabelC216; //C14D
            LabelC150:
            and(0x0F);//C150
            cmp(a, 0x0E);//C152
            if (z)
                goto LabelC164;//C154
            cmp(x, 0x05);//C156
            if (!c)
                goto LabelC164;//C158
            iny();//C15A
            ldam(W(0xE9) + y);//C15B
            and(0x3F);//C15D
            cmp(a, 0x2E);//C15F
            if (z)
                goto LabelC164;//C161
            return;
            LabelC164:
            ldam(0x071D);//C164
            c = false;//C167
            adc(0x30);//C168
            and(0xF0);//C16A
            str(0x07, a);//C16C
            ldam(0x071B);//C16E
            adc(0x00);//C171
            str(0x06, a);//C173
            ldym(0x0739);//C175
            iny();//C178
            ldam(W(0xE9) + y);//C179
            asl();//C17B
            if (!c)
                goto LabelC189;//C17C
            ldam(0x073B);//C17E
            if (!z)
                goto LabelC189;//C181
            inc(0x073B);//C183
            inc(0x073A);//C186
            LabelC189:
            dey();//C189
            ldam(W(0xE9) + y);//C18A
            and(0x0F);//C18C
            cmp(a, 0x0F);//C18E
            if (!z)
                goto LabelC1AB;//C190
            ldam(0x073B);//C192
            if (!z)
                goto LabelC1AB;//C195
            iny();//C197
            ldam(W(0xE9) + y);//C198
            and(0x3F);//C19A
            str(0x073A, a);//C19C
            inc(0x0739);//C19F
            inc(0x0739);//C1A2
            inc(0x073B);//C1A5
            LabelC0CC(); //C1A8
            return;
            LabelC1AB:
            ldam(0x073A);//C1AB
            str(0x6E + x, a);//C1AE
            ldam(W(0xE9) + y);//C1B0
            and(0xF0);//C1B2
            str(0x87 + x, a);//C1B4
            cmp(a, ram[0x071D]);//C1B6
            ldam(0x6E + x);//C1B9
            sbc(ram[0x071B]);//C1BB
            if (c)
                goto LabelC1CB;//C1BE
            ldam(W(0xE9) + y);//C1C0
            and(0x0F);//C1C2
            cmp(a, 0x0E);//C1C4
            if (z)
                goto LabelC231;//C1C6
            goto LabelC250; //C1C8
            LabelC1CB:
            ldam(0x07);//C1CB
            cmp(a, ram[0x87 + x]);//C1CD
            ldam(0x06);//C1CF
            sbc(ram[0x6E + x]);//C1D1
            if (!c)
                goto LabelC216;//C1D3
            lda(0x01);//C1D5
            str(0xB6 + x, a);//C1D7
            ldam(W(0xE9) + y);//C1D9
            asl();//C1DB
            asl();//C1DC
            asl();//C1DD
            asl();//C1DE
            str(0xCF + x, a);//C1DF
            cmp(a, 0xE0);//C1E1
            if (z)
                goto LabelC231;//C1E3
            iny();//C1E5
            ldam(W(0xE9) + y);//C1E6
            and(0x40);//C1E8
            if (z)
                goto LabelC1F1;//C1EA
            ldam(0x06CC);//C1EC
            if (z)
                goto LabelC25E;//C1EF
            LabelC1F1:
            ldam(W(0xE9) + y);//C1F1
            and(0x3F);//C1F3
            cmp(a, 0x37);//C1F5
            if (!c)
                goto LabelC1FD;//C1F7
            cmp(a, 0x3F);//C1F9
            if (!c)
                goto LabelC22E;//C1FB
            LabelC1FD:
            cmp(a, 0x06);//C1FD
            if (!z)
                goto LabelC208;//C1FF
            ldym(0x076A);//C201
            if (z)
                goto LabelC208;//C204
            lda(0x02);//C206
            LabelC208:
            str(0x16 + x, a);//C208
            lda(0x01);//C20A
            str(0x0F + x, a);//C20C
            LabelC226();//C20E
            ldam(0x0F + x);//C211
            if (!z)
                goto LabelC25E;//C213
            return;
            LabelC216:
            ldam(0x06CB);//C216
            if (!z)
                goto LabelC224;//C219
            ldam(0x0398);//C21B
            cmp(a, 0x01);//C21E
            if (!z)
                return;//C220
            lda(0x2F);//C222
            LabelC224:
            str(0x16 + x, a);//C224
            LabelC226:
            lda(0x00);//C226
            str(0x1E + x, a);//C228
            LabelC26C();//C22A
            return;
            LabelC22E:
            LabelC71B(); //C22E
            return;
            LabelC231:
            iny();//C231
            iny();//C232
            ldam(W(0xE9) + y);//C233
            lsr();//C235
            lsr();//C236
            lsr();//C237
            lsr();//C238
            LabelC239:
            lsr();//C239
            cmp(a, ram[0x075F]);//C23A
            if (!z)
                goto LabelC24D;//C23D
            dey();//C23F
            ldam(W(0xE9) + y);//C240
            str(0x0750, a);//C242
            iny();//C245
            ldam(W(0xE9) + y);//C246
            and(0x1F);//C248
            str(0x0751, a);//C24A
            LabelC24D:
            goto LabelC25B; //C24D
            LabelC250:
            ldym(0x0739);//C250
            ldam(W(0xE9) + y);//C253
            and(0x0F);//C255
            cmp(a, 0x0E);//C257
            if (!z)
                goto LabelC25E;//C259
            LabelC25B:
            inc(0x0739);//C25B
            LabelC25E:
            inc(0x0739);//C25E
            inc(0x0739);//C261
            lda(0x00);//C264
            str(0x073B, a);//C266
            ldxm(0x08);//C269
        }

        void LabelC226()
        {
            lda(0x00);//C226
            str(0x1E + x, a);//C228
            LabelC26C();//C22A
            return;
        }

        void LabelC25E()
        {
            inc(0x0739);//C25E
            inc(0x0739);//C261
            lda(0x00);//C264
            str(0x073B, a);//C266
            ldxm(0x08);//C269
        }

        void LabelC26C()
        {
            ldam(0x16 + x);//C26C
            cmp(a, 0x15);//C26E
            if (c)
                goto LabelC27F;//C270
            tay();//C272
            ldam(0xCF + x);//C273
            adc(0x08);//C275
            str(0xCF + x, a);//C277
            lda(0x01);//C279
            str(0x03D8 + x, a);//C27B
            tya();//C27E

            LabelC27F:
            switch (a)
            {
                case 0:
                    LabelC30E();
                    break;
                case 1:
                    LabelC30E();
                    break;
                case 2:
                    LabelC30E();
                    break;
                case 3:
                    LabelC31E();
                    break;
                case 4:
                    return;
                case 5:
                    LabelC328();
                    break;
                case 6:
                    LabelC2F1();
                    break;
                case 7:
                    LabelC342();
                    break;
                case 8:
                    LabelC36B();
                    break;
                case 9:
                    return;
                case 10:
                    LabelC375();
                    break;
                case 11:
                    LabelC375();
                    break;
                case 12:
                    LabelC2F7();
                    break;
                case 13:
                    LabelC787();
                    break;
                case 14:
                    LabelC7D1();
                    break;
                case 15:
                    LabelC34A();
                    break;
                case 16:
                    LabelC33D();
                    break;
                case 17:
                    LabelC385();
                    break;
                case 18:
                    LabelC7A0();
                    break;
                case 19:
                    return;
                case 20:
                    LabelC7A0();
                    break;
                case 21:
                    LabelC7A0();
                    break;
                case 22:
                    LabelC7A0();
                    break;
                case 23:
                    LabelC7A0();
                    break;
                case 24:
                    LabelC7B8();
                    break;
                case 25:
                    return;
                case 26:
                    return;
                case 27:
                    LabelC45C();
                    break;
                case 28:
                    LabelC45C();
                    break;
                case 29:
                    LabelC45C();
                    break;
                case 30:
                    LabelC45C();
                    break;
                case 31:
                    LabelC459();
                    break;
                case 32:
                    return;
                case 33:
                    return;
                case 34:
                    return;
                case 35:
                    return;
                case 36:
                    LabelC7DF();
                    break;
                case 37:
                    LabelC812();
                    break;
                case 38:
                    LabelC83F();
                    break;
                case 39:
                    LabelC845();
                    break;
                case 40:
                    LabelC80B();
                    break;
                case 41:
                    LabelC803();
                    break;
                case 42:
                    LabelC80B();
                    break;
                case 43:
                    LabelC84B();
                    break;
                case 44:
                    LabelC857();
                    break;
                case 45:
                    LabelC549();
                    break;
                case 46:
                    LabelBC60();
                    break;
                case 47:
                    LabelB91E();
                    break;
                case 48:
                    return;
                case 49:
                    return;
                case 50:
                    return;
                case 51:
                    return;
                case 52:
                    return;
                case 53:
                    LabelC307();
                    break;
                case 54:
                    return;
            }
        }

        void LabelC2F1()
        {
            LabelC30E();//C2F1
            LabelC346(); //C2F4
            return;
        }

        void LabelC2F7()
        {
            lda(0x02);//C2F7
            str(0xB6 + x, a);//C2F9
            str(0xCF + x, a);//C2FB
            lsr();//C2FD
            str(0x0796 + x, a);//C2FE
            lsr();//C301
            str(0x1E + x, a);//C302
            LabelC346(); //C304
            return;
        }

        void LabelC307()
        {
            lda(0xB8);//C307
            str(0xCF + x, a);//C309
        }

        void LabelC30E()
        {
            ldy(0x01);//C30E
            ldam(0x076A);//C310
            if (!z)
                goto LabelC316;//C313
            dey();//C315
            LabelC316:
            ldam(0xC30C + y);//C316
            str(0x58 + x, a);//C319
            LabelC35A(); //C31B
            return;
        }

        void LabelC319()
        {
            str(0x58 + x, a);//C319
            LabelC35A(); //C31B
            return;
        }

        void LabelC31E()
        {
            LabelC30E();//C31E
            lda(0x01);//C321
            str(0x1E + x, a);//C323
        }

        void LabelC328()
        {
            lda(0x00);//C328
            str(0x03A2 + x, a);//C32A
            str(0x58 + x, a);//C32D
            ldym(0x06CC);//C32F
            ldam(0xC326 + y);//C332
            str(0x0796 + x, a);//C335
            lda(0x0B);//C338
            LabelC35C(); //C33A
            return;
        }

        void LabelC33D()
        {
            lda(0x00);//C33D
            LabelC319(); //C33F
            return;
        }

        void LabelC342()
        {
            lda(0x00);//C342
            str(0x58 + x, a);//C344
            lda(0x09);//C346
            if (!z)
                goto LabelC35C;//C348
            ldy(0x30);//C34A
            ldam(0xCF + x);//C34C
            str(0x0401 + x, a);//C34E
            if (!n)
                goto LabelC355;//C351
            ldy(0xE0);//C353
            LabelC355:
            tya();//C355
            adc(ram[0xCF + x]);//C356
            str(0x58 + x, a);//C358
            lda(0x03);//C35A
            LabelC35C:
            str(0x049A + x, a);//C35C
            lda(0x02);//C35F
            str(0x46 + x, a);//C361
            lda(0x00);//C363
            str(0xA0 + x, a);//C365
            str(0x0434 + x, a);//C367
        }

        void LabelC346()
        {
            lda(0x09);//C346
            if (!z)
                goto LabelC35C;//C348
            ldy(0x30);//C34A
            ldam(0xCF + x);//C34C
            str(0x0401 + x, a);//C34E
            if (!n)
                goto LabelC355;//C351
            ldy(0xE0);//C353
            LabelC355:
            tya();//C355
            adc(ram[0xCF + x]);//C356
            str(0x58 + x, a);//C358
            lda(0x03);//C35A
            LabelC35C:
            str(0x049A + x, a);//C35C
            lda(0x02);//C35F
            str(0x46 + x, a);//C361
            lda(0x00);//C363
            str(0xA0 + x, a);//C365
            str(0x0434 + x, a);//C367
        }

        void LabelC34A()
        {
            ldy(0x30);//C34A
            ldam(0xCF + x);//C34C
            str(0x0401 + x, a);//C34E
            if (!n)
                goto LabelC355;//C351
            ldy(0xE0);//C353
            LabelC355:
            tya();//C355
            adc(ram[0xCF + x]);//C356
            str(0x58 + x, a);//C358
            lda(0x03);//C35A
            str(0x049A + x, a);//C35C
            lda(0x02);//C35F
            str(0x46 + x, a);//C361
            lda(0x00);//C363
            str(0xA0 + x, a);//C365
            str(0x0434 + x, a);//C367
        }

        void LabelC35A()
        {
            lda(0x03);//C35A
            LabelC35C:
            str(0x049A + x, a);//C35C
            lda(0x02);//C35F
            str(0x46 + x, a);//C361
            lda(0x00);//C363
            str(0xA0 + x, a);//C365
            str(0x0434 + x, a);//C367
        }

        void LabelC35C()
        {
            str(0x049A + x, a);//C35C
            lda(0x02);//C35F
            str(0x46 + x, a);//C361
            lda(0x00);//C363
            str(0xA0 + x, a);//C365
            str(0x0434 + x, a);//C367
        }

        void LabelC363()
        {
            lda(0x00);//C363
            str(0xA0 + x, a);//C365
            str(0x0434 + x, a);//C367
        }

        void LabelC36B()
        {
            lda(0x02);//C36B
            str(0x46 + x, a);//C36D
            lda(0x09);//C36F
            str(0x049A + x, a);//C371
        }

        void LabelC375()
        {
            LabelC346();//C375
            ldam(0x07A7 + x);//C378
            and(0x10);//C37B
            str(0x58 + x, a);//C37D
            ldam(0xCF + x);//C37F
            str(0x0434 + x, a);//C381
        }

        void LabelC385()
        {
            ldam(0x06CB);//C385
            if (!z)
                goto LabelC395;//C388
            lda(0x00);//C38A
            str(0x06D1, a);//C38C
            LabelC33D();//C38F
            LabelC7D9(); //C392
            return;
            LabelC395:
            LabelC998(); //C395
            return;
        }

        void LabelC38A()
        {
            lda(0x00);//C38A
            str(0x06D1, a);//C38C
            LabelC33D();//C38F
            LabelC7D9(); //C392
            return;
        }

        void LabelC3A4()
        {
            ldam(0x078F);//C3A4
            if (!z)
                return;//C3A7
            cmp(x, 0x05);//C3A9
            if (c)
                return;//C3AB
            lda(0x80);//C3AD
            str(0x078F, a);//C3AF
            ldy(0x04);//C3B2
            LabelC3B4:
            ldam(0x0016 + y);//C3B4
            cmp(a, 0x11);//C3B7
            if (z)
                goto LabelC3E6;//C3B9
            dey();//C3BB
            if (!n)
                goto LabelC3B4;//C3BC
            inc(0x06D1);//C3BE
            ldam(0x06D1);//C3C1
            cmp(a, 0x07);//C3C4
            if (!c)
                return;//C3C6
            ldx(0x04);//C3C8
            LabelC3CA:
            ldam(0x0F + x);//C3CA
            if (z)
                goto LabelC3D3;//C3CC
            dex();//C3CE
            if (!n)
                goto LabelC3CA;//C3CF
            if (n) //
                goto LabelC3E3;//C3D1
            LabelC3D3:
            lda(0x00);//C3D3
            str(0x1E + x, a);//C3D5
            lda(0x11);//C3D7
            str(0x16 + x, a);//C3D9
            LabelC38A();//C3DB
            lda(0x20);//C3DE
            LabelC5D8();//C3E0
            LabelC3E3:
            ldxm(0x08);//C3E3
            return;
            LabelC3E6:
            ldam(0xCE);//C3E6
            cmp(a, 0x2C);//C3E8
            if (!c)
                return;//C3EA
            ldam(0x001E + y);//C3EC
            if (!z)
                return;//C3EF
            ldam(0x006E + y);//C3F1
            str(0x6E + x, a);//C3F4
            ldam(0x0087 + y);//C3F6
            str(0x87 + x, a);//C3F9
            lda(0x01);//C3FB
            str(0xB6 + x, a);//C3FD
            ldam(0x00CF + y);//C3FF
            c = true;//C402
            sbc(0x08);//C403
            str(0xCF + x, a);//C405
            ldam(0x07A7 + x);//C407
            and(0x03);//C40A
            tay();//C40C
            ldx(0x02);//C40D
            LabelC40F:
            ldam(0xC398 + y);//C40F
            str(0x01 + x, a);//C412
            iny();//C414
            iny();//C415
            iny();//C416
            iny();//C417
            dex();//C418
            if (!n)
                goto LabelC40F;//C419
            ldxm(0x08);//C41B
            LabelCF6C();//C41D
            ldym(0x57);//C420
            cmp(y, 0x08);//C422
            if (c)
                goto LabelC434;//C424
            tay();//C426
            ldam(0x07A8 + x);//C427
            and(0x03);//C42A
            if (z)
                goto LabelC433;//C42C
            tya();//C42E
            eor(0xFF);//C42F
            tay();//C431
            iny();//C432
            LabelC433:
            tya();//C433
            LabelC434:
            LabelC346();//C434
            ldy(0x02);//C437
            str(0x58 + x, a);//C439
            cmp(a, 0x00);//C43B
            if (n) //
                goto LabelC440;//C43D
            dey();//C43F
            LabelC440:
            str(0x46 + x, y);//C440
            lda(0xFD);//C442
            str(0xA0 + x, a);//C444
            lda(0x01);//C446
            str(0x0F + x, a);//C448
            lda(0x05);//C44A
            str(0x1E + x, a);//C44C
        }

        void LabelC459()
        {
            LabelC575();//C459
            lda(0x00);//C45C
            str(0x58 + x, a);//C45E
            ldam(0x16 + x);//C460
            c = true;//C462
            sbc(0x1B);//C463
            tay();//C465
            ldam(0xC44F + y);//C466
            str(0x0388 + x, a);//C469
            ldam(0xC454 + y);//C46C
            str(0x34 + x, a);//C46F
            ldam(0xCF + x);//C471
            c = false;//C473
            adc(0x04);//C474
            str(0xCF + x, a);//C476
            ldam(0x87 + x);//C478
            c = false;//C47A
            adc(0x04);//C47B
            str(0x87 + x, a);//C47D
            ldam(0x6E + x);//C47F
            adc(0x00);//C481
            str(0x6E + x, a);//C483
            LabelC7D9(); //C485
            return;
        }

        void LabelC45C()
        {
            lda(0x00);//C45C
            str(0x58 + x, a);//C45E
            ldam(0x16 + x);//C460
            c = true;//C462
            sbc(0x1B);//C463
            tay();//C465
            ldam(0xC44F + y);//C466
            str(0x0388 + x, a);//C469
            ldam(0xC454 + y);//C46C
            str(0x34 + x, a);//C46F
            ldam(0xCF + x);//C471
            c = false;//C473
            adc(0x04);//C474
            str(0xCF + x, a);//C476
            ldam(0x87 + x);//C478
            c = false;//C47A
            adc(0x04);//C47B
            str(0x87 + x, a);//C47D
            ldam(0x6E + x);//C47F
            adc(0x00);//C481
            str(0x6E + x, a);//C483
            LabelC7D9(); //C485
            return;
        }

        void LabelC4A8()
        {
            ldam(0x078F);//C4A8
            if (!z)
                return;//C4AB
            LabelC346();//C4AD
            ldam(0x07A8 + x);//C4B0
            and(0x03);//C4B3
            tay();//C4B5
            ldam(0xC4A4 + y);//C4B6
            str(0x078F, a);//C4B9
            ldy(0x03);//C4BC
            ldam(0x06CC);//C4BE
            if (z)
                goto LabelC4C4;//C4C1
            iny();//C4C3
            LabelC4C4:
            str(0x00, y);//C4C4
            cmp(x, ram[0x00]);//C4C6
            if (c)
                return;//C4C8
            ldam(0x07A7 + x);//C4CA
            and(0x03);//C4CD
            str(0x00, a);//C4CF
            str(0x01, a);//C4D1
            lda(0xFB);//C4D3
            str(0xA0 + x, a);//C4D5
            lda(0x00);//C4D7
            ldym(0x57);//C4D9
            if (z)
                goto LabelC4E4;//C4DB
            lda(0x04);//C4DD
            cmp(y, 0x19);//C4DF
            if (!c)
                goto LabelC4E4;//C4E1
            asl();//C4E3
            LabelC4E4:
            pha();//C4E4
            c = false;//C4E5
            adc(ram[0x00]);//C4E6
            str(0x00, a);//C4E8
            ldam(0x07A8 + x);//C4EA
            and(0x03);//C4ED
            if (z)
                goto LabelC4F8;//C4EF
            ldam(0x07A9 + x);//C4F1
            and(0x0F);//C4F4
            str(0x00, a);//C4F6
            LabelC4F8:
            pla();//C4F8
            c = false;//C4F9
            adc(ram[0x01]);//C4FA
            tay();//C4FC
            ldam(0xC498 + y);//C4FD
            str(0x58 + x, a);//C500
            lda(0x01);//C502
            str(0x46 + x, a);//C504
            ldam(0x57);//C506
            if (!z)
                goto LabelC51C;//C508
            ldym(0x00);//C50A
            tya();//C50C
            and(0x02);//C50D
            if (z)
                goto LabelC51C;//C50F
            ldam(0x58 + x);//C511
            eor(0xFF);//C513
            c = false;//C515
            adc(0x01);//C516
            str(0x58 + x, a);//C518
            inc(0x46 + x);//C51A
            LabelC51C:
            tya();//C51C
            and(0x02);//C51D
            if (z)
                goto LabelC530;//C51F
            ldam(0x86);//C521
            c = false;//C523
            adc(ram[0xC488 + y]);//C524
            str(0x87 + x, a);//C527
            ldam(0x6D);//C529
            adc(0x00);//C52B
            goto LabelC53C; //C52D
            LabelC530:
            ldam(0x86);//C530
            c = true;//C532
            sbc(ram[0xC488 + y]);//C533
            str(0x87 + x, a);//C536
            ldam(0x6D);//C538
            sbc(0x00);//C53A
            LabelC53C:
            str(0x6E + x, a);//C53C
            lda(0x01);//C53E
            str(0x0F + x, a);//C540
            str(0xB6 + x, a);//C542
            lda(0xF8);//C544
            str(0xCF + x, a);//C546
        }

        void LabelC549()
        {
            LabelC575();//C549
            str(0x0368, x);//C54C
            lda(0x00);//C54F
            str(0x0363, a);//C551
            str(0x0369, a);//C554
            ldam(0x87 + x);//C557
            str(0x0366, a);//C559
            lda(0xDF);//C55C
            str(0x0790, a);//C55E
            str(0x46 + x, a);//C561
            lda(0x20);//C563
            str(0x0364, a);//C565
            str(0x078A + x, a);//C568
            lda(0x05);//C56B
            str(0x0483, a);//C56D
            lsr();//C570
            str(0x0365, a);//C571
        }

        void LabelC575()
        {
            ldy(0xFF);//C575
            LabelC577:
            iny();//C577
            ldam(0x000F + y);//C578
            if (!z)
                goto LabelC577;//C57B
            str(0x06CF, y);//C57D
            txa();//C580
            or(0x80);//C581
            str(0x000F + y, a);//C583
            ldam(0x6E + x);//C586
            str(0x006E + y, a);//C588
            ldam(0x87 + x);//C58B
            str(0x0087 + y, a);//C58D
            lda(0x01);//C590
            str(0x0F + x, a);//C592
            str(0x00B6 + y, a);//C594
            ldam(0xCF + x);//C597
            str(0x00CF + y, a);//C599
        }

        void LabelC5A3()
        {
            ldam(0x078F);//C5A3
            if (!z)
                return;//C5A6
            str(0x0434 + x, a);//C5A8
            ldam(0xFD);//C5AB
            or(0x02);//C5AD
            str(0xFD, a);//C5AF
            ldym(0x0368);//C5B1
            ldam(0x0016 + y);//C5B4
            cmp(a, 0x2D);//C5B7
            if (z)
                goto LabelC5EC;//C5B9
            LabelD1D9();//C5BB
            c = false;//C5BE
            adc(0x20);//C5BF
            ldym(0x06CC);//C5C1
            if (z)
                goto LabelC5C9;//C5C4
            c = true;//C5C6
            sbc(0x10);//C5C7
            LabelC5C9:
            str(0x078F, a);//C5C9
            ldam(0x07A7 + x);//C5CC
            and(0x03);//C5CF
            str(0x0417 + x, a);//C5D1
            tay();//C5D4
            ldam(0xC59D + y);//C5D5
            str(0xCF + x, a);//C5D8
            ldam(0x071D);//C5DA
            c = false;//C5DD
            adc(0x20);//C5DE
            str(0x87 + x, a);//C5E0
            ldam(0x071B);//C5E2
            adc(0x00);//C5E5
            str(0x6E + x, a);//C5E7
            LabelC61F(); //C5E9
            return;
            LabelC5EC:
            ldam(0x0087 + y);//C5EC
            c = true;//C5EF
            sbc(0x0E);//C5F0
            str(0x87 + x, a);//C5F2
            ldam(0x006E + y);//C5F4
            str(0x6E + x, a);//C5F7
            ldam(0x00CF + y);//C5F9
            c = false;//C5FC
            adc(0x08);//C5FD
            str(0xCF + x, a);//C5FF
            ldam(0x07A7 + x);//C601
            and(0x03);//C604
            str(0x0417 + x, a);//C606
            tay();//C609
            ldam(0xC59D + y);//C60A
            ldy(0x00);//C60D
            cmp(a, ram[0xCF + x]);//C60F
            if (!c)
                goto LabelC614;//C611
            iny();//C613
            LabelC614:
            ldam(0xC5A1 + y);//C614
            str(0x0434 + x, a);//C617
            lda(0x00);//C61A
            str(0x06CB, a);//C61C
            lda(0x08);//C61F
            str(0x049A + x, a);//C621
            lda(0x01);//C624
            str(0xB6 + x, a);//C626
            str(0x0F + x, a);//C628
            lsr();//C62A
            str(0x0401 + x, a);//C62B
            str(0x1E + x, a);//C62E
        }

        void LabelC5D8()
        {
            str(0xCF + x, a);//C5D8
            ldam(0x071D);//C5DA
            c = false;//C5DD
            adc(0x20);//C5DE
            str(0x87 + x, a);//C5E0
            ldam(0x071B);//C5E2
            adc(0x00);//C5E5
            str(0x6E + x, a);//C5E7
            LabelC61F(); //C5E9
            return;
        }

        void LabelC5EC()
        {
            ldam(0x0087 + y);//C5EC
            c = true;//C5EF
            sbc(0x0E);//C5F0
            str(0x87 + x, a);//C5F2
            ldam(0x006E + y);//C5F4
            str(0x6E + x, a);//C5F7
            ldam(0x00CF + y);//C5F9
            c = false;//C5FC
            adc(0x08);//C5FD
            str(0xCF + x, a);//C5FF
            ldam(0x07A7 + x);//C601
            and(0x03);//C604
            str(0x0417 + x, a);//C606
            tay();//C609
            ldam(0xC59D + y);//C60A
            ldy(0x00);//C60D
            cmp(a, ram[0xCF + x]);//C60F
            if (!c)
                goto LabelC614;//C611
            iny();//C613
            LabelC614:
            ldam(0xC5A1 + y);//C614
            str(0x0434 + x, a);//C617
            lda(0x00);//C61A
            str(0x06CB, a);//C61C
            lda(0x08);//C61F
            str(0x049A + x, a);//C621
            lda(0x01);//C624
            str(0xB6 + x, a);//C626
            str(0x0F + x, a);//C628
            lsr();//C62A
            str(0x0401 + x, a);//C62B
            str(0x1E + x, a);//C62E
        }

        void LabelC61F()
        {
            lda(0x08);//C61F
            str(0x049A + x, a);//C621
            lda(0x01);//C624
            str(0xB6 + x, a);//C626
            str(0x0F + x, a);//C628
            lsr();//C62A
            str(0x0401 + x, a);//C62B
            str(0x1E + x, a);//C62E
        }

        void LabelC63D()
        {
            ldam(0x078F);//C63D
            if (!z)
                return;//C640
            lda(0x20);//C642
            str(0x078F, a);//C644
            dec(0x06D7);//C647
            ldy(0x06);//C64A
            LabelC64C:
            dey();//C64C
            ldam(0x0016 + y);//C64D
            cmp(a, 0x31);//C650
            if (!z)
                goto LabelC64C;//C652
            ldam(0x0087 + y);//C654
            c = true;//C657
            sbc(0x30);//C658
            pha();//C65A
            ldam(0x006E + y);//C65B
            sbc(0x00);//C65E
            str(0x00, a);//C660
            ldam(0x06D7);//C662
            c = false;//C665
            adc(ram[0x001E + y]);//C666
            tay();//C669
            pla();//C66A
            c = false;//C66B
            adc(ram[0xC631 + y]);//C66C
            str(0x87 + x, a);//C66F
            ldam(0x00);//C671
            adc(0x00);//C673
            str(0x6E + x, a);//C675
            ldam(0xC637 + y);//C677
            str(0xCF + x, a);//C67A
            lda(0x01);//C67C
            str(0xB6 + x, a);//C67E
            str(0x0F + x, a);//C680
            lsr();//C682
            str(0x58 + x, a);//C683
            lda(0x08);//C685
            str(0xA0 + x, a);//C687
        }

        void LabelC69C()
        {
            ldam(0x078F);//C69C
            if (!z)
                return;//C69F
            ldam(0x074E);//C6A1
            if (!z)
                goto LabelC6FD;//C6A4
            cmp(x, 0x03);//C6A6
            if (c)
                return;//C6A8
            ldy(0x00);//C6AA
            ldam(0x07A7 + x);//C6AC
            cmp(a, 0xAA);//C6AF
            if (!c)
                goto LabelC6B4;//C6B1
            iny();//C6B3
            LabelC6B4:
            ldam(0x075F);//C6B4
            cmp(a, 0x01);//C6B7
            if (z)
                goto LabelC6BC;//C6B9
            iny();//C6BB
            LabelC6BC:
            tya();//C6BC
            and(0x01);//C6BD
            tay();//C6BF
            ldam(0xC69A + y);//C6C0
            LabelC6C3:
            str(0x16 + x, a);//C6C3
            ldam(0x06DD);//C6C5
            cmp(a, 0xFF);//C6C8
            if (!z)
                goto LabelC6D1;//C6CA
            lda(0x00);//C6CC
            str(0x06DD, a);//C6CE
            LabelC6D1:
            ldam(0x07A7 + x);//C6D1
            and(0x07);//C6D4
            LabelC6D6:
            tay();//C6D6
            ldam(0xC68A + y);//C6D7
            bit(ram[0x06DD]);//C6DA
            if (z)
                goto LabelC6E6;//C6DD
            iny();//C6DF
            tya();//C6E0
            and(0x07);//C6E1
            goto LabelC6D6; //C6E3
            LabelC6E6:
            orm(0x06DD);//C6E6
            str(0x06DD, a);//C6E9
            ldam(0xC692 + y);//C6EC
            LabelC5D8();//C6EF
            str(0x0417 + x, a);//C6F2
            lda(0x20);//C6F5
            str(0x078F, a);//C6F7
            LabelC26C(); //C6FA
            return;
            LabelC6FD:
            ldy(0xFF);//C6FD
            LabelC6FF:
            iny();//C6FF
            cmp(y, 0x05);//C700
            if (c)
                goto LabelC711;//C702
            ldam(0x000F + y);//C704
            if (z)
                goto LabelC6FF;//C707
            ldam(0x0016 + y);//C709
            cmp(a, 0x08);//C70C
            if (!z)
                goto LabelC6FF;//C70E
            return;
            LabelC711:
            ldam(0xFE);//C711
            or(0x08);//C713
            str(0xFE, a);//C715
            lda(0x08);//C717
            if (!z)
                goto LabelC6C3;//C719
            ldy(0x00);//C71B
            c = true;//C71D
            sbc(0x37);//C71E
            pha();//C720
            cmp(a, 0x04);//C721
            if (c)
                goto LabelC730;//C723
            pha();//C725
            ldy(0x06);//C726
            ldam(0x076A);//C728
            if (z)
                goto LabelC72F;//C72B
            ldy(0x02);//C72D
            LabelC72F:
            pla();//C72F
            LabelC730:
            str(0x01, y);//C730
            ldy(0xB0);//C732
            and(0x02);//C734
            if (z)
                goto LabelC73A;//C736
            ldy(0x70);//C738
            LabelC73A:
            str(0x00, y);//C73A
            ldam(0x071B);//C73C
            str(0x02, a);//C73F
            ldam(0x071D);//C741
            str(0x03, a);//C744
            ldy(0x02);//C746
            pla();//C748
            lsr();//C749
            if (!c)
                goto LabelC74D;//C74A
            iny();//C74C
            LabelC74D:
            str(0x06D3, y);//C74D
            LabelC750:
            ldx(0xFF);//C750
            LabelC752:
            inx();//C752
            cmp(x, 0x05);//C753
            if (c)
                goto LabelC784;//C755
            ldam(0x0F + x);//C757
            if (!z)
                goto LabelC752;//C759
            ldam(0x01);//C75B
            str(0x16 + x, a);//C75D
            ldam(0x02);//C75F
            str(0x6E + x, a);//C761
            ldam(0x03);//C763
            str(0x87 + x, a);//C765
            c = false;//C767
            adc(0x18);//C768
            str(0x03, a);//C76A
            ldam(0x02);//C76C
            adc(0x00);//C76E
            str(0x02, a);//C770
            ldam(0x00);//C772
            str(0xCF + x, a);//C774
            lda(0x01);//C776
            str(0xB6 + x, a);//C778
            str(0x0F + x, a);//C77A
            LabelC26C();//C77C
            dec(0x06D3);//C77F
            if (!z)
                goto LabelC750;//C782
            LabelC784:
            LabelC25E(); //C784
            return;
        }

        void LabelC71B()
        {
            ldy(0x00);//C71B
            c = true;//C71D
            sbc(0x37);//C71E
            pha();//C720
            cmp(a, 0x04);//C721
            if (c)
                goto LabelC730;//C723
            pha();//C725
            ldy(0x06);//C726
            ldam(0x076A);//C728
            if (z)
                goto LabelC72F;//C72B
            ldy(0x02);//C72D
            LabelC72F:
            pla();//C72F
            LabelC730:
            str(0x01, y);//C730
            ldy(0xB0);//C732
            and(0x02);//C734
            if (z)
                goto LabelC73A;//C736
            ldy(0x70);//C738
            LabelC73A:
            str(0x00, y);//C73A
            ldam(0x071B);//C73C
            str(0x02, a);//C73F
            ldam(0x071D);//C741
            str(0x03, a);//C744
            ldy(0x02);//C746
            pla();//C748
            lsr();//C749
            if (!c)
                goto LabelC74D;//C74A
            iny();//C74C
            LabelC74D:
            str(0x06D3, y);//C74D
            LabelC750:
            ldx(0xFF);//C750
            LabelC752:
            inx();//C752
            cmp(x, 0x05);//C753
            if (c)
                goto LabelC784;//C755
            ldam(0x0F + x);//C757
            if (!z)
                goto LabelC752;//C759
            ldam(0x01);//C75B
            str(0x16 + x, a);//C75D
            ldam(0x02);//C75F
            str(0x6E + x, a);//C761
            ldam(0x03);//C763
            str(0x87 + x, a);//C765
            c = false;//C767
            adc(0x18);//C768
            str(0x03, a);//C76A
            ldam(0x02);//C76C
            adc(0x00);//C76E
            str(0x02, a);//C770
            ldam(0x00);//C772
            str(0xCF + x, a);//C774
            lda(0x01);//C776
            str(0xB6 + x, a);//C778
            str(0x0F + x, a);//C77A
            LabelC26C();//C77C
            dec(0x06D3);//C77F
            if (!z)
                goto LabelC750;//C782
            LabelC784:
            LabelC25E(); //C784
            return;
        }

        void LabelC787()
        {
            lda(0x01);//C787
            str(0x58 + x, a);//C789
            lsr();//C78B
            str(0x1E + x, a);//C78C
            str(0xA0 + x, a);//C78E
            ldam(0xCF + x);//C790
            str(0x0434 + x, a);//C792
            c = true;//C795
            sbc(0x18);//C796
            str(0x0417 + x, a);//C798
            lda(0x09);//C79B
            LabelC7DB(); //C79D
            return;
        }

        void LabelC7A0()
        {
            ldam(0x16 + x);//C7A0
            str(0x06CB, a);//C7A2
            c = true;//C7A5
            sbc(0x12);//C7A6

            switch (a)
            {
                case 0:
                    LabelC3A4();
                    break;
                case 1:
                    return;
                case 2:
                    LabelC4A8();
                    break;
                case 3:
                    LabelC5A3();
                    break;
                case 4:
                    LabelC63D();
                    break;
                case 5:
                    LabelC69C();
                    break;
            }
        }

        void LabelC7B8()
        {
            ldy(0x05);//C7B8
            LabelC7BA:
            ldam(0x0016 + y);//C7BA
            cmp(a, 0x11);//C7BD
            if (!z)
                goto LabelC7C6;//C7BF
            lda(0x01);//C7C1
            str(0x001E + y, a);//C7C3
            LabelC7C6:
            dey();//C7C6
            if (!n)
                goto LabelC7BA;//C7C7
            lda(0x00);//C7C9
            str(0x06CB, a);//C7CB
            str(0x0F + x, a);//C7CE
        }

        void LabelC7D1()
        {
            lda(0x02);//C7D1
            str(0x46 + x, a);//C7D3
            lda(0xF8);//C7D5
            str(0x58 + x, a);//C7D7
            lda(0x03);//C7D9
            str(0x049A + x, a);//C7DB
        }

        void LabelC7D9()
        {
            lda(0x03);//C7D9
            str(0x049A + x, a);//C7DB
        }

        void LabelC7DB()
        {
            str(0x049A + x, a);//C7DB
        }

        void LabelC7DF()
        {
            dec(0xCF + x);//C7DF
            dec(0xCF + x);//C7E1
            ldym(0x06CC);//C7E3
            if (!z)
                goto LabelC7ED;//C7E6
            ldy(0x02);//C7E8
            LabelC871();//C7EA
            LabelC7ED:
            ldy(0xFF);//C7ED
            ldam(0x03A0);//C7EF
            str(0x1E + x, a);//C7F2
            if (!n)
                goto LabelC7F8;//C7F4
            txa();//C7F6
            tay();//C7F7
            LabelC7F8:
            str(0x03A0, y);//C7F8
            lda(0x00);//C7FB
            str(0x46 + x, a);//C7FD
            tay();//C7FF
            LabelC871();//C800
            lda(0xFF);//C803
            str(0x03A2 + x, a);//C805
            LabelC828(); //C808
            return;
        }

        void LabelC803()
        {
            lda(0xFF);//C803
            str(0x03A2 + x, a);//C805
            LabelC828(); //C808
            return;
        }

        void LabelC80B()
        {
            lda(0x00);//C80B
            str(0x58 + x, a);//C80D
            LabelC828(); //C80F
            return;
        }

        void LabelC812()
        {
            ldy(0x40);//C812
            ldam(0xCF + x);//C814
            if (!n)
                goto LabelC81F;//C816
            eor(0xFF);//C818
            c = false;//C81A
            adc(0x01);//C81B
            ldy(0xC0);//C81D
            LabelC81F:
            str(0x0401 + x, a);//C81F
            tya();//C822
            c = false;//C823
            adc(ram[0xCF + x]);//C824
            str(0x58 + x, a);//C826
            LabelC363();//C828
            lda(0x05);//C82B
            ldym(0x074E);//C82D
            cmp(y, 0x03);//C830
            if (z)
                goto LabelC83B;//C832
            ldym(0x06CC);//C834
            if (!z)
                goto LabelC83B;//C837
            lda(0x06);//C839
            LabelC83B:
            str(0x049A + x, a);//C83B
        }

        void LabelC828()
        {
            LabelC363();//C828
            lda(0x05);//C82B
            ldym(0x074E);//C82D
            cmp(y, 0x03);//C830
            if (z)
                goto LabelC83B;//C832
            ldym(0x06CC);//C834
            if (!z)
                goto LabelC83B;//C837
            lda(0x06);//C839
            LabelC83B:
            str(0x049A + x, a);//C83B
        }

        void LabelC82B()
        {
            lda(0x05);//C82B
            ldym(0x074E);//C82D
            cmp(y, 0x03);//C830
            if (z)
                goto LabelC83B;//C832
            ldym(0x06CC);//C834
            if (!z)
                goto LabelC83B;//C837
            lda(0x06);//C839
            LabelC83B:
            str(0x049A + x, a);//C83B
        }

        void LabelC83F()
        {
            LabelC84B();//C83F
            LabelC848(); //C842
            return;
        }

        void LabelC845()
        {
            LabelC857();//C845
            LabelC82B(); //C848
            return;
        }

        void LabelC848()
        {
            LabelC82B(); //C848
            return;
        }

        void LabelC84B()
        {
            lda(0x10);//C84B
            str(0x0434 + x, a);//C84D
            lda(0xFF);//C850
            str(0xA0 + x, a);//C852
            LabelC860(); //C854
            return;
        }

        void LabelC857()
        {
            lda(0xF0);//C857
            str(0x0434 + x, a);//C859
            lda(0x00);//C85C
            str(0xA0 + x, a);//C85E
            ldy(0x01);//C860
            LabelC871();//C862
            lda(0x04);//C865
            str(0x049A + x, a);//C867
        }

        void LabelC860()
        {
            ldy(0x01);//C860
            LabelC871();//C862
            lda(0x04);//C865
            str(0x049A + x, a);//C867
        }

        void LabelC871()
        {
            ldam(0x87 + x);//C871
            c = false;//C873
            adc(ram[0xC86B + y]);//C874
            str(0x87 + x, a);//C877
            ldam(0x6E + x);//C879
            adc(ram[0xC86E + y]);//C87B
            str(0x6E + x, a);//C87E
        }

        void LabelC882()
        {
            ldxm(0x08);//C882
            lda(0x00);//C884
            ldym(0x16 + x);//C886
            cmp(y, 0x15);//C888
            if (!c)
                goto LabelC88F;//C88A
            tya();//C88C
            sbc(0x14);//C88D
            LabelC88F:
            switch (a)
            {
                case 0:
                    LabelC8E0();
                    break;
                case 1:
                    LabelC935();
                    break;
                case 2:
                    LabelD295();
                    break;
                case 3:
                    return;
                case 4:
                    return;
                case 5:
                    return;
                case 6:
                    return;
                case 7:
                    LabelC947();
                    break;
                case 8:
                    LabelC947();
                    break;
                case 9:
                    LabelC947();
                    break;
                case 10:
                    LabelC947();
                    break;
                case 11:
                    LabelC947();
                    break;
                case 12:
                    LabelC947();
                    break;
                case 13:
                    LabelC947();
                    break;
                case 14:
                    LabelC947();
                    break;
                case 15:
                    return;
                case 16:
                    LabelC965();
                    break;
                case 17:
                    LabelC965();
                    break;
                case 18:
                    LabelC965();
                    break;
                case 19:
                    LabelC965();
                    break;
                case 20:
                    LabelC965();
                    break;
                case 21:
                    LabelC965();
                    break;
                case 22:
                    LabelC965();
                    break;
                case 23:
                    LabelC94D();
                    break;
                case 24:
                    LabelC94D();
                    break;
                case 25:
                    LabelD065();
                    break;
                case 26:
                    LabelBC85();
                    break;
                case 27:
                    LabelB94B();
                    break;
                case 28:
                    return;
                case 29:
                    LabelD2D9();
                    break;
                case 30:
                    LabelB8BA();
                    break;
                case 31:
                    return;
                case 32:
                    LabelB7A4();
                    break;
                case 33:
                    LabelC8D7();
                    break;
            }
        }

        void LabelC8D7()
        {
            LabelF1AF();//C8D7
            LabelF152();//C8DA
            LabelE87D(); //C8DD
            return;
        }

        void LabelC8E0()
        {
            lda(0x00);//C8E0
            str(0x03C5 + x, a);//C8E2
            LabelF1AF();//C8E5
            LabelF152();//C8E8
            LabelE87D();//C8EB
            LabelE243();//C8EE
            LabelDFC1();//C8F1
            LabelDA33();//C8F4
            LabelD853();//C8F7
            ldym(0x0747);//C8FA
            if (!z)
                goto LabelC902;//C8FD
            LabelC905();//C8FF
            LabelC902:
            LabelD67A(); //C902
            return;
        }

        void LabelC905()
        {
            ldam(0x16 + x);//C905
            switch (a)
            {
                case 0:
                    LabelCA77();
                    break;
                case 1:
                    LabelCA77();
                    break;
                case 2:
                    LabelCA77();
                    break;
                case 3:
                    LabelCA77();
                    break;
                case 4:
                    LabelCA77();
                    break;
                case 5:
                    LabelC9D8();
                    break;
                case 6:
                    LabelCA77();
                    break;
                case 7:
                    LabelCB89();
                    break;
                case 8:
                    LabelCC36();
                    break;
                case 9:
                    return;
                case 10:
                    LabelCC4A();
                    break;
                case 11:
                    LabelCC4A();
                    break;
                case 12:
                    LabelC9B0();
                    break;
                case 13:
                    LabelD3B0();
                    break;
                case 14:
                    LabelCAF9();
                    break;
                case 15:
                    LabelCAFF();
                    break;
                case 16:
                    LabelCB25();
                    break;
                case 17:
                    LabelCF28();
                    break;
                case 18:
                    LabelCA77();
                    break;
                case 19:
                    return;
                case 20:
                    LabelCEDF();
                    break;
            }
        }

        void LabelC935()
        {
            LabelD1EB();//C935
            LabelF1AF();//C938
            LabelF152();//C93B
            LabelE243();//C93E
            LabelD853();//C941
            LabelD67A(); //C944
            return;
        }

        void LabelC947()
        {
            LabelCD3C();//C947
            LabelD67A(); //C94A
            return;
        }

        void LabelC94D()
        {
            LabelF1AF();//C94D
            LabelF152();//C950
            LabelE24C();//C953
            LabelDB7B();//C956
            LabelF152();//C959
            LabelED66();//C95C
            LabelD655();//C95F
            LabelD67A(); //C962
        }

        void LabelC965()
        {
            LabelF1AF();//C965
            LabelF152();//C968
            LabelE273();//C96B
            LabelDB45();//C96E
            ldam(0x0747);//C971
            if (!z)
                goto LabelC979;//C974
            LabelC982();//C976
            LabelC979:
            LabelF152();//C979
            LabelE5C8();//C97C
            LabelD67A(); //C97F
            return;
        }

        void LabelC982()
        {
            ldam(0x16 + x);//C982
            c = true;//C984
            sbc(0x24);//C985

            switch (a)
            {
                case 0:
                    LabelD432();
                    break;
                case 1:
                    LabelD5D3();
                    break;
                case 2:
                    LabelD64F();
                    break;
                case 3:
                    LabelD64F();
                    break;
                case 4:
                    LabelD607();
                    break;
                case 5:
                    LabelD631();
                    break;
                case 6:
                    LabelD63D();
                    break;
            }
        }

        void LabelC998()
        {
            lda(0x00);//C998
            str(0x0F + x, a);//C99A
            str(0x16 + x, a);//C99C
            str(0x1E + x, a);//C99E
            str(0x0110 + x, a);//C9A0
            str(0x0796 + x, a);//C9A3
            str(0x0125 + x, a);//C9A6
            str(0x03C5 + x, a);//C9A9
            str(0x078A + x, a);//C9AC
        }

        void LabelC9B0()
        {
            ldam(0x0796 + x);//C9B0
            if (!z)
                goto LabelC9CB;//C9B3
            LabelC2F7();//C9B5
            ldam(0x07A8 + x);//C9B8
            or(0x80);//C9BB
            str(0x0434 + x, a);//C9BD
            and(0x0F);//C9C0
            or(0x06);//C9C2
            str(0x0796 + x, a);//C9C4
            lda(0xF9);//C9C7
            str(0xA0 + x, a);//C9C9
            LabelC9CB:
            LabelBF92(); //C9CB
            return;
        }

        void LabelC9D8()
        {
            ldam(0x1E + x);//C9D8
            and(0x20);//C9DA
            if (z)
                goto LabelC9E1;//C9DC
            LabelCAE5(); //C9DE
            return;
            LabelC9E1:
            ldam(0x3C + x);//C9E1
            if (z)
                goto LabelCA12;//C9E3
            dec(0x3C + x);//C9E5
            ldam(0x03D1);//C9E7
            and(0x0C);//C9EA
            if (!z)
                goto LabelCA58;//C9EC
            ldam(0x03A2 + x);//C9EE
            if (!z)
                goto LabelCA0A;//C9F1
            ldym(0x06CC);//C9F3
            ldam(0xC9CE + y);//C9F6
            str(0x03A2 + x, a);//C9F9
            LabelBA94();//C9FC
            if (!c)
                goto LabelCA0A;//C9FF
            ldam(0x1E + x);//CA01
            or(0x08);//CA03
            str(0x1E + x, a);//CA05
            LabelCA58(); //CA07
            return;
            LabelCA0A:
            dec(0x03A2 + x);//CA0A
            LabelCA58(); //CA0D
            return;
            LabelCA12:
            ldam(0x1E + x);//CA12
            and(0x07);
            cmp(a, 0x01);//CA16
            if (z)
                goto LabelCA58;//CA18
            lda(0x00);//CA1A
            str(0x00, a);//CA1C
            ldy(0xFA);//CA1E
            ldam(0xCF + x);//CA20
            if (n) //
                goto LabelCA37;//CA22
            ldy(0xFD);//CA24
            cmp(a, 0x70);//CA26
            inc(0x00);//CA28
            if (!c)
                goto LabelCA37;//CA2A
            dec(0x00);//CA2C
            ldam(0x07A8 + x);//CA2E
            and(0x01);//CA31
            if (!z)
                goto LabelCA37;//CA33
            ldy(0xFA);//CA35
            LabelCA37:
            str(0xA0 + x, y);//CA37
            ldam(0x1E + x);//CA39
            or(0x01);//CA3B
            str(0x1E + x, a);//CA3D
            ldam(0x00);//CA3F
            and(ram[0x07A9 + x]);//CA41
            tay();//CA44
            ldam(0x06CC);//CA45
            if (!z)
                goto LabelCA4B;//CA48
            tay();//CA4A
            LabelCA4B:
            ldam(0xCA10 + y);//CA4B
            str(0x078A + x, a);//CA4E
            ldam(0x07A8 + x);//CA51
            or(0xC0);//CA54
            str(0x3C + x, a);//CA56
            LabelCA58:
            ldy(0xFC);//CA58
            ldam(0x09);//CA5A
            and(0x40);//CA5C
            if (!z)
                goto LabelCA62;//CA5E
            ldy(0x04);//CA60
            LabelCA62:
            str(0x58 + x, y);//CA62
            ldy(0x01);//CA64
            LabelE143();//CA66
            if (n) //
                goto LabelCA75;//CA69
            iny();//CA6B
            ldam(0x0796 + x);//CA6C
            if (!z)
                goto LabelCA75;//CA6F
            lda(0xF8);//CA71
            str(0x58 + x, a);//CA73
            LabelCA75:
            str(0x46 + x, y);//CA75
            ldy(0x00);//CA77
            ldam(0x1E + x);//CA79
            and(0x40);//CA7B
            if (!z)
                goto LabelCA98;//CA7D
            ldam(0x1E + x);//CA7F
            asl();//CA81
            if (c)
                goto LabelCAB4;//CA82
            ldam(0x1E + x);//CA84
            and(0x20);//CA86
            if (!z)
                goto LabelCAE5;//CA88
            ldam(0x1E + x);//CA8A
            and(0x07);//CA8C
            if (z)
                goto LabelCAB4;//CA8E
            cmp(a, 0x05);//CA90
            if (z)
                goto LabelCA98;//CA92
            cmp(a, 0x03);//CA94
            if (c)
                goto LabelCAC8;//CA96
            LabelCA98:
            LabelBF63();//CA98
            ldy(0x00);//CA9B
            ldam(0x1E + x);//CA9D
            cmp(a, 0x02);//CA9F
            if (z)
                goto LabelCAAF;//CAA1
            and(0x40);//CAA3
            if (z)
                goto LabelCAB4;//CAA5
            ldam(0x16 + x);//CAA7
            cmp(a, 0x2E);//CAA9
            if (z)
                goto LabelCAB4;//CAAB
            if (!z)
                goto LabelCAB2;//CAAD
            LabelCAAF:
            LabelBF02(); //CAAF
            return;
            LabelCAB2:
            ldy(0x01);//CAB2
            LabelCAB4:
            ldam(0x58 + x);//CAB4
            pha();//CAB6
            if (!n)
                goto LabelCABB;//CAB7
            iny();//CAB9
            iny();//CABA
            LabelCABB:
            c = false;//CABB
            adc(ram[0xC9D0 + y]);//CABC
            str(0x58 + x, a);//CABF
            LabelBF02();//CAC1
            pla();//CAC4
            str(0x58 + x, a);//CAC5
            return;
            LabelCAC8:
            ldam(0x0796 + x);//CAC8
            if (!z)
                goto LabelCAEB;//CACB
            str(0x1E + x, a);//CACD
            ldam(0x09);//CACF
            and(0x01);//CAD1
            tay();//CAD3
            iny();//CAD4
            str(0x46 + x, y);//CAD5
            dey();//CAD7
            ldam(0x076A);//CAD8
            if (z)
                goto LabelCADF;//CADB
            iny();//CADD
            iny();//CADE
            LabelCADF:
            ldam(0xC9D4 + y);//CADF
            str(0x58 + x, a);//CAE2
            return;
            LabelCAE5:
            LabelBF63();//CAE5
            LabelBF02(); //CAE8
            return;
            LabelCAEB:
            cmp(a, 0x0E);//CAEB
            if (!z)
                return;//CAED
            ldam(0x16 + x);//CAEF
            cmp(a, 0x06);//CAF1
            if (!z)
                return;//CAF3
            LabelC998();//CAF5
        }

        void LabelCA37()
        {
            str(0xA0 + x, y);//CA37
            ldam(0x1E + x);//CA39
            or(0x01);//CA3B
            str(0x1E + x, a);//CA3D
            ldam(0x00);//CA3F
            and(ram[0x07A9 + x]);//CA41
            tay();//CA44
            ldam(0x06CC);//CA45
            if (!z)
                goto LabelCA4B;//CA48
            tay();//CA4A
            LabelCA4B:
            ldam(0xCA10 + y);//CA4B
            str(0x078A + x, a);//CA4E
            ldam(0x07A8 + x);//CA51
            or(0xC0);//CA54
            str(0x3C + x, a);//CA56
            LabelCA58:
            ldy(0xFC);//CA58
            ldam(0x09);//CA5A
            and(0x40);//CA5C
            if (!z)
                goto LabelCA62;//CA5E
            ldy(0x04);//CA60
            LabelCA62:
            str(0x58 + x, y);//CA62
            ldy(0x01);//CA64
            LabelE143();//CA66
            if (n) //
                goto LabelCA75;//CA69
            iny();//CA6B
            ldam(0x0796 + x);//CA6C
            if (!z)
                goto LabelCA75;//CA6F
            lda(0xF8);//CA71
            str(0x58 + x, a);//CA73
            LabelCA75:
            str(0x46 + x, y);//CA75
            ldy(0x00);//CA77
            ldam(0x1E + x);//CA79
            and(0x40);//CA7B
            if (!z)
                goto LabelCA98;//CA7D
            ldam(0x1E + x);//CA7F
            asl();//CA81
            if (c)
                goto LabelCAB4;//CA82
            ldam(0x1E + x);//CA84
            and(0x20);//CA86
            if (!z)
                goto LabelCAE5;//CA88
            ldam(0x1E + x);//CA8A
            and(0x07);//CA8C
            if (z)
                goto LabelCAB4;//CA8E
            cmp(a, 0x05);//CA90
            if (z)
                goto LabelCA98;//CA92
            cmp(a, 0x03);//CA94
            if (c)
                goto LabelCAC8;//CA96
            LabelCA98:
            LabelBF63();//CA98
            ldy(0x00);//CA9B
            ldam(0x1E + x);//CA9D
            cmp(a, 0x02);//CA9F
            if (z)
                goto LabelCAAF;//CAA1
            and(0x40);//CAA3
            if (z)
                goto LabelCAB4;//CAA5
            ldam(0x16 + x);//CAA7
            cmp(a, 0x2E);//CAA9
            if (z)
                goto LabelCAB4;//CAAB
            if (!z)
                goto LabelCAB2;//CAAD
            LabelCAAF:
            LabelBF02(); //CAAF
            return;
            LabelCAB2:
            ldy(0x01);//CAB2
            LabelCAB4:
            ldam(0x58 + x);//CAB4
            pha();//CAB6
            if (!n)
                goto LabelCABB;//CAB7
            iny();//CAB9
            iny();//CABA
            LabelCABB:
            c = false;//CABB
            adc(ram[0xC9D0 + y]);//CABC
            str(0x58 + x, a);//CABF
            LabelBF02();//CAC1
            pla();//CAC4
            str(0x58 + x, a);//CAC5
            return;
            LabelCAC8:
            ldam(0x0796 + x);//CAC8
            if (!z)
                goto LabelCAEB;//CACB
            str(0x1E + x, a);//CACD
            ldam(0x09);//CACF
            and(0x01);//CAD1
            tay();//CAD3
            iny();//CAD4
            str(0x46 + x, y);//CAD5
            dey();//CAD7
            ldam(0x076A);//CAD8
            if (z)
                goto LabelCADF;//CADB
            iny();//CADD
            iny();//CADE
            LabelCADF:
            ldam(0xC9D4 + y);//CADF
            str(0x58 + x, a);//CAE2
            return;
            LabelCAE5:
            LabelBF63();//CAE5
            LabelBF02(); //CAE8
            return;
            LabelCAEB:
            cmp(a, 0x0E);//CAEB
            if (!z)
                return;//CAED
            ldam(0x16 + x);//CAEF
            cmp(a, 0x06);//CAF1
            if (!z)
                return;//CAF3
            LabelC998();//CAF5
        }

        void LabelCA58()
        {
            ldy(0xFC);//CA58
            ldam(0x09);//CA5A
            and(0x40);//CA5C
            if (!z)
                goto LabelCA62;//CA5E
            ldy(0x04);//CA60
            LabelCA62:
            str(0x58 + x, y);//CA62
            ldy(0x01);//CA64
            LabelE143();//CA66
            if (n) //
                goto LabelCA75;//CA69
            iny();//CA6B
            ldam(0x0796 + x);//CA6C
            if (!z)
                goto LabelCA75;//CA6F
            lda(0xF8);//CA71
            str(0x58 + x, a);//CA73
            LabelCA75:
            str(0x46 + x, y);//CA75
            ldy(0x00);//CA77
            ldam(0x1E + x);//CA79
            and(0x40);//CA7B
            if (!z)
                goto LabelCA98;//CA7D
            ldam(0x1E + x);//CA7F
            asl();//CA81
            if (c)
                goto LabelCAB4;//CA82
            ldam(0x1E + x);//CA84
            and(0x20);//CA86
            if (!z)
                goto LabelCAE5;//CA88
            ldam(0x1E + x);//CA8A
            and(0x07);//CA8C
            if (z)
                goto LabelCAB4;//CA8E
            cmp(a, 0x05);//CA90
            if (z)
                goto LabelCA98;//CA92
            cmp(a, 0x03);//CA94
            if (c)
                goto LabelCAC8;//CA96
            LabelCA98:
            LabelBF63();//CA98
            ldy(0x00);//CA9B
            ldam(0x1E + x);//CA9D
            cmp(a, 0x02);//CA9F
            if (z)
                goto LabelCAAF;//CAA1
            and(0x40);//CAA3
            if (z)
                goto LabelCAB4;//CAA5
            ldam(0x16 + x);//CAA7
            cmp(a, 0x2E);//CAA9
            if (z)
                goto LabelCAB4;//CAAB
            if (!z)
                goto LabelCAB2;//CAAD
            LabelCAAF:
            LabelBF02(); //CAAF
            return;
            LabelCAB2:
            ldy(0x01);//CAB2
            LabelCAB4:
            ldam(0x58 + x);//CAB4
            pha();//CAB6
            if (!n)
                goto LabelCABB;//CAB7
            iny();//CAB9
            iny();//CABA
            LabelCABB:
            c = false;//CABB
            adc(ram[0xC9D0 + y]);//CABC
            str(0x58 + x, a);//CABF
            LabelBF02();//CAC1
            pla();//CAC4
            str(0x58 + x, a);//CAC5
            return;
            LabelCAC8:
            ldam(0x0796 + x);//CAC8
            if (!z)
                goto LabelCAEB;//CACB
            str(0x1E + x, a);//CACD
            ldam(0x09);//CACF
            and(0x01);//CAD1
            tay();//CAD3
            iny();//CAD4
            str(0x46 + x, y);//CAD5
            dey();//CAD7
            ldam(0x076A);//CAD8
            if (z)
                goto LabelCADF;//CADB
            iny();//CADD
            iny();//CADE
            LabelCADF:
            ldam(0xC9D4 + y);//CADF
            str(0x58 + x, a);//CAE2
            return;
            LabelCAE5:
            LabelBF63();//CAE5
            LabelBF02(); //CAE8
            return;
            LabelCAEB:
            cmp(a, 0x0E);//CAEB
            if (!z)
                return;//CAED
            ldam(0x16 + x);//CAEF
            cmp(a, 0x06);//CAF1
            if (!z)
                return;//CAF3
            LabelC998();//CAF5
        }

        void LabelCA77()
        {
            ldy(0x00);//CA77
            ldam(0x1E + x);//CA79
            and(0x40);//CA7B
            if (!z)
                goto LabelCA98;//CA7D
            ldam(0x1E + x);//CA7F
            asl();//CA81
            if (c)
                goto LabelCAB4;//CA82
            ldam(0x1E + x);//CA84
            and(0x20);//CA86
            if (!z)
                goto LabelCAE5;//CA88
            ldam(0x1E + x);//CA8A
            and(0x07);//CA8C
            if (z)
                goto LabelCAB4;//CA8E
            cmp(a, 0x05);//CA90
            if (z)
                goto LabelCA98;//CA92
            cmp(a, 0x03);//CA94
            if (c)
                goto LabelCAC8;//CA96
            LabelCA98:
            LabelBF63();//CA98
            ldy(0x00);//CA9B
            ldam(0x1E + x);//CA9D
            cmp(a, 0x02);//CA9F
            if (z)
                goto LabelCAAF;//CAA1
            and(0x40);//CAA3
            if (z)
                goto LabelCAB4;//CAA5
            ldam(0x16 + x);//CAA7
            cmp(a, 0x2E);//CAA9
            if (z)
                goto LabelCAB4;//CAAB
            if (!z)
                goto LabelCAB2;//CAAD
            LabelCAAF:
            LabelBF02(); //CAAF
            return;
            LabelCAB2:
            ldy(0x01);//CAB2
            LabelCAB4:
            ldam(0x58 + x);//CAB4
            pha();//CAB6
            if (!n)
                goto LabelCABB;//CAB7
            iny();//CAB9
            iny();//CABA
            LabelCABB:
            c = false;//CABB
            adc(ram[0xC9D0 + y]);//CABC
            str(0x58 + x, a);//CABF
            LabelBF02();//CAC1
            pla();//CAC4
            str(0x58 + x, a);//CAC5
            return;
            LabelCAC8:
            ldam(0x0796 + x);//CAC8
            if (!z)
                goto LabelCAEB;//CACB
            str(0x1E + x, a);//CACD
            ldam(0x09);//CACF
            and(0x01);//CAD1
            tay();//CAD3
            iny();//CAD4
            str(0x46 + x, y);//CAD5
            dey();//CAD7
            ldam(0x076A);//CAD8
            if (z)
                goto LabelCADF;//CADB
            iny();//CADD
            iny();//CADE
            LabelCADF:
            ldam(0xC9D4 + y);//CADF
            str(0x58 + x, a);//CAE2
            return;
            LabelCAE5:
            LabelBF63();//CAE5
            LabelBF02(); //CAE8
            return;
            LabelCAEB:
            cmp(a, 0x0E);//CAEB
            if (!z)
                return;//CAED
            ldam(0x16 + x);//CAEF
            cmp(a, 0x06);//CAF1
            if (!z)
                return;//CAF3
            LabelC998();//CAF5
        }

        void LabelCAE5()
        {
            LabelBF63();//CAE5
            LabelBF02(); //CAE8
            return;
        }

        void LabelCAF9()
        {
            LabelBF92();//CAF9
            LabelBF02(); //CAFC
            return;
        }

        void LabelCAFF()
        {
            ldam(0xA0 + x);//CAFF
            orm(0x0434 + x);//CB01
            if (!z)
                goto LabelCB19;//CB04
            str(0x0417 + x, a);//CB06
            ldam(0xCF + x);//CB09
            cmp(a, ram[0x0401 + x]);//CB0B
            if (c)
                goto LabelCB19;//CB0E
            ldam(0x09);//CB10
            and(0x07);//CB12
            if (!z)
                return;//CB14
            inc(0xCF + x);//CB16
            return;
            LabelCB19:
            ldam(0xCF + x);//CB19
            cmp(a, ram[0x58 + x]);//CB1B
            if (!c)
                goto LabelCB22;//CB1D
            LabelBF75(); //CB1F
            return;
            LabelCB22:
            LabelBF70(); //CB22
            return;
        }

        void LabelCB25()
        {
            LabelCB45();//CB25
            LabelCB66();//CB28
            ldy(0x01);//CB2B
            ldam(0x09);//CB2D
            and(0x03);//CB2F
            if (!z)
                return;//CB31
            ldam(0x09);//CB33
            and(0x40);//CB35
            if (!z)
                goto LabelCB3B;//CB37
            ldy(0xFF);//CB39
            LabelCB3B:
            str(0x00, y);//CB3B
            ldam(0xCF + x);//CB3D
            c = false;//CB3F
            adc(ram[0x00]);//CB40
            str(0xCF + x, a);//CB42
        }

        void LabelCB45()
        {
            lda(0x13);//CB45
            str(0x01, a);//CB47
            ldam(0x09);//CB49
            and(0x03);//CB4B
            if (!z)
                return;//CB4D
            ldym(0x58 + x);//CB4F
            ldam(0xA0 + x);//CB51
            lsr();//CB53
            if (c)
                goto LabelCB60;//CB54
            cmp(y, ram[0x01]);//CB56
            if (z)
                goto LabelCB5D;//CB58
            inc(0x58 + x);//CB5A
            return;
            LabelCB5D:
            inc(0xA0 + x);//CB5D
            return;
            LabelCB60:
            tya();//CB60
            if (z)
                goto LabelCB5D;//CB61
            dec(0x58 + x);//CB63
        }

        void LabelCB47()
        {
            str(0x01, a);//CB47
            ldam(0x09);//CB49
            and(0x03);//CB4B
            if (!z)
                return;//CB4D
            ldym(0x58 + x);//CB4F
            ldam(0xA0 + x);//CB51
            lsr();//CB53
            if (c)
                goto LabelCB60;//CB54
            cmp(y, ram[0x01]);//CB56
            if (z)
                goto LabelCB5D;//CB58
            inc(0x58 + x);//CB5A
            return;
            LabelCB5D:
            inc(0xA0 + x);//CB5D
            return;
            LabelCB60:
            tya();//CB60
            if (z)
                goto LabelCB5D;//CB61
            dec(0x58 + x);//CB63
        }

        void LabelCB66()
        {
            ldam(0x58 + x);//CB66
            pha();//CB68
            ldy(0x01);//CB69
            ldam(0xA0 + x);//CB6B
            and(0x02);//CB6D
            if (!z)
                goto LabelCB7C;//CB6F
            ldam(0x58 + x);//CB71
            eor(0xFF);//CB73
            c = false;//CB75
            adc(0x01);//CB76
            str(0x58 + x, a);//CB78
            ldy(0x02);//CB7A
            LabelCB7C:
            str(0x46 + x, y);//CB7C
            LabelBF02();//CB7E
            str(0x00, a);//CB81
            pla();//CB83
            str(0x58 + x, a);//CB84
        }

        void LabelCB89()
        {
            ldam(0x1E + x);//CB89
            and(0x20);//CB8B
            if (!z)
                goto LabelCBDC;//CB8D
            ldym(0x06CC);//CB8F
            ldam(0x07A8 + x);//CB92
            and(ram[0xCB87 + y]);//CB95
            if (!z)
                goto LabelCBAC;//CB98
            txa();//CB9A
            lsr();//CB9B
            if (!c)
                goto LabelCBA2;//CB9C
            ldym(0x45);//CB9E
            if (c)
                goto LabelCBAA;//CBA0
            LabelCBA2:
            ldy(0x02);//CBA2
            LabelE143();//CBA4
            if (!n)
                goto LabelCBAA;//CBA7
            dey();//CBA9
            LabelCBAA:
            str(0x46 + x, y);//CBAA
            LabelCBAC:
            LabelCBDF();//CBAC
            ldam(0xCF + x);//CBAF
            c = true;//CBB1
            sbc(ram[0x0434 + x]);//CBB2
            cmp(a, 0x20);//CBB5
            if (!c)
                goto LabelCBBB;//CBB7
            str(0xCF + x, a);//CBB9
            LabelCBBB:
            ldym(0x46 + x);//CBBB
            dey();//CBBD
            if (!z)
                goto LabelCBCE;//CBBE
            ldam(0x87 + x);//CBC0
            c = false;//CBC2
            adc(ram[0x58 + x]);//CBC3
            str(0x87 + x, a);//CBC5
            ldam(0x6E + x);//CBC7
            adc(0x00);//CBC9
            str(0x6E + x, a);//CBCB
            return;
            LabelCBCE:
            ldam(0x87 + x);//CBCE
            c = true;//CBD0
            sbc(ram[0x58 + x]);//CBD1
            str(0x87 + x, a);//CBD3
            ldam(0x6E + x);//CBD5
            sbc(0x00);//CBD7
            str(0x6E + x, a);//CBD9
            return;
            LabelCBDC:
            LabelBF8C(); //CBDC
            return;
        }

        void LabelCBDF()
        {
            ldam(0xA0 + x);//CBDF
            and(0x02);//CBE1
            if (!z)
                goto LabelCC1C;//CBE3
            ldam(0x09);//CBE5
            and(0x07);//CBE7
            pha();//CBE9
            ldam(0xA0 + x);//CBEA
            lsr();//CBEC
            if (c)
                goto LabelCC04;//CBED
            pla();//CBEF
            if (!z)
                return;//CBF0
            ldam(0x0434 + x);//CBF2
            c = false;//CBF5
            adc(0x01);//CBF6
            str(0x0434 + x, a);//CBF8
            str(0x58 + x, a);//CBFB
            cmp(a, 0x02);//CBFD
            if (!z)
                return;//CBFF
            inc(0xA0 + x);//CC01
            return;
            LabelCC04:
            pla();//CC04
            if (!z)
                return;//CC05
            ldam(0x0434 + x);//CC07
            c = true;//CC0A
            sbc(0x01);//CC0B
            str(0x0434 + x, a);//CC0D
            str(0x58 + x, a);//CC10
            if (!z)
                return;//CC12
            inc(0xA0 + x);//CC14
            lda(0x02);//CC16
            str(0x0796 + x, a);//CC18
            return;
            LabelCC1C:
            ldam(0x0796 + x);//CC1C
            if (z)
                goto LabelCC29;//CC1F
            LabelCC21:
            ldam(0x09);//CC21
            lsr();//CC23
            if (c)
                return;//CC24
            inc(0xCF + x);//CC26
            return;
            LabelCC29:
            ldam(0xCF + x);//CC29
            adc(0x10);//CC2B
            cmp(a, ram[0xCE]);//CC2D
            if (!c)
                goto LabelCC21;//CC2F
            lda(0x00);//CC31
            str(0xA0 + x, a);//CC33
        }

        void LabelCC36()
        {
            ldam(0x1E + x);//CC36
            and(0x20);//CC38
            if (z)
                goto LabelCC3F;//CC3A
            LabelBF92(); //CC3C
            return;
            LabelCC3F:
            lda(0xE8);//CC3F
            str(0x58 + x, a);//CC41
            LabelBF02(); //CC43
            return;
        }

        void LabelCC4A()
        {
            ldam(0x1E + x);//CC4A
            and(0x20);//CC4C
            if (z)
                goto LabelCC53;//CC4E
            LabelBF8C(); //CC50
            return;
            LabelCC53:
            str(0x03, a);//CC53
            ldam(0x16 + x);//CC55
            c = true;//CC57
            sbc(0x0A);//CC58
            tay();//CC5A
            ldam(0xCC46 + y);//CC5B
            str(0x02, a);//CC5E
            ldam(0x0401 + x);//CC60
            c = true;//CC63
            sbc(ram[0x02]);//CC64
            str(0x0401 + x, a);//CC66
            ldam(0x87 + x);//CC69
            sbc(0x00);//CC6B
            str(0x87 + x, a);//CC6D
            ldam(0x6E + x);//CC6F
            sbc(0x00);//CC71
            str(0x6E + x, a);//CC73
            lda(0x20);//CC75
            str(0x02, a);//CC77
            cmp(x, 0x02);//CC79
            if (!c)
                return;//CC7B
            ldam(0x58 + x);//CC7D
            cmp(a, 0x10);//CC7F
            if (!c)
                goto LabelCC99;//CC81
            ldam(0x0417 + x);//CC83
            c = false;//CC86
            adc(ram[0x02]);//CC87
            str(0x0417 + x, a);//CC89
            ldam(0xCF + x);//CC8C
            adc(ram[0x03]);//CC8E
            str(0xCF + x, a);//CC90
            ldam(0xB6 + x);//CC92
            adc(0x00);//CC94
            goto LabelCCAC; //CC96
            LabelCC99:
            ldam(0x0417 + x);//CC99
            c = true;//CC9C
            sbc(ram[0x02]);//CC9D
            str(0x0417 + x, a);//CC9F
            ldam(0xCF + x);//CCA2
            sbc(ram[0x03]);//CCA4
            str(0xCF + x, a);//CCA6
            ldam(0xB6 + x);//CCA8
            sbc(0x00);//CCAA
            LabelCCAC:
            str(0xB6 + x, a);//CCAC
            ldy(0x00);//CCAE
            ldam(0xCF + x);//CCB0
            c = true;//CCB2
            sbc(ram[0x0434 + x]);//CCB3
            if (!n)
                goto LabelCCBF;//CCB6
            ldy(0x10);//CCB8
            eor(0xFF);//CCBA
            c = false;//CCBC
            adc(0x01);//CCBD
            LabelCCBF:
            cmp(a, 0x0F);//CCBF
            if (!c)
                return;//CCC1
            tya();//CCC3
            str(0x58 + x, a);//CCC4
        }

        void LabelCD3C()
        {
            LabelF1AF();//CD3C
            ldam(0x03D1);//CD3F
            and(0x08);//CD42
            if (!z)
                return;//CD44
            ldam(0x0747);//CD46
            if (!z)
                goto LabelCD55;//CD49
            ldam(0x0388 + x);//CD4B
            LabelD410();//CD4E
            and(0x1F);//CD51
            str(0xA0 + x, a);//CD53
            LabelCD55:
            ldam(0xA0 + x);//CD55
            ldym(0x16 + x);//CD57
            cmp(y, 0x1F);//CD59
            if (!c)
                goto LabelCD6A;//CD5B
            cmp(a, 0x08);//CD5D
            if (z)
                goto LabelCD65;//CD5F
            cmp(a, 0x18);//CD61
            if (!z)
                goto LabelCD6A;//CD63
            LabelCD65:
            c = false;//CD65
            adc(0x01);//CD66
            str(0xA0 + x, a);//CD68
            LabelCD6A:
            str(0xEF, a);//CD6A
            LabelF152();//CD6C
            LabelCE8E();//CD6F
            ldym(0x06E5 + x);//CD72
            ldam(0x03B9);//CD75
            str(0x0200 + y, a);//CD78
            str(0x07, a);//CD7B
            ldam(0x03AE);//CD7D
            str(0x0203 + y, a);//CD80
            str(0x06, a);//CD83
            lda(0x01);//CD85
            str(0x00, a);//CD87
            LabelCE08();//CD89
            ldy(0x05);//CD8C
            ldam(0x16 + x);//CD8E
            cmp(a, 0x1F);//CD90
            if (!c)
                goto LabelCD96;//CD92
            ldy(0x0B);//CD94
            LabelCD96:
            str(0xED, y);//CD96
            lda(0x00);//CD98
            str(0x00, a);//CD9A
            LabelCD9C:
            ldam(0xEF);//CD9C
            LabelCE8E();//CD9E
            LabelCDBB();//CDA1
            ldam(0x00);//CDA4
            cmp(a, 0x04);//CDA6
            if (!z)
                goto LabelCDB2;//CDA8
            ldym(0x06CF);//CDAA
            ldam(0x06E5 + y);//CDAD
            str(0x06, a);//CDB0
            LabelCDB2:
            inc(0x00);//CDB2
            ldam(0x00);//CDB4
            cmp(a, ram[0xED]);//CDB6
            if (!c)
                goto LabelCD9C;//CDB8
        }

        void LabelCDBB()
        {
            ldam(0x03);//CDBB
            str(0x05, a);//CDBD
            ldym(0x06);//CDBF
            ldam(0x01);//CDC1
            lsrm(0x05);//CDC3
            if (c)
                goto LabelCDCB;//CDC5
            eor(0xFF);//CDC7
            adc(0x01);//CDC9
            LabelCDCB:
            c = false;//CDCB
            adc(ram[0x03AE]);//CDCC
            str(0x0203 + y, a);//CDCF
            str(0x06, a);//CDD2
            cmp(a, ram[0x03AE]);//CDD4
            if (c)
                goto LabelCDE2;//CDD7
            ldam(0x03AE);//CDD9
            c = true;//CDDC
            sbc(ram[0x06]);//CDDD
            goto LabelCDE6; //CDDF
            LabelCDE2:
            c = true;//CDE2
            sbc(ram[0x03AE]);//CDE3
            LabelCDE6:
            cmp(a, 0x59);//CDE6
            if (!c)
                goto LabelCDEE;//CDE8
            lda(0xF8);//CDEA
            if (!z)
                goto LabelCE03;//CDEC
            LabelCDEE:
            ldam(0x03B9);//CDEE
            cmp(a, 0xF8);//CDF1
            if (z)
                goto LabelCE03;//CDF3
            ldam(0x02);//CDF5
            lsrm(0x05);//CDF7
            if (c)
                goto LabelCDFF;//CDF9
            eor(0xFF);//CDFB
            adc(0x01);//CDFD
            LabelCDFF:
            c = false;//CDFF
            adc(ram[0x03B9]);//CE00
            LabelCE03:
            str(0x0200 + y, a);//CE03
            str(0x07, a);//CE06
            LabelECED();//CE08
            tya();//CE0B
            pha();//CE0C
            ldam(0x079F);//CE0D
            orm(0x0747);//CE10
            if (!z)
                goto LabelCE85;//CE13
            str(0x05, a);//CE15
            ldym(0xB5);//CE17
            dey();//CE19
            if (!z)
                goto LabelCE85;//CE1A
            ldym(0xCE);//CE1C
            ldam(0x0754);//CE1E
            if (!z)
                goto LabelCE28;//CE21
            ldam(0x0714);//CE23
            if (z)
                goto LabelCE31;//CE26
            LabelCE28:
            inc(0x05);//CE28
            inc(0x05);//CE2A
            tya();//CE2C
            c = false;//CE2D
            adc(0x18);//CE2E
            tay();//CE30
            LabelCE31:
            tya();//CE31
            c = true;//CE32
            sbc(ram[0x07]);//CE33
            if (!n)
                goto LabelCE3C;//CE35
            eor(0xFF);//CE37
            c = false;//CE39
            adc(0x01);//CE3A
            LabelCE3C:
            cmp(a, 0x08);//CE3C
            if (c)
                goto LabelCE5C;//CE3E
            ldam(0x06);//CE40
            cmp(a, 0xF0);//CE42
            if (c)
                goto LabelCE5C;//CE44
            ldam(0x0207);//CE46
            c = false;//CE49
            adc(0x04);//CE4A
            str(0x04, a);//CE4C
            c = true;//CE4E
            sbc(ram[0x06]);//CE4F
            if (!n)
                goto LabelCE58;//CE51
            eor(0xFF);//CE53
            c = false;//CE55
            adc(0x01);//CE56
            LabelCE58:
            cmp(a, 0x08);//CE58
            if (!c)
                goto LabelCE6F;//CE5A
            LabelCE5C:
            ldam(0x05);//CE5C
            cmp(a, 0x02);//CE5E
            if (z)
                goto LabelCE85;//CE60
            ldym(0x05);//CE62
            ldam(0xCE);//CE64
            c = false;//CE66
            adc(ram[0xCD3A + y]);//CE67
            inc(0x05);//CE6A
            LabelCE32(); //CE6C
            return;
            LabelCE6F:
            ldx(0x01);//CE6F
            ldam(0x04);//CE71
            cmp(a, ram[0x06]);//CE73
            if (c)
                goto LabelCE78;//CE75
            inx();//CE77
            LabelCE78:
            str(0x46, x);//CE78
            ldx(0x00);//CE7A
            ldam(0x00);//CE7C
            pha();//CE7E
            LabelD92C();//CE7F
            pla();//CE82
            str(0x00, a);//CE83
            LabelCE85:
            pla();//CE85
            c = false;//CE86
            adc(0x04);//CE87
            str(0x06, a);//CE89
            ldxm(0x08);//CE8B
        }

        void LabelCE32()
        {
            c = true;//CE32
            sbc(ram[0x07]);//CE33
            if (!n)
                goto LabelCE3C;//CE35
            eor(0xFF);//CE37
            c = false;//CE39
            adc(0x01);//CE3A
            LabelCE3C:
            cmp(a, 0x08);//CE3C
            if (c)
                goto LabelCE5C;//CE3E
            ldam(0x06);//CE40
            cmp(a, 0xF0);//CE42
            if (c)
                goto LabelCE5C;//CE44
            ldam(0x0207);//CE46
            c = false;//CE49
            adc(0x04);//CE4A
            str(0x04, a);//CE4C
            c = true;//CE4E
            sbc(ram[0x06]);//CE4F
            if (!n)
                goto LabelCE58;//CE51
            eor(0xFF);//CE53
            c = false;//CE55
            adc(0x01);//CE56
            LabelCE58:
            cmp(a, 0x08);//CE58
            if (!c)
                goto LabelCE6F;//CE5A
            LabelCE5C:
            ldam(0x05);//CE5C
            cmp(a, 0x02);//CE5E
            if (z)
                goto LabelCE85;//CE60
            ldym(0x05);//CE62
            ldam(0xCE);//CE64
            c = false;//CE66
            adc(ram[0xCD3A + y]);//CE67
            inc(0x05);//CE6A
            LabelCE32(); //CE6C
            return;
            LabelCE6F:
            ldx(0x01);//CE6F
            ldam(0x04);//CE71
            cmp(a, ram[0x06]);//CE73
            if (c)
                goto LabelCE78;//CE75
            inx();//CE77
            LabelCE78:
            str(0x46, x);//CE78
            ldx(0x00);//CE7A
            ldam(0x00);//CE7C
            pha();//CE7E
            LabelD92C();//CE7F
            pla();//CE82
            str(0x00, a);//CE83
            LabelCE85:
            pla();//CE85
            c = false;//CE86
            adc(0x04);//CE87
            str(0x06, a);//CE89
            ldxm(0x08);//CE8B
        }

        void LabelCE08()
        {
            LabelECED();//CE08
            tya();//CE0B
            pha();//CE0C
            ldam(0x079F);//CE0D
            orm(0x0747);//CE10
            if (!z)
                goto LabelCE85;//CE13
            str(0x05, a);//CE15
            ldym(0xB5);//CE17
            dey();//CE19
            if (!z)
                goto LabelCE85;//CE1A
            ldym(0xCE);//CE1C
            ldam(0x0754);//CE1E
            if (!z)
                goto LabelCE28;//CE21
            ldam(0x0714);//CE23
            if (z)
                goto LabelCE31;//CE26
            LabelCE28:
            inc(0x05);//CE28
            inc(0x05);//CE2A
            tya();//CE2C
            c = false;//CE2D
            adc(0x18);//CE2E
            tay();//CE30
            LabelCE31:
            tya();//CE31
            c = true;//CE32
            sbc(ram[0x07]);//CE33
            if (!n)
                goto LabelCE3C;//CE35
            eor(0xFF);//CE37
            c = false;//CE39
            adc(0x01);//CE3A
            LabelCE3C:
            cmp(a, 0x08);//CE3C
            if (c)
                goto LabelCE5C;//CE3E
            ldam(0x06);//CE40
            cmp(a, 0xF0);//CE42
            if (c)
                goto LabelCE5C;//CE44
            ldam(0x0207);//CE46
            c = false;//CE49
            adc(0x04);//CE4A
            str(0x04, a);//CE4C
            c = true;//CE4E
            sbc(ram[0x06]);//CE4F
            if (!n)
                goto LabelCE58;//CE51
            eor(0xFF);//CE53
            c = false;//CE55
            adc(0x01);//CE56
            LabelCE58:
            cmp(a, 0x08);//CE58
            if (!c)
                goto LabelCE6F;//CE5A
            LabelCE5C:
            ldam(0x05);//CE5C
            cmp(a, 0x02);//CE5E
            if (z)
                goto LabelCE85;//CE60
            ldym(0x05);//CE62
            ldam(0xCE);//CE64
            c = false;//CE66
            adc(ram[0xCD3A + y]);//CE67
            inc(0x05);//CE6A
            LabelCE32(); //CE6C
            return;
            LabelCE6F:
            ldx(0x01);//CE6F
            ldam(0x04);//CE71
            cmp(a, ram[0x06]);//CE73
            if (c)
                goto LabelCE78;//CE75
            inx();//CE77
            LabelCE78:
            str(0x46, x);//CE78
            ldx(0x00);//CE7A
            ldam(0x00);//CE7C
            pha();//CE7E
            LabelD92C();//CE7F
            pla();//CE82
            str(0x00, a);//CE83
            LabelCE85:
            pla();//CE85
            c = false;//CE86
            adc(0x04);//CE87
            str(0x06, a);//CE89
            ldxm(0x08);//CE8B
        }

        void LabelCE8E()
        {
            pha();//CE8E
            and(0x0F);//CE8F
            cmp(a, 0x09);//CE91
            if (!c)
                goto LabelCE9A;//CE93
            eor(0x0F);//CE95
            c = false;//CE97
            adc(0x01);//CE98
            LabelCE9A:
            str(0x01, a);//CE9A
            ldym(0x00);//CE9C
            ldam(0xCD2E + y);//CE9E
            c = false;//CEA1
            adc(ram[0x01]);//CEA2
            tay();//CEA4
            ldam(0xCCC7 + y);//CEA5
            str(0x01, a);//CEA8
            pla();//CEAA
            pha();//CEAB
            c = false;//CEAC
            adc(0x08);//CEAD
            and(0x0F);//CEAF
            cmp(a, 0x09);//CEB1
            if (!c)
                goto LabelCEBA;//CEB3
            eor(0x0F);//CEB5
            c = false;//CEB7
            adc(0x01);//CEB8
            LabelCEBA:
            str(0x02, a);//CEBA
            ldym(0x00);//CEBC
            ldam(0xCD2E + y);//CEBE
            c = false;//CEC1
            adc(ram[0x02]);//CEC2
            tay();//CEC4
            ldam(0xCCC7 + y);//CEC5
            str(0x02, a);//CEC8
            pla();//CECA
            lsr();//CECB
            lsr();//CECC
            lsr();//CECD
            tay();//CECE
            ldam(0xCD2A + y);//CECF
            str(0x03, a);//CED2
        }

        void LabelCEDF()
        {
            ldam(0x1E + x);//CEDF
            and(0x20);//CEE1
            if (z)
                goto LabelCEED;//CEE3
            lda(0x00);//CEE5
            str(0x03C5 + x, a);//CEE7
            LabelBF92(); //CEEA
            return;
            LabelCEED:
            LabelBF02();//CEED
            ldy(0x0D);//CEF0
            LabelCEF2:
            lda(0x05);//CEF2
            LabelBF96();//CEF4
            ldam(0x0434 + x);//CEF7
            lsr();//CEFA
            lsr();//CEFB
            lsr();//CEFC
            lsr();//CEFD
            tay();//CEFE
            ldam(0xCF + x);//CEFF
            c = true;//CF01
            sbc(ram[0xCED5 + y]);//CF02
            if (!n)
                goto LabelCF0C;//CF05
            eor(0xFF);//CF07
            c = false;//CF09
            adc(0x01);//CF0A
            LabelCF0C:
            cmp(a, 0x08);//CF0C
            if (c)
                goto LabelCF1E;//CF0E
            ldam(0x0434 + x);//CF10
            c = false;//CF13
            adc(0x10);//CF14
            str(0x0434 + x, a);//CF16
            lsr();//CF19
            lsr();//CF1A
            lsr();//CF1B
            lsr();//CF1C
            tay();//CF1D
            LabelCF1E:
            ldam(0xCEDA + y);//CF1E
            str(0x03C5 + x, a);//CF21
        }

        void LabelCF28()
        {
            ldam(0x1E + x);//CF28
            and(0x20);//CF2A
            if (z)
                goto LabelCF31;//CF2C
            LabelBF63(); //CF2E
            return;
            LabelCF31:
            ldam(0x1E + x);//CF31
            if (z)
                goto LabelCF40;//CF33
            lda(0x00);//CF35
            str(0xA0 + x, a);//CF37
            str(0x06CB, a);//CF39
            lda(0x10);//CF3C
            if (!z)
                goto LabelCF53;//CF3E
            LabelCF40:
            lda(0x12);//CF40
            str(0x06CB, a);//CF42
            ldy(0x02);//CF45
            LabelCF47:
            ldam(0xCF25 + y);//CF47
            str(0x0001 + y, a);//CF4A
            dey();//CF4D
            if (!n)
                goto LabelCF47;//CF4E
            LabelCF6C();//CF50
            LabelCF53:
            str(0x58 + x, a);//CF53
            ldy(0x01);//CF55
            ldam(0xA0 + x);//CF57
            and(0x01);//CF59
            if (!z)
                goto LabelCF67;//CF5B
            ldam(0x58 + x);//CF5D
            eor(0xFF);//CF5F
            c = false;//CF61
            adc(0x01);//CF62
            str(0x58 + x, a);//CF64
            iny();//CF66
            LabelCF67:
            str(0x46 + x, y);//CF67
            LabelBF02(); //CF69
            return;
        }

        void LabelCF6C()
        {
            ldy(0x00);//CF6C
            LabelE143();//CF6E
            if (!n)
                goto LabelCF7D;//CF71
            iny();//CF73
            ldam(0x00);//CF74
            eor(0xFF);//CF76
            c = false;//CF78
            adc(0x01);//CF79
            str(0x00, a);//CF7B
            LabelCF7D:
            ldam(0x00);//CF7D
            cmp(a, 0x3C);//CF7F
            if (!c)
                goto LabelCF9F;//CF81
            lda(0x3C);//CF83
            str(0x00, a);//CF85
            ldam(0x16 + x);//CF87
            cmp(a, 0x11);//CF89
            if (!z)
                goto LabelCF9F;//CF8B
            tya();//CF8D
            cmp(a, ram[0xA0 + x]);//CF8E
            if (z)
                goto LabelCF9F;//CF90
            ldam(0xA0 + x);//CF92
            if (z)
                goto LabelCF9C;//CF94
            dec(0x58 + x);//CF96
            ldam(0x58 + x);//CF98
            if (!z)
                return;//CF9A
            LabelCF9C:
            tya();//CF9C
            str(0xA0 + x, a);//CF9D
            LabelCF9F:
            ldam(0x00);//CF9F
            and(0x3C);//CFA1
            lsr();//CFA3
            lsr();//CFA4
            str(0x00, a);//CFA5
            ldy(0x00);//CFA7
            ldam(0x57);//CFA9
            if (z)
                goto LabelCFD1;//CFAB
            ldam(0x0775);//CFAD
            if (z)
                goto LabelCFD1;//CFB0
            iny();//CFB2
            ldam(0x57);//CFB3
            cmp(a, 0x19);//CFB5
            if (!c)
                goto LabelCFC1;//CFB7
            ldam(0x0775);//CFB9
            cmp(a, 0x02);//CFBC
            if (!c)
                goto LabelCFC1;//CFBE
            iny();//CFC0
            LabelCFC1:
            ldam(0x16 + x);//CFC1
            cmp(a, 0x12);//CFC3
            if (!z)
                goto LabelCFCB;//CFC5
            ldam(0x57);//CFC7
            if (!z)
                goto LabelCFD1;//CFC9
            LabelCFCB:
            ldam(0xA0 + x);//CFCB
            if (!z)
                goto LabelCFD1;//CFCD
            ldy(0x00);//CFCF
            LabelCFD1:
            ldam(0x0001 + y);//CFD1
            ldym(0x00);//CFD4
            LabelCFD6:
            c = true;//CFD6
            sbc(0x01);//CFD7
            dey();//CFD9
            if (!n)
                goto LabelCFD6;//CFDA
        }

        void LabelCFEC()
        {
            ldxm(0x0368);//CFEC
            ldam(0x16 + x);//CFEF
            cmp(a, 0x2D);//CFF1
            if (!z)
                goto LabelD005;//CFF3
            str(0x08, x);//CFF5
            ldam(0x1E + x);//CFF7
            if (z)
                goto LabelD015;//CFF9
            and(0x40);//CFFB
            if (z)
                goto LabelD005;//CFFD
            ldam(0xCF + x);//CFFF
            cmp(a, 0xE0);//D001
            if (!c)
                goto LabelD00F;//D003
            LabelD005:
            lda(0x80);//D005
            str(0xFC, a);//D007
            inc(0x0772);//D009
            LabelD071(); //D00C
            return;
            LabelD00F:
            LabelBF8C();//D00F
            LabelD17B(); //D012
            return;
            LabelD015:
            dec(0x0364);//D015
            if (!z)
                goto LabelD05E;//D018
            lda(0x04);//D01A
            LabelD01C:
            str(0x0364, a);//D01C
            ldam(0x0363);//D01F
            eor(0x01);//D022
            str(0x0363, a);//D024
            lda(0x22);//D027
            str(0x05, a);//D029
            ldym(0x0369);//D02B
            ldam(0xCFDD + y);//D02E
            str(0x04, a);//D031
            ldym(0x0300);//D033
            iny();//D036
            ldx(0x0C);//D037
            Label8ACD();//D039
            ldxm(0x08);//D03C
            Label8A8F();//D03E
            lda(0x08);//D041
            str(0xFE, a);//D043
            lda(0x01);//D045
            str(0xFD, a);//D047
            inc(0x0369);//D049
            ldam(0x0369);//D04C
            cmp(a, 0x0F);//D04F
            if (!z)
                goto LabelD05E;//D051
            LabelC363();//D053
            lda(0x40);//D056
            str(0x1E + x, a);//D058
            lda(0x80);//D05A
            str(0xFE, a);//D05C
            LabelD05E:
            LabelD17B(); //D05E
            return;
        }

        void LabelD00F()
        {
            LabelBF8C();//D00F
            LabelD17B(); //D012
            return;
        }

        void LabelD065()
        {
            ldam(0x1E + x);//D065
            and(0x20);//D067
            if (z)
                goto LabelD07F;//D069
            ldam(0xCF + x);//D06B
            cmp(a, 0xE0);//D06D
            if (!c)
            {
                LabelD00F();//D06F
                return;
            }
            ldx(0x04);//D071
            LabelD073:
            LabelC998();//D073
            dex();//D076
            if (!n)
                goto LabelD073;//D077
            str(0x06CB, a);//D079
            ldxm(0x08);//D07C
            return;
            LabelD07F:
            lda(0x00);//D07F
            str(0x06CB, a);//D081
            ldam(0x0747);//D084
            if (z)
                goto LabelD08C;//D087
            LabelD139(); //D089
            return;
            LabelD08C:
            ldam(0x0363);//D08C
            if (!n)
                goto LabelD094;//D08F
            goto LabelD10F; //D091
            LabelD094:
            dec(0x0364);//D094
            if (!z)
                goto LabelD0A6;//D097
            lda(0x20);//D099
            str(0x0364, a);//D09B
            ldam(0x0363);//D09E
            eor(0x01);//D0A1
            str(0x0363, a);//D0A3
            LabelD0A6:
            ldam(0x09);//D0A6
            and(0x0F);//D0A8
            if (!z)
                goto LabelD0B0;//D0AA
            lda(0x02);//D0AC
            str(0x46 + x, a);//D0AE
            LabelD0B0:
            ldam(0x078A + x);//D0B0
            if (z)
                goto LabelD0D1;//D0B3
            LabelE143();//D0B5
            if (!n)
                goto LabelD0D1;//D0B8
            lda(0x01);//D0BA
            str(0x46 + x, a);//D0BC
            lda(0x02);//D0BE
            str(0x0365, a);//D0C0
            lda(0x20);//D0C3
            str(0x078A + x, a);//D0C5
            str(0x0790, a);//D0C8
            ldam(0x87 + x);//D0CB
            cmp(a, 0xC8);//D0CD
            if (c)
                goto LabelD10F;//D0CF
            LabelD0D1:
            ldam(0x09);//D0D1
            and(0x03);//D0D3
            if (!z)
                goto LabelD10F;//D0D5
            ldam(0x87 + x);//D0D7
            cmp(a, ram[0x0366]);//D0D9
            if (!z)
                goto LabelD0EA;//D0DC
            ldam(0x07A7 + x);//D0DE
            and(0x03);//D0E1
            tay();//D0E3
            ldam(0xD061 + y);//D0E4
            str(0x06DC, a);//D0E7
            LabelD0EA:
            ldam(0x87 + x);//D0EA
            c = false;//D0EC
            adc(ram[0x0365]);//D0ED
            str(0x87 + x, a);//D0F0
            ldym(0x46 + x);//D0F2
            cmp(y, 0x01);//D0F4
            if (z)
                goto LabelD10F;//D0F6
            ldy(0xFF);//D0F8
            c = true;//D0FA
            sbc(ram[0x0366]);//D0FB
            if (!n)
                goto LabelD107;//D0FE
            eor(0xFF);//D100
            c = false;//D102
            adc(0x01);//D103
            ldy(0x01);//D105
            LabelD107:
            cmp(a, ram[0x06DC]);//D107
            if (!c)
                goto LabelD10F;//D10A
            str(0x0365, y);//D10C
            LabelD10F:
            ldam(0x078A + x);//D10F
            if (!z)
                goto LabelD13C;//D112
            LabelBF8C();//D114
            ldam(0x075F);//D117
            cmp(a, 0x05);//D11A
            if (!c)
                goto LabelD127;//D11C
            ldam(0x09);//D11E
            and(0x03);//D120
            if (!z)
                goto LabelD127;//D122
            LabelBA94();//D124
            LabelD127:
            ldam(0xCF + x);//D127
            cmp(a, 0x80);//D129
            if (!c)
                goto LabelD149;//D12B
            ldam(0x07A7 + x);//D12D
            and(0x03);//D130
            tay();//D132
            ldam(0xD061 + y);//D133
            str(0x078A + x, a);//D136
            goto LabelD149; //D139
            LabelD13C:
            cmp(a, 0x01);//D13C
            if (!z)
                goto LabelD149;//D13E
            dec(0xCF + x);//D140
            LabelC363();//D142
            lda(0xFE);//D145
            str(0xA0 + x, a);//D147
            LabelD149:
            ldam(0x075F);//D149
            cmp(a, 0x07);//D14C
            if (z)
                goto LabelD154;//D14E
            cmp(a, 0x05);//D150
            if (c)
                goto LabelD17B;//D152
            LabelD154:
            ldam(0x0790);//D154
            if (!z)
                goto LabelD17B;//D157
            lda(0x20);//D159
            str(0x0790, a);//D15B
            ldam(0x0363);//D15E
            eor(0x80);//D161
            str(0x0363, a);//D163
            if (n) //
                goto LabelD149;//D166
            LabelD1D9();//D168
            ldym(0x06CC);//D16B
            if (z)
                goto LabelD173;//D16E
            c = true;//D170
            sbc(0x10);//D171
            LabelD173:
            str(0x0790, a);//D173
            lda(0x15);//D176
            str(0x06CB, a);//D178
            LabelD17B:
            LabelD1BC();//D17B
            ldy(0x10);//D17E
            ldam(0x46 + x);//D180
            lsr();//D182
            if (!c)
                goto LabelD187;//D183
            ldy(0xF0);//D185
            LabelD187:
            tya();//D187
            c = false;//D188
            adc(ram[0x87 + x]);//D189
            ldym(0x06CF);//D18B
            str(0x0087 + y, a);//D18E
            ldam(0xCF + x);//D191
            c = false;//D193
            adc(0x08);//D194
            str(0x00CF + y, a);//D196
            ldam(0x1E + x);//D199
            str(0x001E + y, a);//D19B
            ldam(0x46 + x);//D19E
            str(0x0046 + y, a);//D1A0
            ldam(0x08);//D1A3
            pha();//D1A5
            ldxm(0x06CF);//D1A6
            str(0x08, x);//D1A9
            lda(0x2D);//D1AB
            str(0x16 + x, a);//D1AD
            LabelD1BC();//D1AF
            pla();//D1B2
            str(0x08, a);//D1B3
            tax();//D1B5
            lda(0x00);//D1B6
            str(0x036A, a);//D1B8
        }

        void LabelD139()
        {
            goto LabelD149; //D139
            cmp(a, 0x01);//D13C
            if (!z)
                goto LabelD149;//D13E
            dec(0xCF + x);//D140
            LabelC363();//D142
            lda(0xFE);//D145
            str(0xA0 + x, a);//D147
            LabelD149:
            ldam(0x075F);//D149
            cmp(a, 0x07);//D14C
            if (z)
                goto LabelD154;//D14E
            cmp(a, 0x05);//D150
            if (c)
                goto LabelD17B;//D152
            LabelD154:
            ldam(0x0790);//D154
            if (!z)
                goto LabelD17B;//D157
            lda(0x20);//D159
            str(0x0790, a);//D15B
            ldam(0x0363);//D15E
            eor(0x80);//D161
            str(0x0363, a);//D163
            if (n) //
                goto LabelD149;//D166
            LabelD1D9();//D168
            ldym(0x06CC);//D16B
            if (z)
                goto LabelD173;//D16E
            c = true;//D170
            sbc(0x10);//D171
            LabelD173:
            str(0x0790, a);//D173
            lda(0x15);//D176
            str(0x06CB, a);//D178
            LabelD17B:
            LabelD1BC();//D17B
            ldy(0x10);//D17E
            ldam(0x46 + x);//D180
            lsr();//D182
            if (!c)
                goto LabelD187;//D183
            ldy(0xF0);//D185
            LabelD187:
            tya();//D187
            c = false;//D188
            adc(ram[0x87 + x]);//D189
            ldym(0x06CF);//D18B
            str(0x0087 + y, a);//D18E
            ldam(0xCF + x);//D191
            c = false;//D193
            adc(0x08);//D194
            str(0x00CF + y, a);//D196
            ldam(0x1E + x);//D199
            str(0x001E + y, a);//D19B
            ldam(0x46 + x);//D19E
            str(0x0046 + y, a);//D1A0
            ldam(0x08);//D1A3
            pha();//D1A5
            ldxm(0x06CF);//D1A6
            str(0x08, x);//D1A9
            lda(0x2D);//D1AB
            str(0x16 + x, a);//D1AD
            LabelD1BC();//D1AF
            pla();//D1B2
            str(0x08, a);//D1B3
            tax();//D1B5
            lda(0x00);//D1B6
            str(0x036A, a);//D1B8
        }

        void LabelD17B()
        {

            LabelD1BC();//D17B
            ldy(0x10);//D17E
            ldam(0x46 + x);//D180
            lsr();//D182
            if (!c)
                goto LabelD187;//D183
            ldy(0xF0);//D185
            LabelD187:
            tya();//D187
            c = false;//D188
            adc(ram[0x87 + x]);//D189
            ldym(0x06CF);//D18B
            str(0x0087 + y, a);//D18E
            ldam(0xCF + x);//D191
            c = false;//D193
            adc(0x08);//D194
            str(0x00CF + y, a);//D196
            ldam(0x1E + x);//D199
            str(0x001E + y, a);//D19B
            ldam(0x46 + x);//D19E
            str(0x0046 + y, a);//D1A0
            ldam(0x08);//D1A3
            pha();//D1A5
            ldxm(0x06CF);//D1A6
            str(0x08, x);//D1A9
            lda(0x2D);//D1AB
            str(0x16 + x, a);//D1AD
            LabelD1BC();//D1AF
            pla();//D1B2
            str(0x08, a);//D1B3
            tax();//D1B5
            lda(0x00);//D1B6
            str(0x036A, a);//D1B8
        }

        void LabelD071()
        {
            ldx(0x04);//D071
            LabelD073:
            LabelC998();//D073
            dex();//D076
            if (!n)
                goto LabelD073;//D077
            str(0x06CB, a);//D079
            ldxm(0x08);//D07C
            return;
        }

        void LabelD1BC()
        {
            inc(0x036A);//D1BC
            LabelC8D7();//D1BF
            ldam(0x1E + x);//D1C2
            if (!z)
                return;//D1C4
            lda(0x0A);//D1C6
            str(0x049A + x, a);//D1C8
            LabelE243();//D1CB
            LabelD853(); //D1CE
            return;
        }

        void LabelD1D9()
        {
            ldym(0x0367);//D1D9
            inc(0x0367);//D1DC
            ldam(0x0367);//D1DF
            and(0x07);//D1E2
            str(0x0367, a);//D1E4
            ldam(0xD1D1 + y);//D1E7
        }

        void LabelD1EB()
        {
            ldam(0x0747);//D1EB
            if (!z)
                goto LabelD220;//D1EE
            lda(0x40);//D1F0
            ldym(0x06CC);//D1F2
            if (z)
                goto LabelD1F9;//D1F5
            lda(0x60);//D1F7
            LabelD1F9:
            str(0x00, a);//D1F9
            ldam(0x0401 + x);//D1FB
            c = true;//D1FE
            sbc(ram[0x00]);//D1FF
            str(0x0401 + x, a);//D201
            ldam(0x87 + x);//D204
            sbc(0x01);//D206
            str(0x87 + x, a);//D208
            ldam(0x6E + x);//D20A
            sbc(0x00);//D20C
            str(0x6E + x, a);//D20E
            ldym(0x0417 + x);//D210
            ldam(0xCF + x);//D213
            cmp(a, ram[0xC59D + y]);//D215
            if (z)
                goto LabelD220;//D218
            c = false;//D21A
            adc(ram[0x0434 + x]);//D21B
            str(0xCF + x, a);//D21E
            LabelD220:
            LabelF152();//D220
            ldam(0x1E + x);//D223
            if (!z)
                return;//D225
            lda(0x51);//D227
            str(0x00, a);//D229
            ldy(0x02);//D22B
            ldam(0x09);//D22D
            and(0x02);//D22F
            if (z)
                goto LabelD235;//D231
            ldy(0x82);//D233
            LabelD235:
            str(0x01, y);//D235
            ldym(0x06E5 + x);//D237
            ldx(0x00);//D23A
            LabelD23C:
            ldam(0x03B9);//D23C
            str(0x0200 + y, a);//D23F
            ldam(0x00);//D242
            str(0x0201 + y, a);//D244
            inc(0x00);//D247
            ldam(0x01);//D249
            str(0x0202 + y, a);//D24B
            ldam(0x03AE);//D24E
            str(0x0203 + y, a);//D251
            c = false;//D254
            adc(0x08);//D255
            str(0x03AE, a);//D257
            iny();//D25A
            iny();//D25B
            iny();//D25C
            iny();//D25D
            inx();//D25E
            cmp(x, 0x03);//D25F
            if (!c)
                goto LabelD23C;//D261
            ldxm(0x08);//D263
            LabelF1AF();//D265
            ldym(0x06E5 + x);//D268
            ldam(0x03D1);//D26B
            lsr();//D26E
            pha();//D26F
            if (!c)
                goto LabelD277;//D270
            lda(0xF8);//D272
            str(0x020C + y, a);//D274
            LabelD277:
            pla();//D277
            lsr();//D278
            pha();//D279
            if (!c)
                goto LabelD281;//D27A
            lda(0xF8);//D27C
            str(0x0208 + y, a);//D27E
            LabelD281:
            pla();//D281
            lsr();//D282
            pha();//D283
            if (!c)
                goto LabelD28B;//D284
            lda(0xF8);//D286
            str(0x0204 + y, a);//D288
            LabelD28B:
            pla();//D28B
            lsr();//D28C
            if (!c)
                return;//D28D
            lda(0xF8);//D28F
            str(0x0200 + y, a);//D291
        }

        void LabelD295()
        {
            dec(0xA0 + x);//D295
            if (!z)
                goto LabelD2A5;//D297
            lda(0x08);//D299
            str(0xA0 + x, a);//D29B
            inc(0x58 + x);//D29D
            ldam(0x58 + x);//D29F
            cmp(a, 0x03);//D2A1
            if (c)
                goto LabelD2BD;//D2A3
            LabelD2A5:
            LabelF152();//D2A5
            ldam(0x03B9);//D2A8
            str(0x03BA, a);//D2AB
            ldam(0x03AE);//D2AE
            str(0x03AF, a);//D2B1
            ldym(0x06E5 + x);//D2B4
            ldam(0x58 + x);//D2B7
            LabelED17();//D2B9
            return;
            LabelD2BD:
            lda(0x00);//D2BD
            str(0x0F + x, a);//D2BF
            lda(0x08);//D2C1
            str(0xFE, a);//D2C3
            lda(0x05);//D2C5
            str(0x0138, a);//D2C7
            LabelD336(); //D2CA
            return;
        }

        void LabelD2D9()
        {
            lda(0x00);//D2D9
            str(0x06CB, a);//D2DB
            ldam(0x0746);//D2DE
            cmp(a, 0x05);//D2E1
            if (c)
                return;//D2E3

            switch (a)
            {
                case 0:
                    return;
                case 1:
                    LabelD2F2();
                    break;
                case 2:
                    LabelD312();
                    break;
                case 3:
                    LabelD34E();
                    break;
                case 4:
                    LabelD3A2();
                    break;
            }
        }

        void LabelD2F2()
        {
            ldy(0x05);//D2F2
            ldam(0x07FA);//D2F4
            cmp(a, 0x01);//D2F7
            if (z)
                goto LabelD309;//D2F9
            ldy(0x03);//D2FB
            cmp(a, 0x03);//D2FD
            if (z)
                goto LabelD309;//D2FF
            ldy(0x00);//D301
            cmp(a, 0x06);//D303
            if (z)
                goto LabelD309;//D305
            lda(0xFF);//D307
            LabelD309:
            str(0x06D7, a);//D309
            str(0x1E + x, y);//D30C
            inc(0x0746);//D30E
        }

        void LabelD30E()
        {
            inc(0x0746);//D30E
        }

        void LabelD312()
        {
            ldam(0x07F8);//D312
            orm(0x07F9);//D315
            orm(0x07FA);//D318
            if (z)
            {
                LabelD30E();//D31B
                return;
            }
            ldam(0x09);//D31D
            and(0x04);//D31F
            if (z)
                goto LabelD327;//D321
            lda(0x10);//D323
            str(0xFE, a);//D325
            LabelD327:
            ldy(0x23);//D327
            lda(0xFF);//D329
            str(0x0139, a);//D32B
            Label8F5F();//D32E
            lda(0x05);//D331
            str(0x0139, a);//D333
            ldy(0x0B);//D336
            ldam(0x0753);//D338
            if (z)
                goto LabelD33F;//D33B
            ldy(0x11);//D33D
            LabelD33F:
            Label8F5F();//D33F
            ldam(0x0753);//D342
            asl();//D345
            asl();//D346
            asl();//D347
            asl();//D348
            or(0x04);//D349
            LabelBC36(); //D34B
            return;
        }

        void LabelD336()
        {
            ldy(0x0B);//D336
            ldam(0x0753);//D338
            if (z)
                goto LabelD33F;//D33B
            ldy(0x11);//D33D
            LabelD33F:
            Label8F5F();//D33F
            ldam(0x0753);//D342
            asl();//D345
            asl();//D346
            asl();//D347
            asl();//D348
            or(0x04);//D349
            LabelBC36(); //D34B
            return;
        }

        void LabelD34E()
        {
            ldam(0xCF + x);//D34E
            cmp(a, 0x72);//D350
            if (!c)
                goto LabelD359;//D352
            dec(0xCF + x);//D354
            LabelD365(); //D356
            return;
            LabelD359:
            ldam(0x06D7);//D359
            if (z)
                goto LabelD396;//D35C
            if (n) //
                goto LabelD396;//D35E
            lda(0x16);//D360
            str(0x06CB, a);//D362
            LabelF152();//D365
            ldym(0x06E5 + x);//D368
            ldx(0x03);//D36B
            LabelD36D:
            ldam(0x03B9);//D36D
            c = false;//D370
            adc(ram[0xD2CD + x]);//D371
            str(0x0200 + y, a);//D374
            ldam(0xD2D5 + x);//D377
            str(0x0201 + y, a);//D37A
            lda(0x22);//D37D
            str(0x0202 + y, a);//D37F
            ldam(0x03AE);//D382
            c = false;//D385
            adc(ram[0xD2D1 + x]);//D386
            str(0x0203 + y, a);//D389
            iny();//D38C
            iny();//D38D
            iny();//D38E
            iny();//D38F
            dex();//D390
            if (!n)
                goto LabelD36D;//D391
            ldxm(0x08);//D393
            return;
            LabelD396:
            LabelD365();//D396
            lda(0x06);//D399
            str(0x0796 + x, a);//D39B
            inc(0x0746);//D39E
        }

        void LabelD365()
        {
            LabelF152();//D365
            ldym(0x06E5 + x);//D368
            ldx(0x03);//D36B
            LabelD36D:
            ldam(0x03B9);//D36D
            c = false;//D370
            adc(ram[0xD2CD + x]);//D371
            str(0x0200 + y, a);//D374
            ldam(0xD2D5 + x);//D377
            str(0x0201 + y, a);//D37A
            lda(0x22);//D37D
            str(0x0202 + y, a);//D37F
            ldam(0x03AE);//D382
            c = false;//D385
            adc(ram[0xD2D1 + x]);//D386
            str(0x0203 + y, a);//D389
            iny();//D38C
            iny();//D38D
            iny();//D38E
            iny();//D38F
            dex();//D390
            if (!n)
                goto LabelD36D;//D391
            ldxm(0x08);//D393
            return;
        }

        void LabelD39E()
        {
            inc(0x0746);//D39E
        }

        void LabelD3A2()
        {
            LabelD365();//D3A2
            ldam(0x0796 + x);//D3A5
            if (!z)
                return;//D3A8
            ldam(0x07B1);//D3AA
            if (z)
                LabelD39E();//D3AD
        }

        void LabelD3B0()
        {
            ldam(0x1E + x);//D3B0
            if (!z)
                goto LabelD40A;//D3B2
            ldam(0x078A + x);//D3B4
            if (!z)
                goto LabelD40A;//D3B7
            ldam(0xA0 + x);//D3B9
            if (!z)
                goto LabelD3E0;//D3BB
            ldam(0x58 + x);//D3BD
            if (n) //
                goto LabelD3D5;//D3BF
            LabelE143();//D3C1
            if (!n)
                goto LabelD3CF;//D3C4
            ldam(0x00);//D3C6
            eor(0xFF);//D3C8
            c = false;//D3CA
            adc(0x01);//D3CB
            str(0x00, a);//D3CD
            LabelD3CF:
            ldam(0x00);//D3CF
            cmp(a, 0x21);//D3D1
            if (!c)
                goto LabelD40A;//D3D3
            LabelD3D5:
            ldam(0x58 + x);//D3D5
            eor(0xFF);//D3D7
            c = false;//D3D9
            adc(0x01);//D3DA
            str(0x58 + x, a);//D3DC
            inc(0xA0 + x);//D3DE
            LabelD3E0:
            ldam(0x0434 + x);//D3E0
            ldym(0x58 + x);//D3E3
            if (!n)
                goto LabelD3EA;//D3E5
            ldam(0x0417 + x);//D3E7
            LabelD3EA:
            str(0x00, a);//D3EA
            ldam(0x09);//D3EC
            lsr();//D3EE
            if (!c)
                goto LabelD40A;//D3EF
            ldam(0x0747);//D3F1
            if (!z)
                goto LabelD40A;//D3F4
            ldam(0xCF + x);//D3F6
            c = false;//D3F8
            adc(ram[0x58 + x]);//D3F9
            str(0xCF + x, a);//D3FB
            cmp(a, ram[0x00]);//D3FD
            if (!z)
                goto LabelD40A;//D3FF
            lda(0x00);//D401
            str(0xA0 + x, a);//D403
            lda(0x40);//D405
            str(0x078A + x, a);//D407
            LabelD40A:
            lda(0x20);//D40A
            str(0x03C5 + x, a);//D40C
        }

        void LabelD410()
        {
            str(0x07, a);//D410
            ldam(0x34 + x);//D412
            if (!z)
                goto LabelD424;//D414
            ldy(0x18);//D416
            ldam(0x58 + x);//D418
            c = false;//D41A
            adc(ram[0x07]);//D41B
            str(0x58 + x, a);//D41D
            ldam(0xA0 + x);//D41F
            adc(0x00);//D421
            return;
            LabelD424:
            ldy(0x08);//D424
            ldam(0x58 + x);//D426
            c = true;//D428
            sbc(ram[0x07]);//D429
            str(0x58 + x, a);//D42B
            ldam(0xA0 + x);//D42D
            sbc(0x00);//D42F
        }

        void LabelD432()
        {
            ldam(0xB6 + x);//D432
            cmp(a, 0x03);//D434
            if (!z)
                goto LabelD43B;//D436
            LabelC998(); //D438
            return;
            LabelD43B:
            ldam(0x1E + x);//D43B
            if (!n)
                goto LabelD440;//D43D
            return;
            LabelD440:
            tay();//D440
            ldam(0x03A2 + x);//D441
            str(0x00, a);//D444
            ldam(0x46 + x);//D446
            if (z)
                goto LabelD44D;//D448
            LabelD5BB(); //D44A
            return;
            LabelD44D:
            lda(0x2D);//D44D
            cmp(a, ram[0xCF + x]);//D44F
            if (!c)
                goto LabelD462;//D451
            cmp(y, ram[0x00]);//D453
            if (z)
                goto LabelD45F;//D455
            c = false;//D457
            adc(0x02);//D458
            str(0xCF + x, a);//D45A
            LabelD5B1(); //D45C
            return;
            LabelD45F:
            LabelD598(); //D45F
            return;
            LabelD462:
            cmp(a, ram[0x00CF + y]);//D462
            if (!c)
                goto LabelD474;//D465
            cmp(x, ram[0x00]);//D467
            if (z)
                goto LabelD45F;//D469
            c = false;//D46B
            adc(0x02);//D46C
            str(0x00CF + y, a);//D46E
            LabelD5B1(); //D471
            return;
            LabelD474:
            ldam(0xCF + x);//D474
            pha();//D476
            ldam(0x03A2 + x);//D477
            if (!n)
                goto LabelD494;//D47A
            ldam(0x0434 + x);//D47C
            c = false;//D47F
            adc(0x05);//D480
            str(0x00, a);//D482
            ldam(0xA0 + x);//D484
            adc(0x00);//D486
            if (n) //
                goto LabelD4A4;//D488
            if (!z)
                goto LabelD498;//D48A
            ldam(0x00);//D48C
            cmp(a, 0x0B);//D48E
            if (!c)
                goto LabelD49E;//D490
            if (c)
                goto LabelD498;//D492
            LabelD494:
            cmp(a, ram[0x08]);//D494
            if (z)
                goto LabelD4A4;//D496
            LabelD498:
            LabelBFB7();//D498
            goto LabelD4A7; //D49B;
            LabelD49E:
            LabelD5B1();//D49E
            goto LabelD4A7; //D4A1
            LabelD4A4:
            LabelBFB4();//D4A4
            LabelD4A7:
            ldym(0x1E + x);//D4A7
            pla();//D4A9
            c = true;//D4AA
            sbc(ram[0xCF + x]);//D4AB
            LabelD4AD:
            c = false;//D4AD
            adc(ram[0x00CF + y]);//D4AE
            str(0x00CF + y, a);//D4B1
            ldam(0x03A2 + x);//D4B4
            if (n) //
                goto LabelD4BD;//D4B7
            tax();//D4B9
            LabelDC21();//D4BA
            LabelD4BD:
            ldym(0x08);//D4BD
            ldam(0x00A0 + y);//D4BF
            orm(0x0434 + y);//D4C2
            if (z)
                goto LabelD53E;//D4C5
            ldxm(0x0300);//D4C7
            cmp(x, 0x20);//D4CA
            if (c)
                goto LabelD53E;//D4CC
            ldam(0x00A0 + y);//D4CE
            pha();//D4D1
            pha();//D4D2
            LabelD541();//D4D3
            ldam(0x01);//D4D6
            str(0x0301 + x, a);//D4D8
            ldam(0x00);//D4DB
            str(0x0302 + x, a);//D4DD
            lda(0x02);//D4E0
            str(0x0303 + x, a);//D4E2
            ldam(0x00A0 + y);//D4E5
            if (n) //
                goto LabelD4F7;//D4E8
            lda(0xA2);//D4EA
            str(0x0304 + x, a);//D4EC
            lda(0xA3);//D4EF
            str(0x0305 + x, a);//D4F1
            goto LabelD4FF; //D4F4
            LabelD4F7:
            lda(0x24);//D4F7
            str(0x0304 + x, a);//D4F9
            str(0x0305 + x, a);//D4FC
            LabelD4FF:
            ldam(0x001E + y);//D4FF
            tay();//D502
            pla();//D503
            eor(0xFF);//D504
            LabelD541();//D506
            ldam(0x01);//D509
            str(0x0306 + x, a);//D50B
            ldam(0x00);//D50E
            str(0x0307 + x, a);//D510
            lda(0x02);//D513
            str(0x0308 + x, a);//D515
            pla();//D518
            if (!n)
                goto LabelD528;//D519
            lda(0xA2);//D51B
            str(0x0309 + x, a);//D51D
            lda(0xA3);//D520
            str(0x030A + x, a);//D522
            goto LabelD530; //D525
            LabelD528:
            lda(0x24);//D528
            str(0x0309 + x, a);//D52A
            str(0x030A + x, a);//D52D
            LabelD530:
            lda(0x00);//D530
            LabelD532:
            str(0x030B + x, a);//D532
            ldam(0x0300);//D535
            c = false;//D538
            adc(0x0A);//D539
            str(0x0300, a);//D53B
            LabelD53E:
            ldxm(0x08);//D53E
        }

        void LabelD541()
        {
            pha();//D541
            ldam(0x0087 + y);//D542
            c = false;//D545
            adc(0x08);//D546
            ldxm(0x06CC);//D548
            if (!z)
                goto LabelD550;//D54B
            c = false;//D54D
            adc(0x10);//D54E
            LabelD550:
            pha();//D550
            ldam(0x006E + y);//D551
            adc(0x00);//D554
            str(0x02, a);//D556
            pla();//D558
            and(0xF0);//D559
            lsr();//D55B
            lsr();//D55C
            lsr();//D55D
            str(0x00, a);//D55E
            ldxm(0xCF + y);//D560
            pla();//D562
            if (!n)
                goto LabelD56A;//D563
            txa();//D565
            c = false;//D566
            adc(0x08);//D567
            tax();//D569
            LabelD56A:
            txa();//D56A
            ldxm(0x0300);//D56B
            asl();//D56E
            rol();//D56F
            pha();//D570
            rol();//D571
            and(0x03);//D572
            or(0x20);//D574
            str(0x01, a);//D576
            ldam(0x02);//D578
            and(0x01);//D57A
            asl();//D57C
            asl();//D57D
            orm(0x01);//D57E
            str(0x01, a);//D580
            pla();//D582
            and(0xE0);//D583
            c = false;//D585
            adc(ram[0x00]);//D586
            str(0x00, a);//D588
            ldam(0x00CF + y);//D58A
            cmp(a, 0xE8);//D58D
            if (!c)
                return;//D58F
            ldam(0x00);//D591
            and(0xBF);//D593
            str(0x00, a);//D595
            return;
        }

        void LabelD598()
        {
            tya();//D598
            tax();//D599
            LabelF1AF();//D59A
            lda(0x06);//D59D
            LabelDA11();//D59F
            ldam(0x03AD);//D5A2
            str(0x0117 + x, a);//D5A5
            ldam(0xCE);//D5A8
            str(0x011E + x, a);//D5AA
            lda(0x01);//D5AD
            str(0x46 + x, a);//D5AF
            LabelD5B1();
        }

        void LabelD5B1()
        {
            LabelC363();//D5B1
            str(0x00A0 + y, a);//D5B4
            str(0x0434 + y, a);//D5B7
        }

        void LabelD5BB()
        {
            tya();//D5BB
            pha();//D5BC
            LabelBF6B();//D5BD
            pla();//D5C0
            tax();//D5C1
            LabelBF6B();//D5C2
            ldxm(0x08);//D5C5
            ldam(0x03A2 + x);//D5C7
            if (n) //
                goto LabelD5D0;//D5CA
            tax();//D5CC
            LabelDC21();//D5CD
            LabelD5D0:
            ldxm(0x08);//D5D0
        }

        void LabelD5D3()
        {
            ldam(0xA0 + x);//D5D3
            orm(0x0434 + x);//D5D5
            if (!z)
                goto LabelD5EF;//D5D8
            str(0x0417 + x, a);//D5DA
            ldam(0xCF + x);//D5DD
            cmp(a, ram[0x0401 + x]);//D5DF
            if (c)
                goto LabelD5EF;//D5E2
            ldam(0x09);//D5E4
            and(0x07);//D5E6
            if (!z)
                goto LabelD5EC;//D5E8
            inc(0xCF + x);//D5EA
            LabelD5EC:
            LabelD5FE(); //D5EC
            return;
            LabelD5EF:
            ldam(0xCF + x);//D5EF
            cmp(a, ram[0x58 + x]);//D5F1
            if (!c)
                goto LabelD5FB;//D5F3
            LabelBFB7();//D5F5
            LabelD5FE(); //D5F8
            return;
            LabelD5FB:
            LabelBFB4();//D5FB
            LabelD5FE:
            ldam(0x03A2 + x);//D5FE
            if (n) //
                return;//D601
            LabelDC21();//D603
        }

        void LabelD5FE()
        {
            ldam(0x03A2 + x);//D5FE
            if (n) //
                return;//D601
            LabelDC21();//D603
        }

        void LabelD607()
        {
            lda(0x0E);//D607
            LabelCB47();//D609
            LabelCB66();//D60C
            ldam(0x03A2 + x);//D60F
            if (n) //
                return;//D612
            ldam(0x86);//D614
            c = false;//D616
            adc(ram[0x00]);//D617
            str(0x86, a);//D619
            ldam(0x6D);//D61B
            ldym(0x00);//D61D
            if (n) //
                goto LabelD626;//D61F
            adc(0x00);//D621
            goto LabelD628; //D623
            LabelD626:
            sbc(0x00);//D626
            LabelD628:
            str(0x6D, a);//D628
            str(0x03A1, y);//D62A
            LabelDC21();//D62D
        }

        void LabelD614()
        {
            ldam(0x86);//D614
            c = false;//D616
            adc(ram[0x00]);//D617
            str(0x86, a);//D619
            ldam(0x6D);//D61B
            ldym(0x00);//D61D
            if (n) //
                goto LabelD626;//D61F
            adc(0x00);//D621
            goto LabelD628; //D623
            LabelD626:
            sbc(0x00);//D626
            LabelD628:
            str(0x6D, a);//D628
            str(0x03A1, y);//D62A
            LabelDC21();//D62D
        }

        void LabelD631()
        {
            ldam(0x03A2 + x);//D631
            if (n) //
                return;//D634
            LabelBF88();//D636
            LabelDC21();//D639
        }

        void LabelD63D()
        {
            LabelBF02();//D63D
            str(0x00, a);//D640
            ldam(0x03A2 + x);//D642
            if (n) //
                return;//D645
            lda(0x10);//D647
            str(0x58 + x, a);//D649
            LabelD614();//D64B
        }

        void LabelD64F()
        {
            LabelD65B();//D64F
            LabelD5FE(); //D652
            return;
        }

        void LabelD655()
        {
            LabelD65B();//D655
            LabelD671(); //D658
            return;
        }

        void LabelD65B()
        {
            ldam(0x0747);//D65B
            if (!z)
                return;//D65E
            ldam(0x0417 + x);//D660
            c = false;//D663
            adc(ram[0x0434 + x]);//D664
            str(0x0417 + x, a);//D667
            ldam(0xCF + x);//D66A
            adc(ram[0xA0 + x]);//D66C
            str(0xCF + x, a);//D66E
        }

        void LabelD671()
        {
            ldam(0x03A2 + x);//D671
            if (z)
                return;//D674
            LabelDC19();//D676
        }

        void LabelD67A()
        {
            ldam(0x16 + x);//D67A
            cmp(a, 0x14);//D67C
            if (z)
                return;//D67E
            ldam(0x071C);//D680
            ldym(0x16 + x);//D683
            cmp(y, 0x05);//D685
            if (z)
                goto LabelD68D;//D687
            cmp(y, 0x0D);//D689
            if (!z)
                goto LabelD68F;//D68B
            LabelD68D:
            adc(0x38);//D68D
            LabelD68F:
            sbc(0x48);//D68F
            str(0x01, a);//D691
            ldam(0x071A);//D693
            sbc(0x00);//D696
            str(0x00, a);//D698
            ldam(0x071D);//D69A
            adc(0x48);//D69D
            str(0x03, a);//D69F
            ldam(0x071B);//D6A1
            adc(0x00);//D6A4
            str(0x02, a);//D6A6
            ldam(0x87 + x);//D6A8
            cmp(a, ram[0x01]);//D6AA
            ldam(0x6E + x);//D6AC
            sbc(ram[0x00]);//D6AE
            if (n) //
                goto LabelD6D2;//D6B0
            ldam(0x87 + x);//D6B2
            cmp(a, ram[0x03]);//D6B4
            ldam(0x6E + x);//D6B6
            sbc(ram[0x02]);//D6B8
            if (n) //
                return;//D6BA
            ldam(0x1E + x);//D6BC
            cmp(a, 0x05);//D6BE
            if (z)
                return;//D6C0
            cmp(y, 0x0D);//D6C2
            if (z)
                return;//D6C4
            cmp(y, 0x30);//D6C6
            if (z)
                return;//D6C8
            cmp(y, 0x31);//D6CA
            if (z)
                return;//D6CC
            cmp(y, 0x32);//D6CE
            if (z)
                return;//D6D0
            LabelD6D2:
            LabelC998();//D6D2
        }

        void LabelD6D9()
        {
            ldam(0x24 + x);//D6D9
            if (z)
                goto LabelD733;//D6DB
            asl();//D6DD
            if (c)
                goto LabelD733;//D6DE
            ldam(0x09);//D6E0
            lsr();//D6E2
            if (c)
                goto LabelD733;//D6E3
            txa();//D6E5
            asl();//D6E6
            asl();//D6E7
            c = false;//D6E8
            adc(0x1C);//D6E9
            tay();//D6EB
            ldx(0x04);//D6EC
            LabelD6EE:
            str(0x01, x);//D6EE
            tya();//D6F0
            pha();//D6F1
            ldam(0x1E + x);//D6F2
            and(0x20);//D6F4
            if (!z)
                goto LabelD72C;//D6F6
            ldam(0x0F + x);//D6F8
            if (z)
                goto LabelD72C;//D6FA
            ldam(0x16 + x);//D6FC
            cmp(a, 0x24);//D6FE
            if (!c)
                goto LabelD706;//D700
            cmp(a, 0x2B);//D702
            if (!c)
                goto LabelD72C;//D704
            LabelD706:
            cmp(a, 0x06);//D706
            if (!z)
                goto LabelD710;//D708
            ldam(0x1E + x);//D70A
            cmp(a, 0x02);//D70C
            if (c)
                goto LabelD72C;//D70E
            LabelD710:
            ldam(0x03D8 + x);//D710
            if (!z)
                goto LabelD72C;//D713
            txa();//D715
            asl();//D716
            asl();//D717
            c = false;//D718
            adc(0x04);//D719
            tax();//D71B
            LabelE327();//D71C
            ldxm(0x08);//D71F
            if (!c)
                goto LabelD72C;//D721
            lda(0x80);//D723
            str(0x24 + x, a);//D725
            ldxm(0x01);//D727
            LabelD73E();//D729
            LabelD72C:
            pla();//D72C
            tay();//D72D
            ldxm(0x01);//D72E
            dex();//D730
            if (!n)
                goto LabelD6EE;//D731
            LabelD733:
            ldxm(0x08);//D733
        }

        void LabelD73E()
        {
            LabelF152();//D73E
            ldxm(0x01);//D741
            ldam(0x0F + x);//D743
            if (!n)
                goto LabelD752;//D745
            and(0x0F);//D747
            tax();//D749
            ldam(0x16 + x);//D74A
            cmp(a, 0x2D);//D74C
            if (z)
                goto LabelD75C;//D74E
            ldxm(0x01);//D750
            LabelD752:
            ldam(0x16 + x);//D752
            cmp(a, 0x02);//D754
            if (z)
                return;//D756
            cmp(a, 0x2D);//D758
            if (!z)
                goto LabelD789;//D75A
            LabelD75C:
            dec(0x0483);//D75C
            if (!z)
                return;//D75F
            LabelC363();//D761
            str(0x58 + x, a);//D764
            str(0x06CB, a);//D766
            lda(0xFE);//D769
            str(0xA0 + x, a);//D76B
            ldym(0x075F);//D76D
            ldam(0xD736 + y);//D770
            str(0x16 + x, a);//D773
            lda(0x20);//D775
            cmp(y, 0x03);//D777
            if (c)
                goto LabelD77D;//D779
            or(0x03);//D77B
            LabelD77D:
            str(0x1E + x, a);//D77D
            lda(0x80);//D77F
            str(0xFE, a);//D781
            ldxm(0x01);//D783
            lda(0x09);//D785
            if (!z)
                goto LabelD7BC;//D787
            LabelD789:
            cmp(a, 0x08);//D789
            if (z)
                return;//D78B
            cmp(a, 0x0C);//D78D
            if (z)
                return;//D78F
            cmp(a, 0x15);//D791
            if (c)
                return;//D793
            ldam(0x16 + x);//D795
            cmp(a, 0x0D);//D797
            if (!z)
                goto LabelD7A1;//D799
            ldam(0xCF + x);//D79B
            adc(0x18);//D79D
            str(0xCF + x, a);//D79F
            LabelD7A1:
            LabelE01B();//D7A1
            ldam(0x1E + x);//D7A4
            and(0x1F);//D7A6
            or(0x20);//D7A8
            str(0x1E + x, a);//D7AA
            lda(0x02);//D7AC
            ldym(0x16 + x);//D7AE
            cmp(y, 0x05);//D7B0
            if (!z)
                goto LabelD7B6;//D7B2
            lda(0x06);//D7B4
            LabelD7B6:
            cmp(y, 0x06);//D7B6
            if (!z)
                goto LabelD7BC;//D7B8
            lda(0x01);//D7BA
            LabelD7BC:
            LabelDA11();//D7BC
            lda(0x08);//D7BF
            str(0xFF, a);//D7C1
        }

        void LabelD795()
        {
            ldam(0x16 + x);//D795
            cmp(a, 0x0D);//D797
            if (!z)
                goto LabelD7A1;//D799
            ldam(0xCF + x);//D79B
            adc(0x18);//D79D
            str(0xCF + x, a);//D79F
            LabelD7A1:
            LabelE01B();//D7A1
            ldam(0x1E + x);//D7A4
            and(0x1F);//D7A6
            or(0x20);//D7A8
            str(0x1E + x, a);//D7AA
            lda(0x02);//D7AC
            ldym(0x16 + x);//D7AE
            cmp(y, 0x05);//D7B0
            if (!z)
                goto LabelD7B6;//D7B2
            lda(0x06);//D7B4
            LabelD7B6:
            cmp(y, 0x06);//D7B6
            if (!z)
                goto LabelD7BC;//D7B8
            lda(0x01);//D7BA
            LabelD7BC:
            LabelDA11();//D7BC
            lda(0x08);//D7BF
            str(0xFF, a);//D7C1
        }

        void LabelD7C4()
        {
            ldam(0x09);//D7C4
            lsr();//D7C6
            if (!c)
                return;//D7C7
            ldam(0x0747);//D7C9
            orm(0x03D6);//D7CC
            if (!z)
                return;//D7CF
            txa();//D7D1
            asl();//D7D2
            asl();//D7D3
            c = false;//D7D4
            adc(0x24);//D7D5
            tay();//D7D7
            LabelE325();//D7D8
            ldxm(0x08);//D7DB
            if (!c)
                goto LabelD7FA;//D7DD
            ldam(0x06BE + x);//D7DF
            if (!z)
                return;//D7E2
            lda(0x01);//D7E4
            str(0x06BE + x, a);//D7E6
            ldam(0x64 + x);//D7E9
            eor(0xFF);//D7EB
            c = false;//D7ED
            adc(0x01);//D7EE
            str(0x64 + x, a);//D7F0
            ldam(0x079F);//D7F2
            if (!z)
                return;//D7F5
            LabelD92C(); //D7F7
            return;
            LabelD7FA:
            lda(0x00);//D7FA
            str(0x06BE + x, a);//D7FC
        }

        void LabelD800()
        {
            LabelC998();//D800
            lda(0x06);//D803
            LabelDA11();//D805
            lda(0x20);//D808
            str(0xFE, a);//D80A
            ldam(0x39);//D80C
            cmp(a, 0x02);//D80E
            if (!c)
                goto LabelD820;//D810
            cmp(a, 0x03);//D812
            if (z)
                goto LabelD83A;//D814
            lda(0x23);//D816
            str(0x079F, a);//D818
            lda(0x40);//D81B
            str(0xFB, a);//D81D
            LabelD820:
            ldam(0x0756);//D820
            if (z)
                goto LabelD840;//D823
            cmp(a, 0x01);//D825
            if (!z)
                return;//D827
            ldxm(0x08);//D829
            lda(0x02);//D82B
            str(0x0756, a);//D82D
            Label85F1();//D830
            ldxm(0x08);//D833
            lda(0x0C);//D835
            goto LabelD847; //D837
            LabelD83A:
            lda(0x0B);//D83A
            str(0x0110 + x, a);//D83C
            return;
            LabelD840:
            lda(0x01);//D840
            str(0x0756, a);//D842
            lda(0x09);//D845
            LabelD847:
            ldy(0x00);//D847
            LabelD948();//D849
        }

        void LabelD853()
        {
            ldam(0x09);//D853
            lsr();//D855
            if (c)
                return;//D856
            LabelDC41();//D858
            if (c)
                return;//D85B
            ldam(0x03D8 + x);//D85D
            if (!z)
                return;//D860
            ldam(0x0E);//D862
            cmp(a, 0x08);//D864
            if (!z)
                return;//D866
            ldam(0x1E + x);//D868
            and(0x20);//D86A
            if (!z)
                return;//D86C
            LabelDC52();//D86E
            LabelE325();//D871
            ldxm(0x08);//D874
            if (c)
                goto LabelD881;//D876
            ldam(0x0491 + x);//D878
            and(0xFE);//D87B
            str(0x0491 + x, a);//D87D
            return;
            LabelD881:
            ldym(0x16 + x);//D881
            cmp(y, 0x2E);//D883
            if (!z)
                goto LabelD88A;//D885
            LabelD800(); //D887
            return;
            LabelD88A:
            ldam(0x079F);//D88A
            if (z)
                goto LabelD895;//D88D
            LabelD795(); //D88F
            return;
            LabelD895:
            ldam(0x0491 + x);//D895
            and(0x01);//D898
            orm(0x03D8 + x);//D89A
            if (!z)
                return;//D89D
            lda(0x01);//D89F
            orm(0x0491 + x);//D8A1
            str(0x0491 + x, a);//D8A4
            cmp(y, 0x12);//D8A7
            if (z)
                goto LabelD8F9;//D8A9
            cmp(y, 0x0D);//D8AB
            if (z)
            {
                LabelD92C();//D8AD
                return;
            }
            cmp(y, 0x0C);//D8AF
            if (z)
            {
                LabelD92C();//D8B1
                return;
            }
            cmp(y, 0x33);//D8B3
            if (z)
                goto LabelD8F9;//D8B5
            cmp(y, 0x15);//D8B7
            if (c)
            {
                LabelD92C();//D8B9
                return;
            }
            ldam(0x074E);//D8BB
            if (z)
            {
                LabelD92C();//D8BE
                return;
            }
            ldam(0x1E + x);//D8C0
            asl();//D8C2
            if (c)
                goto LabelD8F9;//D8C3
            ldam(0x1E + x);//D8C5
            and(0x07);//D8C7
            cmp(a, 0x02);//D8C9
            if (!c)
                goto LabelD8F9;//D8CB
            ldam(0x16 + x);//D8CD
            cmp(a, 0x06);//D8CF
            if (z)
                return;//D8D1
            lda(0x08);//D8D3
            str(0xFF, a);//D8D5
            ldam(0x1E + x);//D8D7
            or(0x80);//D8D9
            str(0x1E + x, a);//D8DB
            LabelDA05();//D8DD
            ldam(0xD84F + y);//D8E0
            str(0x58 + x, a);//D8E3
            lda(0x03);//D8E5
            c = false;//D8E7
            adc(ram[0x0484]);//D8E8
            ldym(0x0796 + x);//D8EB
            cmp(y, 0x03);//D8EE
            if (c)
                goto LabelD8F5;//D8F0
            ldam(0xD892 + y);//D8F2
            LabelD8F5:
            LabelDA11();//D8F5
            return;
            LabelD8F9:
            ldam(0x9F);//D8F9
            if (n) //
                goto LabelD8FF;//D8FB
            if (!z)
            {
                LabelD969();//D8FD
                return;
            }
            LabelD8FF:
            ldam(0x16 + x);//D8FF
            cmp(a, 0x07);//D901
            if (!c)
                goto LabelD90E;//D903
            ldam(0xCE);//D905
            c = false;//D907
            adc(0x0C);//D908
            cmp(a, ram[0xCF + x]);//D90A
            if (!c)
            {
                LabelD969();//D90C
                return;
            }
            LabelD90E:
            ldam(0x0791);//D90E
            if (!z)
            {
                LabelD969();//D911
                return;
            }
            ldam(0x079E);//D913
            if (!z)
            {
                LabelD955();//D916
                return;
            }
            ldam(0x03AD);//D918
            cmp(a, ram[0x03AE]);//D91B
            if (!c)
                goto LabelD923;//D91E
            LabelD9F6(); //D920
            return;
            LabelD923:
            ldam(0x46 + x);//D923
            cmp(a, 0x01);//D925
            if (!z)
            {
                LabelD92C();//D927
                return;
            }
            LabelD9FF(); //D929
            return;
        }

        void LabelD92C()
        {
            ldam(0x079E);//D92C
            if (!z)
            {
                LabelD955();//D92F
                return;
            }
            LabelD931();
        }

        void LabelD931()
        {
            ldxm(0x0756);//D931
            if (z)
            {
                LabelD958();
                return;//D934
            }
            str(0x0756, a);//D936
            lda(0x08);//D939
            str(0x079E, a);//D93B
            asl();//D93E
            str(0xFF, a);//D93F
            Label85F1();//D941
            lda(0x0A);//D944
            LabelD946();
        }

        void LabelD946()
        {
            ldy(0x01);//D946
            LabelD948();
        }

        void LabelD948()
        {
            str(0x0E, a);//D948
            str(0x1D, y);//D94A
            ldy(0xFF);//D94C
            str(0x0747, y);//D94E
            iny();//D951
            str(0x0775, y);//D952
            LabelD955();
        }

        void LabelD955()
        {
            ldxm(0x08);//D955
        }

        void LabelD958()
        {
            str(0x57, x);//D958
            inx();//D95A
            str(0xFC, x);//D95B
            lda(0xFC);//D95D
            str(0x9F, a);//D95F
            lda(0x0B);//D961
            if (!z)
                LabelD946();//D963
        }

        void LabelD969()
        {
            ldam(0x16 + x);//D969
            cmp(a, 0x12);//D96B
            if (z)
            {
                LabelD92C();//D96D
                return;
            }
            lda(0x04);//D96F
            str(0xFF, a);//D971
            ldam(0x16 + x);//D973
            ldy(0x00);//D975
            cmp(a, 0x14);//D977
            if (z)
                goto LabelD996;//D979
            cmp(a, 0x08);//D97B
            if (z)
                goto LabelD996;//D97D
            cmp(a, 0x33);//D97F
            if (z)
                goto LabelD996;//D981
            cmp(a, 0x0C);//D983
            if (z)
                goto LabelD996;//D985
            iny();//D987
            cmp(a, 0x05);//D988
            if (z)
                goto LabelD996;//D98A
            iny();//D98C
            cmp(a, 0x11);//D98D
            if (z)
                goto LabelD996;//D98F
            iny();//D991
            cmp(a, 0x07);//D992
            if (!z)
            {
                LabelD9B3();
                return;//D994
            }
            LabelD996:
            ldam(0xD965 + y);//D996
            LabelDA11();//D999
            ldam(0x46 + x);//D99C
            pha();//D99E
            LabelE02F();//D99F
            pla();//D9A2
            str(0x46 + x, a);//D9A3
            lda(0x20);//D9A5
            str(0x1E + x, a);//D9A7
            LabelC363();//D9A9
            str(0x58 + x, a);//D9AC
            lda(0xFD);//D9AE
            str(0x9F, a);//D9B0
        }

        void LabelD9B3()
        {
            cmp(a, 0x09);//D9B3
            if (!c)
            {
                LabelD9D4();//D9B5
                return;
            }
            and(0x01);//D9B7
            str(0x16 + x, a);//D9B9
            ldy(0x00);//D9BB
            str(0x1E + x, y);//D9BD
            lda(0x03);//D9BF
            LabelDA11();//D9C1
            LabelC363();//D9C4
            LabelDA05();//D9C7
            ldam(0xD851 + y);//D9CA
            str(0x58 + x, a);//D9CD
            LabelD9F1(); //D9CF
            return;
        }

        void LabelD9D4()
        {
            lda(0x04);//D9D4
            str(0x1E + x, a);//D9D6
            inc(0x0484);//D9D8
            ldam(0x0484);//D9DB
            c = false;//D9DE
            adc(ram[0x0791]);//D9DF
            LabelDA11();//D9E2
            inc(0x0791);//D9E5
            ldym(0x076A);//D9E8
            ldam(0xD9D2 + y);//D9EB
            str(0x0796 + x, a);//D9EE
            lda(0xFC);//D9F1
            str(0x9F, a);//D9F3
        }

        void LabelD9F1()
        {
            lda(0xFC);//D9F1
            str(0x9F, a);//D9F3
        }

        void LabelD9F6()
        {
            ldam(0x46 + x);//D9F6
            cmp(a, 0x01);//D9F8
            if (!z)
                goto LabelD9FF;//D9FA
            LabelD92C(); //D9FC
            return;
            LabelD9FF:
            LabelDB1C();//D9FF
            LabelD92C(); //DA02
            return;
        }

        void LabelD9FF()
        {
            LabelDB1C();//D9FF
            LabelD92C(); //DA02
        }

        void LabelDA05()
        {
            ldy(0x01);//DA05
            LabelE143();//DA07
            if (!n)
                goto LabelDA0D;//DA0A
            iny();//DA0C
            LabelDA0D:
            str(0x46 + x, y);//DA0D
            dey();//DA0F
        }

        void LabelDA11()
        {
            str(0x0110 + x, a);//DA11
            lda(0x30);//DA14
            str(0x012C + x, a);//DA16
            ldam(0xCF + x);//DA19
            str(0x011E + x, a);//DA1B
            ldam(0x03AE);//DA1E
            str(0x0117 + x, a);//DA21
        }

        void LabelDA33()
        {
            ldam(0x09);//DA33
            lsr();//DA35
            if (!c)
                return;//DA36
            ldam(0x074E);//DA38
            if (z)
                return;//DA3B
            ldam(0x16 + x);//DA3D
            cmp(a, 0x15);//DA3F
            if (c)
                goto LabelDAB1;//DA41
            cmp(a, 0x11);//DA43
            if (z)
                goto LabelDAB1;//DA45
            cmp(a, 0x0D);//DA47
            if (z)
                goto LabelDAB1;//DA49
            ldam(0x03D8 + x);//DA4B
            if (!z)
                goto LabelDAB1;//DA4E
            LabelDC52();//DA50
            dex();//DA53
            if (n) //
                goto LabelDAB1;//DA54
            LabelDA56:
            str(0x01, x);//DA56
            tya();//DA58
            pha();//DA59
            ldam(0x0F + x);//DA5A
            if (z)
                goto LabelDAAA;//DA5C
            ldam(0x16 + x);//DA5E
            cmp(a, 0x15);//DA60
            if (c)
                goto LabelDAAA;//DA62
            cmp(a, 0x11);//DA64
            if (z)
                goto LabelDAAA;//DA66
            cmp(a, 0x0D);//DA68
            if (z)
                goto LabelDAAA;//DA6A
            ldam(0x03D8 + x);//DA6C
            if (!z)
                goto LabelDAAA;//DA6F
            txa();//DA71
            asl();//DA72
            asl();//DA73
            c = false;//DA74
            adc(0x04);//DA75
            tax();//DA77
            LabelE327();//DA78
            ldxm(0x08);//DA7B
            ldym(0x01);//DA7D
            if (!c)
                goto LabelDAA1;//DA7F
            ldam(0x1E + x);//DA81
            orm(0x001E + y);//DA83
            and(0x80);//DA86
            if (!z)
                goto LabelDA9B;//DA88
            ldam(0x0491 + y);//DA8A
            and(ram[0xDA25 + x]);//DA8D
            if (!z)
                goto LabelDAAA;//DA90
            ldam(0x0491 + y);//DA92
            orm(0xDA25 + x);//DA95
            str(0x0491 + y, a);//DA98
            LabelDA9B:
            LabelDAB4();//DA9B
            goto LabelDAAA; //DA9E
            LabelDAA1:
            ldam(0x0491 + y);//DAA1
            and(ram[0xDA2C + x]);//DAA4
            str(0x0491 + y, a);//DAA7
            LabelDAAA:
            pla();//DAAA
            tay();//DAAB
            ldxm(0x01);//DAAC
            dex();//DAAE
            if (!n)
                goto LabelDA56;//DAAF
            LabelDAB1:
            ldxm(0x08);//DAB1
        }

        void LabelDAB4()
        {
            ldam(0x001E + y);//DAB4
            orm(0x1E + x);//DAB7
            and(0x20);//DAB9
            if (!z)
                return;//DABB
            ldam(0x1E + x);//DABD
            cmp(a, 0x06);//DABF
            if (!c)
                goto LabelDAF1;//DAC1
            ldam(0x16 + x);//DAC3
            cmp(a, 0x05);//DAC5
            if (z)
                return;//DAC7
            ldam(0x001E + y);//DAC9
            asl();//DACC
            if (!c)
                goto LabelDAD9;//DACD
            lda(0x06);//DACF
            LabelDA11();//DAD1
            LabelD795();//DAD4
            ldym(0x01);//DAD7
            LabelDAD9:
            tya();//DAD9
            tax();//DADA
            LabelD795();//DADB
            ldxm(0x08);//DADE
            ldam(0x0125 + x);//DAE0
            c = false;//DAE3
            adc(0x04);//DAE4
            ldxm(0x01);//DAE6
            LabelDA11();//DAE8
            ldxm(0x08);//DAEB
            inc(0x0125 + x);//DAED
            return;
            LabelDAF1:
            ldam(0x001E + y);//DAF1
            cmp(a, 0x06);//DAF4
            if (!c)
                goto LabelDB15;//DAF6
            ldam(0x0016 + y);//DAF8
            cmp(a, 0x05);//DAFB
            if (z)
                return;//DAFD
            LabelD795();//DAFF
            ldym(0x01);//DB02
            ldam(0x0125 + y);//DB04
            c = false;//DB07
            adc(0x04);//DB08
            ldxm(0x08);//DB0A
            LabelDA11();//DB0C
            ldxm(0x01);//DB0F
            inc(0x0125 + x);//DB11
            return;
            LabelDB15:
            tya();//DB15
            tax();//DB16
            LabelDB1C();//DB17
            ldxm(0x08);//DB1A
            LabelDB1C();
        }

        void LabelDB1C()
        {
            ldam(0x16 + x);//DB1C
            cmp(a, 0x0D);//DB1E
            if (z)
                return;//DB20
            cmp(a, 0x11);//DB22
            if (z)
                return;//DB24
            cmp(a, 0x05);//DB26
            if (z)
                return;//DB28
            cmp(a, 0x12);//DB2A
            if (z)
            {
                LabelDB36();//DB2C
                return;
            }
            cmp(a, 0x0E);//DB2E
            if (z)
            {
                LabelDB36();//DB30
                return;
            }
            cmp(a, 0x07);//DB32
            if (c)
                return;//DB34
            LabelDB36();
        }

        void LabelDB36()
        {
            ldam(0x58 + x);//DB36
            eor(0xFF);//DB38
            tay();//DB3A
            iny();//DB3B
            str(0x58 + x, y);//DB3C
            ldam(0x46 + x);//DB3E
            eor(0x03);//DB40
            str(0x46 + x, a);//DB42
        }

        void LabelDB45()
        {
            lda(0xFF);//DB45
            str(0x03A2 + x, a);//DB47
            ldam(0x0747);//DB4A
            if (!z)
            {
                LabelDB78();//DB4D
                return;
            }
            ldam(0x1E + x);//DB4F
            if (n) //
            {
                LabelDB78();//DB51
                return;
            }
            ldam(0x16 + x);//DB53
            cmp(a, 0x24);//DB55
            if (!z)
            {
                LabelDB5F();//DB57
                return;
            }
            ldam(0x1E + x);//DB59
            tax();//DB5B
            LabelDB5F();//DB5C
            LabelDB5F();
        }

        void LabelDB5F()
        {
            LabelDC41();//DB5F
            if (c)
            {
                LabelDB78();//DB62
                return;
            }
            txa();//DB64
            LabelDC54();//DB65
            ldam(0xCF + x);//DB68
            str(0x00, a);//DB6A
            txa();//DB6C
            pha();//DB6D
            LabelE325();//DB6E
            pla();//DB71
            tax();//DB72
            if (!c)
            {
                LabelDB78();//DB73
                return;
            }
            LabelDBBC();//DB75
            LabelDB78();
        }

        void LabelDB78()
        {
            ldxm(0x08);//DB78
        }

        void LabelDB7B()
        {
            ldam(0x0747);//DB7B
            if (!z)
            {
                LabelDBB7();//DB7E
                return;
            }
            str(0x03A2 + x, a);//DB80
            LabelDC41();//DB83
            if (c)
            {
                LabelDBB7();//DB86
                return;
            }
            lda(0x02);//DB88
            str(0x00, a);//DB8A
            LabelDB8C();
        }

        void LabelDB8C()
        {
            ldxm(0x08);//DB8C
            LabelDC52();//DB8E
            and(0x02);//DB91
            if (!z)
            {
                LabelDBB7();//DB93
                return;
            }
            ldam(0x04AD + y);//DB95
            cmp(a, 0x20);//DB98
            if (!c)
            {
                LabelDBA1();//DB9A
                return;
            }
            LabelE325();//DB9C
            if (c)
            {
                LabelDBBA();//DB9F
                return;
            }
            LabelDBA1();
        }

        void LabelDBA1()
        {
            ldam(0x04AD + y);//DBA1
            c = false;//DBA4
            adc(0x80);//DBA5
            str(0x04AD + y, a);//DBA7
            ldam(0x04AF + y);//DBAA
            c = false;//DBAD
            adc(0x80);//DBAE
            str(0x04AF + y, a);//DBB0
            dec(0x00);//DBB3
            if (!z)
                LabelDB8C();//DBB5
            LabelDBB7();
        }

        void LabelDBB7()
        {
            ldxm(0x08);//DBB7
        }

        void LabelDBBA()
        {
            ldxm(0x08);//DBBA
            LabelDBBC();
        }

        void LabelDBBC()
        {
            ldam(0x04AF + y);//DBBC
            c = true;//DBBF
            sbc(ram[0x04AD]);//DBC0
            cmp(a, 0x04);//DBC3
            if (c)
                goto LabelDBCF;//DBC5
            ldam(0x9F);//DBC7
            if (!n)
                goto LabelDBCF;//DBC9
            lda(0x01);//DBCB
            str(0x9F, a);//DBCD
            LabelDBCF:
            ldam(0x04AF);//DBCF
            c = true;//DBD2
            sbc(ram[0x04AD + y]);//DBD3
            cmp(a, 0x06);//DBD6
            if (c)
                return;//DBD8
            ldam(0x9F);//DBDA
            if (n) //
                return;//DBDC
            ldam(0x00);//DBDE
            ldym(0x16 + x);//DBE0
            cmp(y, 0x2B);//DBE2
            if (z)
                goto LabelDBEB;//DBE4
            cmp(y, 0x2C);//DBE6
            if (z)
                goto LabelDBEB;//DBE8
            txa();//DBEA
            LabelDBEB:
            ldxm(0x08);//DBEB
            str(0x03A2 + x, a);//DBED
            lda(0x00);//DBF0
            str(0x1D, a);//DBF2
        }

        void LabelDBF5()
        {
            lda(0x01);//DBF5
            str(0x00, a);//DBF7
            ldam(0x04AE);//DBF9
            c = true;//DBFC
            sbc(ram[0x04AC + y]);//DBFD
            cmp(a, 0x08);//DC00
            if (!c)
                goto LabelDC11;//DC02
            inc(0x00);//DC04
            ldam(0x04AE + y);//DC06
            c = false;//DC09
            sbc(ram[0x04AC]);//DC0A
            cmp(a, 0x09);//DC0D
            if (c)
                goto LabelDC14;//DC0F
            LabelDC11:
            LabelDF4B();//DC11
            LabelDC14:
            ldxm(0x08);//DC14
        }

        void LabelDC19()
        {
            tay();//DC19
            ldam(0xCF + x);//DC1A 
            c = false;//DC1C
            adc(ram[0xDC16 + y]);//DC1D
            LabelDC23();
        }

        void LabelDC21()
        {
            ldam(0xCF + x);//DC21
            LabelDC23();
        }

        void LabelDC23()
        {
            ldym(0x0E);//DC23
            cmp(y, 0x0B);//DC25
            if (z)
                return;//DC27
            ldym(0xB6 + x);//DC29
            cmp(y, 0x01);//DC2B
            if (!z)
                return;//DC2D
            c = true;//DC2F
            sbc(0x20);//DC30
            str(0xCE, a);//DC32
            tya();//DC34
            sbc(0x00);//DC35
            str(0xB5, a);//DC37
            lda(0x00);//DC39
            str(0x9F, a);//DC3B
            str(0x0433, a);//DC3D
        }

        void LabelDC41()
        {
            ldam(0x03D0);//DC41
            cmp(a, 0xF0);//DC44
            if (c)
                return;//DC46
            ldym(0xB5);//DC48
            dey();//DC4A
            if (!z)
                return;//DC4B
            ldam(0xCE);//DC4D
            cmp(a, 0xD0);//DC4F
        }

        void LabelDC52()
        {
            ldam(0x08);//DC52
            LabelDC54();
        }

        void LabelDC54()
        {
            asl();//DC54
            asl();//DC55
            c = false;//DC56
            adc(0x04);//DC57
            tay();//DC59
            ldam(0x03D1);//DC5A
            and(0x0F);//DC5D
            cmp(a, 0x0F);//DC5F
        }

        void LabelDC64()
        {
            ldam(0x0716);//DC64
            if (!z)
                return;//DC67
            ldam(0x0E);//DC69
            cmp(a, 0x0B);//DC6B
            if (z)
                return;//DC6D
            cmp(a, 0x04);//DC6F
            if (!c)
                return;//DC71
            lda(0x01);//DC73
            ldym(0x0704);//DC75
            if (!z)
                goto LabelDC84;//DC78
            ldam(0x1D);//DC7A
            if (z)
                goto LabelDC82;//DC7C
            cmp(a, 0x03);//DC7E
            if (!z)
                goto LabelDC86;//DC80
            LabelDC82:
            lda(0x02);//DC82
            LabelDC84:
            str(0x1D, a);//DC84
            LabelDC86:
            ldam(0xB5);//DC86
            cmp(a, 0x01);//DC88
            if (!z)
                return;//DC8A
            lda(0xFF);//DC8C
            str(0x0490, a);//DC8E
            ldam(0xCE);//DC91
            cmp(a, 0xCF);//DC93
            if (!c)
                goto LabelDC98;//DC95
            return;
            LabelDC98:
            ldy(0x02);//DC98
            ldam(0x0714);//DC9A
            if (!z)
                goto LabelDCAB;//DC9D
            ldam(0x0754);//DC9F
            if (!z)
                goto LabelDCAB;//DCA2
            dey();//DCA4
            ldam(0x0704);//DCA5
            if (!z)
                goto LabelDCAB;//DCA8
            dey();//DCAA
            LabelDCAB:
            ldam(0xE3AD + y);//DCAB
            str(0xEB, a);//DCAE
            tay();//DCB0
            ldxm(0x0754);//DCB1
            ldam(0x0714);//DCB4
            if (z)
                goto LabelDCBA;//DCB7
            inx();//DCB9
            LabelDCBA:
            ldam(0xCE);//DCBA
            cmp(a, ram[0xDC62 + x]);//DCBC
            if (!c)
                goto LabelDCF6;//DCBF
            LabelE3E9();//DCC1
            if (z)
                goto LabelDCF6;//DCC4
            LabelDFA1();//DCC6
            if (c)
                goto LabelDD1A;//DCC9
            ldym(0x9F);//DCCB
            if (!n)
                goto LabelDCF6;//DCCD
            ldym(0x04);//DCCF
            cmp(y, 0x04);//DCD1
            if (!c)
                goto LabelDCF6;//DCD3
            LabelDF8F();//DCD5
            if (c)
                goto LabelDCEA;//DCD8
            ldym(0x074E);//DCDA
            if (z)
                goto LabelDCF2;//DCDD
            ldym(0x0784);//DCDF
            if (!z)
                goto LabelDCF2;//DCE2
            LabelBCED();//DCE4
            goto LabelDCF6; //DCE7
            LabelDCEA:
            cmp(a, 0x26);//DCEA
            if (z)
                goto LabelDCF2;//DCEC
            lda(0x02);//DCEE
            str(0xFF, a);//DCF0
            LabelDCF2:
            lda(0x01);//DCF2
            str(0x9F, a);//DCF4
            LabelDCF6:
            ldym(0xEB);//DCF6
            ldam(0xCE);//DCF8
            cmp(a, 0xCF);//DCFA
            if (c)
                goto LabelDD5E;//DCFC
            LabelE3E8();//DCFE
            LabelDFA1();//DD01
            if (c)
                goto LabelDD1A;//DD04
            pha();//DD06
            LabelE3E8();//DD07
            str(0x00, a);//DD0A
            pla();//DD0C
            str(0x01, a);//DD0D
            if (!z)
                goto LabelDD1D;//DD0F
            ldam(0x00);//DD11
            if (z)
                goto LabelDD5E;//DD13
            LabelDFA1();//DD15
            if (!c)
                goto LabelDD1D;//DD18
            LabelDD1A:
            goto LabelDE05; //DD1A
            LabelDD1D:
            LabelDF9A();//DD1D
            if (c)
                goto LabelDD5E;//DD20
            ldym(0x9F);//DD22
            if (n) //
                goto LabelDD5E;//DD24
            cmp(a, 0xC5);//DD26
            if (!z)
                goto LabelDD2D;//DD28
            goto LabelDE0E; //DD2A
            LabelDD2D:
            LabelDEBD();//DD2D
            if (z)
                goto LabelDD5E;//DD30
            ldym(0x070E);//DD32
            if (!z)
                goto LabelDD5A;//DD35
            ldym(0x04);//DD37
            cmp(y, 0x05);//DD39
            if (!c)
                goto LabelDD44;//DD3B
            ldam(0x45);//DD3D
            str(0x00, a);//DD3F
            LabelDF4B(); //DD41
            return;
            LabelDD44:
            LabelDEC4();//DD44
            lda(0xF0);//DD47
            and(ram[0xCE]);//DD49
            str(0xCE, a);//DD4B
            LabelDEE8();//DD4D
            lda(0x00);//DD50
            str(0x9F, a);//DD52
            str(0x0433, a);//DD54
            str(0x0484, a);//DD57
            LabelDD5A:
            lda(0x00);//DD5A
            str(0x1D, a);//DD5C
            LabelDD5E:
            ldym(0xEB);//DD5E
            iny();//DD60
            iny();//DD61
            lda(0x02);//DD62
            str(0x00, a);//DD64
            LabelDD66:
            iny();//DD66
            str(0xEB, y);//DD67
            ldam(0xCE);//DD69
            cmp(a, 0x20);//DD6B
            if (!c)
                goto LabelDD85;//DD6D
            cmp(a, 0xE4);//DD6F
            if (c)
                return;//DD71
            LabelE3EC();//DD73
            if (z)
                goto LabelDD85;//DD76
            cmp(a, 0x1C);//DD78
            if (z)
                goto LabelDD85;//DD7A
            cmp(a, 0x6B);//DD7C
            if (z)
                goto LabelDD85;//DD7E
            LabelDF9A();//DD80
            if (!c)
                goto LabelDD9C;//DD83
            LabelDD85:
            ldym(0xEB);//DD85
            iny();//DD87
            ldam(0xCE);//DD88
            cmp(a, 0x08);//DD8A
            if (!c)
                return;//DD8C
            cmp(a, 0xD0);//DD8E
            if (c)
                return;//DD90
            LabelE3EC();//DD92
            if (!z)
                goto LabelDD9C;//DD95
            dec(0x00);//DD97
            if (!z)
                goto LabelDD66;//DD99
            return;
            LabelDD9C:
            LabelDEBD();//DD9C
            if (z)
                return;//DD9F
            LabelDF9A();//DDA1
            if (!c)
                goto LabelDDA9;//DDA4
            goto LabelDE2E; //DDA6
            LabelDDA9:
            LabelDFA1();//DDA9
            if (c)
                goto LabelDE05;//DDAC
            LabelDEDD();//DDAE
            if (!c)
                goto LabelDDBB;//DDB1
            ldam(0x070E);//DDB3
            if (!z)
                return;//DDB6
            LabelDDFF(); //DDB8
            return;
            LabelDDBB:
            ldym(0x1D);//DDBB
            cmp(y, 0x00);//DDBD
            if (!z)
                goto LabelDDFF;//DDBF
            ldym(0x33);//DDC1
            dey();//DDC3
            if (!z)
                goto LabelDDFF;//DDC4
            cmp(a, 0x6C);//DDC6
            if (z)
                goto LabelDDCE;//DDC8
            cmp(a, 0x1F);//DDCA
            if (!z)
                goto LabelDDFF;//DDCC
            LabelDDCE:
            ldam(0x03C4);//DDCE
            if (!z)
                goto LabelDDD7;//DDD1
            LabelDDD3:
            ldy(0x10);//DDD3
            str(0xFF, y);//DDD5
            LabelDDD7:
            or(0x20);//DDD7
            str(0x03C4, a);//DDD9
            ldam(0x86);//DDDC
            and(0x0F);//DDDE
            if (z)
                goto LabelDDF0;//DDE0
            ldy(0x00);//DDE2
            ldam(0x071A);//DDE4
            if (z)
                goto LabelDDEA;//DDE7
            iny();//DDE9
            LabelDDEA:
            ldam(0xDE03 + y);//DDEA
            str(0x06DE, a);//DDED
            LabelDDF0:
            ldam(0x0E);//DDF0
            cmp(a, 0x07);//DDF2
            if (z)
                return;//DDF4
            cmp(a, 0x08);//DDF6
            if (!z)
                return;//DDF8
            lda(0x02);//DDFA
            str(0x0E, a);//DDFC
            return;
            LabelDDFF:
            LabelDF4B();//DDFF
            return;
            LabelDE05:
            LabelDE1C();//DE05
            inc(0x0748);//DE08
            LabelBBFE(); //DE0B
            return;
            LabelDE0E:
            lda(0x00);//DE0E
            str(0x0772, a);//DE10
            LabelDE13:
            lda(0x02);//DE13
            str(0x0770, a);//DE15
            lda(0x18);//DE18
            str(0x57, a);//DE1A
            ldym(0x02);//DE1C
            lda(0x00);//DE1E
            str(W(0x06) + y, a);//DE20
            Label8A4D(); //DE22
            return;
            LabelDE2E:
            ldym(0x04);//DE2E
            cmp(y, 0x06);//DE30
            if (!c)
                return;//DE32
            cmp(y, 0x0A);//DE34
            if (!c)
                goto LabelDE39;//DE36
            return;
            LabelDE39:
            cmp(a, 0x24);//DE39
            if (z)
                goto LabelDE41;//DE3B
            cmp(a, 0x25);//DE3D
            if (!z)
                goto LabelDE7A;//DE3F
            LabelDE41:
            ldam(0x0E);//DE41
            cmp(a, 0x05);//DE43
            if (z)
                goto LabelDE88;//DE45
            lda(0x01);//DE47
            str(0x33, a);//DE49
            inc(0x0723);//DE4B
            ldam(0x0E);//DE4E
            cmp(a, 0x04);//DE50
            if (z)
                goto LabelDE73;//DE52
            lda(0x33);//DE54
            Label9716();//DE56
            lda(0x80);//DE59
            str(0xFC, a);//DE5B
            lsr();//DE5D
            str(0x0713, a);//DE5E
            ldx(0x04);//DE61
            ldam(0xCE);//DE63
            str(0x070F, a);//DE65
            LabelDE68:
            cmp(a, ram[0xDE29 + x]);//DE68
            if (c)
                goto LabelDE70;//DE6B
            dex();//DE6D
            if (!z)
                goto LabelDE68;//DE6E
            LabelDE70:
            str(0x010F, x);//DE70
            LabelDE73:
            lda(0x04);//DE73
            str(0x0E, a);//DE75
            goto LabelDE88; //DE77
            LabelDE7A:
            cmp(a, 0x26);//DE7A
            if (!z)
                goto LabelDE88;//DE7C
            ldam(0xCE);//DE7E
            cmp(a, 0x20);//DE80
            if (c)
                goto LabelDE88;//DE82
            lda(0x01);//DE84
            str(0x0E, a);//DE86
            LabelDE88:
            lda(0x03);//DE88
            str(0x1D, a);//DE8A
            lda(0x00);//DE8C
            str(0x57, a);//DE8E
            str(0x0705, a);//DE90
            ldam(0x86);//DE93
            c = true;//DE95
            sbc(ram[0x071C]);//DE96
            cmp(a, 0x10);//DE99
            if (c)
                goto LabelDEA1;//DE9B
            lda(0x02);//DE9D
            str(0x33, a);//DE9F
            LabelDEA1:
            ldym(0x33);//DEA1
            ldam(0x06);//DEA3
            asl();//DEA5
            asl();//DEA6
            asl();//DEA7
            asl();//DEA8
            c = false;//DEA9
            adc(ram[0xDE24 + y]);//DEAA
            str(0x86, a);//DEAD
            ldam(0x06);//DEAF
            if (!z)
                return;//DEB1
            ldam(0x071B);//DEB3
            c = false;//DEB6
            adc(ram[0xDE26 + y]);//DEB7
            str(0x6D, a);//DEBA
        }

        void LabelDDFF()
        {
            LabelDF4B();//DDFF
        }

        void LabelDE1C()
        {
            ldym(0x02);//DE1C
            lda(0x00);//DE1E
            str(W(0x06) + y, a);//DE20
            Label8A4D(); //DE22
        }

        void LabelDEBD()
        {
            cmp(a, 0x5F);//DEBD
            if (z)
                return;//DEBF
            cmp(a, 0x60);//DEC1
        }

        void LabelDEC4()
        {
            LabelDEDD();//DEC4
            if (!c)
                return;//DEC7
            lda(0x70);//DEC9
            str(0x0709, a);//DECB
            lda(0xF9);//DECE
            str(0x06DB, a);//DED0
            lda(0x03);//DED3
            str(0x0786, a);//DED5
            lsr();//DED8
            str(0x070E, a);//DED9
        }

        void LabelDEDD()
        {
            cmp(a, 0x67);//DEDD
            if (z)
                goto LabelDEE6;//DEDF
            cmp(a, 0x68);//DEE1
            c = false;//DEE3
            if (!z)
                return;//DEE4
            LabelDEE6:
            c = true;//DEE6
        }

        void LabelDEE8()
        {
            ldam(0x0B);//DEE8
            and(0x04);//DEEA
            if (z)
                return;//DEEC
            ldam(0x00);//DEEE
            cmp(a, 0x11);//DEF0
            if (!z)
                return;//DEF2
            ldam(0x01);//DEF4
            cmp(a, 0x10);//DEF6
            if (!z)
                return;//DEF8
            lda(0x30);//DEFA
            str(0x06DE, a);//DEFC
            lda(0x03);//DEFF
            str(0x0E, a);//DF01
            lda(0x10);//DF03
            str(0xFF, a);//DF05
            lda(0x20);//DF07
            str(0x03C4, a);//DF09
            ldam(0x06D6);//DF0C
            if (z)
                return;//DF0F
            and(0x03);//DF11
            asl();//DF13
            asl();//DF14
            tax();//DF15
            ldam(0x86);//DF16
            cmp(a, 0x60);//DF18
            if (!c)
                goto LabelDF22;//DF1A
            inx();//DF1C
            cmp(a, 0xA0);//DF1D
            if (!c)
                goto LabelDF22;//DF1F
            inx();//DF21
            LabelDF22:
            ldym(0x87F2 + x);//DF22
            dey();//DF25
            str(0x075F, y);//DF26
            ldxm(0x9CB4 + y);//DF29
            ldam(0x9CBC + x);//DF2C
            str(0x0750, a);//DF2F
            lda(0x80);//DF32
            str(0xFC, a);//DF34
            lda(0x00);//DF36
            str(0x0751, a);//DF38
            str(0x0760, a);//DF3B
            str(0x075C, a);//DF3E
            str(0x0752, a);//DF41
            inc(0x075D);//DF44
            inc(0x0757);//DF47
        }

        void LabelDF4B()
        {
            lda(0x00);//DF4B
            ldym(0x57);//DF4D
            ldxm(0x00);//DF4F
            dex();//DF51
            if (!z)
                goto LabelDF5E;//DF52
            inx();//DF54
            cmp(y, 0x00);//DF55
            if (n) //
                goto LabelDF81;//DF57
            lda(0xFF);//DF59
            goto LabelDF66; //DF5B
            LabelDF5E:
            ldx(0x02);//DF5E
            cmp(y, 0x01);//DF60
            if (!n)
                goto LabelDF81;//DF62
            lda(0x01);//DF64
            LabelDF66:
            ldy(0x10);//DF66
            str(0x0785, y);//DF68
            ldy(0x00);//DF6B
            str(0x57, y);//DF6D
            cmp(a, 0x00);//DF6F
            if (!n)
                goto LabelDF74;//DF71
            dey();//DF73
            LabelDF74:
            str(0x00, y);//DF74
            c = false;//DF76
            adc(ram[0x86]);//DF77
            str(0x86, a);//DF79
            ldam(0x6D);//DF7B
            adc(ram[0x00]);//DF7D
            str(0x6D, a);//DF7F
            LabelDF81:
            txa();//DF81
            eor(0xFF);//DF82
            and(ram[0x0490]);//DF84
            str(0x0490, a);//DF87
        }

        void LabelDF8F()
        {
            LabelDFB0();//DF8F
            cmp(a, ram[0xDF8B + x]);//DF92
        }

        void LabelDF9A()
        {
            LabelDFB0();//DF9A
            cmp(a, ram[0xDF96 + x]);//DF9D
        }

        void LabelDFA1()
        {
            cmp(a, 0xC2);//DFA1
            if (z)
                goto LabelDFAB;//DFA3
            cmp(a, 0xC3);//DFA5
            if (z)
                goto LabelDFAB;//DFA7
            c = false;//DFA9
            return;
            LabelDFAB:
            lda(0x01);//DFAB
            str(0xFE, a);//DFAD
        }

        void LabelDFB0()
        {
            tay();//DFB0
            and(0xC0);//DFB1
            asl();//DFB3
            rol();//DFB4
            rol();//DFB5
            tax();//DFB6
            tya();//DFB7
        }

        void LabelDFC1()
        {
            ldam(0x1E + x);//DFC1
            and(0x20);//DFC3
            if (!z)
                return;//DFC5
            LabelE15B();//DFC7
            if (!c)
                return;//DFCA
            ldym(0x16 + x);//DFCC
            cmp(y, 0x12);//DFCE
            if (!z)
                goto LabelDFD8;//DFD0
            ldam(0xCF + x);//DFD2
            cmp(a, 0x25);//DFD4
            if (!c)
                return;//DFD6
            LabelDFD8:
            cmp(y, 0x0E);//DFD8
            if (!z)
                goto LabelDFDF;//DFDA
            LabelE163(); //DFDC
            return;
            LabelDFDF:
            cmp(y, 0x05);//DFDF
            if (!z)
                goto LabelDFE6;//DFE1
            LabelE185(); //DFE3
            return;
            LabelDFE6:
            cmp(y, 0x12);//DFE6
            if (z)
                goto LabelDFF2;//DFE8
            cmp(y, 0x2E);//DFEA
            if (z)
                goto LabelDFF2;//DFEC
            cmp(y, 0x07);//DFEE
            if (c)
                return;//DFF0
            LabelDFF2:
            LabelE1AE();//DFF2
            if (!z)
                goto LabelDFFA;//DFF5
            LabelDFF7:
            LabelE0E2(); //DFF7
            return;
            LabelDFFA:
            LabelE1B5();//DFFA
            if (z)
                goto LabelDFF7;//DFFD
            cmp(a, 0x23);//DFFF
            if (!z)
            {
                LabelE067();//E001
                return;
            }
            ldym(0x02);//E003
            lda(0x00);//E005
            str(W(0x06) + y, a);//E007
            ldam(0x16 + x);//E009
            cmp(a, 0x15);//E00B
            if (c)
            {
                LabelE01B();//E00D
                return;
            }
            cmp(a, 0x06);//E00F
            if (!z)
                goto LabelE016;//E011
            LabelE18E();//E013
            LabelE016:
            lda(0x01);//E016
            LabelDA11();//E018
            LabelE01B();
        }

        void LabelE01B()
        {
            cmp(a, 0x09);//E01B
            if (!c)
                goto LabelE02F;//E01D
            cmp(a, 0x11);//E01F
            if (c)
                goto LabelE02F;//E021
            cmp(a, 0x0A);//E023
            if (!c)
                goto LabelE02B;//E025
            cmp(a, 0x0D);//E027
            if (!c)
                goto LabelE02F;//E029
            LabelE02B:
            and(0x01);//E02B
            str(0x16 + x, a);//E02D
            LabelE02F:
            ldam(0x1E + x);//E02F
            and(0xF0);//E031
            or(0x02);//E033
            str(0x1E + x, a);//E035
            dec(0xCF + x);//E037
            dec(0xCF + x);//E039
            ldam(0x16 + x);//E03B
            cmp(a, 0x07);//E03D
            if (z)
                goto LabelE048;//E03F
            lda(0xFD);//E041
            ldym(0x074E);//E043
            if (!z)
                goto LabelE04A;//E046
            LabelE048:
            lda(0xFF);//E048
            LabelE04A:
            str(0xA0 + x, a);//E04A
            ldy(0x01);//E04C
            LabelE143();//E04E
            if (!n)
                goto LabelE054;//E051
            iny();//E053
            LabelE054:
            ldam(0x16 + x);//E054
            cmp(a, 0x33);//E056
            if (z)
                goto LabelE060;//E058
            cmp(a, 0x08);//E05A
            if (z)
                goto LabelE060;//E05C
            str(0x46 + x, y);//E05E
            LabelE060:
            dey();//E060
            ldam(0xDFBF + y);//E061
            str(0x58 + x, a);//E064
        }


        void LabelE02F()
        {
            ldam(0x1E + x);//E02F
            and(0xF0);//E031
            or(0x02);//E033
            str(0x1E + x, a);//E035
            dec(0xCF + x);//E037
            dec(0xCF + x);//E039
            ldam(0x16 + x);//E03B
            cmp(a, 0x07);//E03D
            if (z)
                goto LabelE048;//E03F
            lda(0xFD);//E041
            ldym(0x074E);//E043
            if (!z)
                goto LabelE04A;//E046
            LabelE048:
            lda(0xFF);//E048
            LabelE04A:
            str(0xA0 + x, a);//E04A
            ldy(0x01);//E04C
            LabelE143();//E04E
            if (!n)
                goto LabelE054;//E051
            iny();//E053
            LabelE054:
            ldam(0x16 + x);//E054
            cmp(a, 0x33);//E056
            if (z)
                goto LabelE060;//E058
            cmp(a, 0x08);//E05A
            if (z)
                goto LabelE060;//E05C
            str(0x46 + x, y);//E05E
            LabelE060:
            dey();//E060
            ldam(0xDFBF + y);//E061
            str(0x58 + x, a);//E064
        }

        void LabelE067()
        {
            ldam(0x04);//E067
            c = true;//E069
            sbc(0x08);//E06A
            cmp(a, 0x05);//E06C
            if (c)
            {
                LabelE0E2();//E06E
                return;
            }
            ldam(0x1E + x);//E070
            and(0x40);//E072
            if (!z)
                goto LabelE0CD;//E074
            ldam(0x1E + x);//E076
            asl();//E078
            if (!c)
                goto LabelE07E;//E079
            LabelE07B:
            LabelE0FE(); //E07B
            return;
            LabelE07E:
            ldam(0x1E + x);//E07E
            if (z)
                goto LabelE07B;//E080
            cmp(a, 0x05);//E082
            if (z)
                goto LabelE0A5;//E084
            cmp(a, 0x03);//E086
            if (c)
                return;//E088
            ldam(0x1E + x);//E08A
            cmp(a, 0x02);//E08C
            if (!z)
                goto LabelE0A5;//E08E
            lda(0x10);//E090
            ldym(0x16 + x);//E092
            cmp(y, 0x12);//E094
            if (!z)
                goto LabelE09A;//E096
            lda(0x00);//E098
            LabelE09A:
            str(0x0796 + x, a);//E09A
            lda(0x03);//E09D
            str(0x1E + x, a);//E09F
            LabelE14F();//E0A1
            LabelE0A5:
            ldam(0x16 + x);//E0A5
            cmp(a, 0x06);//E0A7
            if (z)
                goto LabelE0CD;//E0A9
            cmp(a, 0x12);//E0AB
            if (!z)
                goto LabelE0BD;//E0AD
            lda(0x01);//E0AF
            str(0x46 + x, a);//E0B1
            lda(0x08);//E0B3
            str(0x58 + x, a);//E0B5
            ldam(0x09);//E0B7
            and(0x07);//E0B9
            if (z)
                goto LabelE0CD;//E0BB
            LabelE0BD:
            ldy(0x01);//E0BD
            LabelE143();//E0BF
            if (!n)
                goto LabelE0C5;//E0C2
            iny();//E0C4
            LabelE0C5:
            tya();//E0C5
            cmp(a, ram[0x46 + x]);//E0C6
            if (!z)
                goto LabelE0CD;//E0C8
            LabelE124();//E0CA
            LabelE0CD:
            LabelE14F();//E0CD
            ldam(0x1E + x);//E0D0
            and(0x80);//E0D2
            if (!z)
                goto LabelE0DB;//E0D4
            lda(0x00);//E0D6
            str(0x1E + x, a);//E0D8
            return;
            LabelE0DB:
            ldam(0x1E + x);//E0DB
            and(0xBF);//E0DD
            str(0x1E + x, a);//E0DF
        }
        void LabelE0E2()
        {
            ldam(0x16 + x);//E0E2
            cmp(a, 0x03);//E0E4
            if (!z)
                goto LabelE0EC;//E0E6
            ldam(0x1E + x);//E0E8
            if (z)
            {
                LabelE124();//E0EA
                return;
            }
            LabelE0EC:
            ldam(0x1E + x);//E0EC
            tay();//E0EE
            asl();//E0EF
            if (!c)
                goto LabelE0F9;//E0F0
            ldam(0x1E + x);//E0F2
            or(0x40);//E0F4
            LabelE0FC(); //E0F6
            return;
            LabelE0F9:
            ldam(0xDFB9 + y);//E0F9
            str(0x1E + x, a);//E0FC
        }

        void LabelE0FC()
        {
            str(0x1E + x, a);//E0FC
            LabelE0FE();
        }

        void LabelE0FE()
        {
            ldam(0xCF + x);//E0FE
            cmp(a, 0x20);//E100
            if (!c)
                return;//E102
            ldy(0x16);//E104
            lda(0x02);//E106
            str(0xEB, a);//E108
            LabelE10A:
            ldam(0xEB);//E10A
            cmp(a, ram[0x46 + x]);//E10C
            if (!z)
                goto LabelE11C;//E10E
            lda(0x01);//E110
            LabelE388();//E112
            if (z)
                goto LabelE11C;//E115
            LabelE1B5();//E117
            if (!z)
            {
                LabelE124();//E11A
                return;
            }
            LabelE11C:
            dec(0xEB);//E11C
            iny();//E11E
            cmp(y, 0x18);//E11F
            if (!c)
                goto LabelE10A;//E121
        }

        void LabelE124()
        {
            cmp(x, 0x05);//E124
            if (z)
                goto LabelE131;//E126
            ldam(0x1E + x);//E128
            asl();//E12A
            if (!c)
                goto LabelE131;//E12B
            lda(0x02);//E12D
            str(0xFF, a);//E12F
            LabelE131:
            ldam(0x16 + x);//E131
            cmp(a, 0x05);//E133
            if (!z)
                goto LabelE140;//E135
            lda(0x00);//E137
            str(0x00, a);//E139
            ldy(0xFA);//E13B
            LabelCA37(); //E13D
            return;
            LabelE140:
            LabelDB36(); //E140
            return;
        }

        void LabelE143()
        {
            ldam(0x87 + x);//E143
            c = true;//E145
            sbc(ram[0x86]);//E146
            str(0x00, a);//E148
            ldam(0x6E + x);//E14A
            sbc(ram[0x6D]);//E14C
        }

        void LabelE14F()
        {
            LabelC363();//E14F
            ldam(0xCF + x);//E152
            and(0xF0);//E154
            or(0x08);//E156
            str(0xCF + x, a);//E158
        }

        void LabelE15B()
        {
            ldam(0xCF + x);//E15B
            c = false;//E15D
            adc(0x3E);//E15E
            cmp(a, 0x44);//E160
        }

        void LabelE163()
        {
            LabelE15B();//E163
            if (!c)
                goto LabelE182;//E166
            ldam(0xA0 + x);//E168
            c = false;//E16A
            adc(0x02);//E16B
            cmp(a, 0x03);//E16D
            if (!c)
                goto LabelE182;//E16F
            LabelE1AE();//E171
            if (z)
                goto LabelE182;//E174
            LabelE1B5();//E176
            if (z)
                goto LabelE182;//E179
            LabelE14F();//E17B
            lda(0xFD);//E17E
            str(0xA0 + x, a);//E180
            LabelE182:
            LabelE0FE(); //E182
            return;
        }

        void LabelE185()
        {
            LabelE1AE();//E185
            if (z)
            {
                LabelE1A7();//E188
                return;
            }
            cmp(a, 0x23);//E18A
            if (!z)
            {
                LabelE196();//E18C
                return;
            }
            LabelE18E();
        }

        void LabelE18E()
        {
            LabelD795();//E18E
            lda(0xFC);//E191
            str(0xA0 + x, a);//E193
        }

        void LabelE196()
        {
            ldam(0x078A + x);//E196
            if (!z)
            {
                LabelE1A7();//E199
                return;
            }
            ldam(0x1E + x);//E19B
            and(0x88);//E19D
            str(0x1E + x, a);//E19F
            LabelE14F();//E1A1
            LabelE0FE(); //E1A4
            return;
        }

        void LabelE1A7()
        {
            ldam(0x1E + x);//E1A7
            or(0x01);//E1A9
            str(0x1E + x, a);//E1AB
        }

        void LabelE1AE()
        {
            lda(0x00);//E1AE
            ldy(0x15);//E1B0
            LabelE388(); //E1B2
            return;
        }

        void LabelE1B5()
        {
            cmp(a, 0x26);//E1B5
            if (z)
                return;//E1B7
            cmp(a, 0xC2);//E1B9
            if (z)
                return;//E1BB
            cmp(a, 0xC3);//E1BD
            if (z)
                return;//E1BF
            cmp(a, 0x5F);//E1C1
            if (z)
                return;//E1C3
            cmp(a, 0x60);//E1C5
        }

        void LabelE1C8()
        {
            ldam(0xD5 + x);//E1C8
            cmp(a, 0x18);//E1CA
            if (!c)
                goto LabelE1EF;//E1CC
            LabelE39C();//E1CE
            if (z)
                goto LabelE1EF;//E1D1
            LabelE1B5();//E1D3
            if (z)
                goto LabelE1EF;//E1D6
            ldam(0xA6 + x);//E1D8
            if (n) //
                goto LabelE1F4;//E1DA
            ldam(0x3A + x);//E1DC
            if (!z)
                goto LabelE1F4;//E1DE
            lda(0xFD);//E1E0
            str(0xA6 + x, a);//E1E2
            lda(0x01);//E1E4
            str(0x3A + x, a);//E1E6
            ldam(0xD5 + x);//E1E8
            and(0xF8);//E1EA
            str(0xD5 + x, a);//E1EC
            return;
            LabelE1EF:
            lda(0x00);//E1EF
            str(0x3A + x, a);//E1F1
            return;
            LabelE1F4:
            lda(0x80);//E1F4
            str(0x24 + x, a);//E1F6
            lda(0x02);//E1F8
            str(0xFF, a);//E1FA
        }

        void LabelE22D()
        {
            txa();//E22D
            c = false;//E22E
            adc(0x07);//E22F
            tax();//E231
            ldy(0x02);//E232
            if (!z)
                LabelE23D();//E234
        }

        void LabelE236()
        {
            txa();//E236
            c = false;//E237
            adc(0x09);//E238
            tax();//E23A
            ldy(0x06);//E23B
            LabelE23D();
        }

        void LabelE23D()
        {
            LabelE29C();//E23D
            LabelE2DE(); //E240
            return;
        }

        void LabelE243()
        {
            ldy(0x48);//E243
            str(0x00, y);//E245
            ldy(0x44);//E247
            LabelE252(); //E249
            return;
        }

        void LabelE24C()
        {
            ldy(0x08);//E24C
            str(0x00, y);//E24E
            ldy(0x04);//E250
            LabelE252();
        }

        void LabelE252()
        {
            ldam(0x87 + x);//E252
            c = true;//E254
            sbc(ram[0x071C]);//E255
            str(0x01, a);//E258
            ldam(0x6E + x);//E25A
            sbc(ram[0x071A]);//E25C
            if (n) //
                goto LabelE267;//E25F
            orm(0x01);//E261
            if (z)
                goto LabelE267;//E263
            ldym(0x00);//E265
            LabelE267:
            tya();//E267
            and(ram[0x03D1]);//E268
            str(0x03D8 + x, a);//E26B
            if (!z)
            {
                LabelE289();//E26E
                return;
            }
            LabelE27C(); //E270
            return;
        }

        void LabelE273()
        {
            inx();//E273
            LabelF1F6();//E274
            dex();//E277
            cmp(a, 0xFE);//E278
            if (c)
                LabelE289();//E27A
            LabelE27C();
        }

        void LabelE27C()
        {
            txa();//E27C
            c = false;//E27D
            adc(0x01);//E27E
            tax();//E280
            ldy(0x01);//E281
            LabelE29C();//E283
            LabelE2DE(); //E286
            return;
        }

        void LabelE289()
        {
            txa();//E289
            asl();//E28A
            asl();//E28B
            tay();//E28C
            lda(0xFF);//E28D
            str(0x04B0 + y, a);//E28F
            str(0x04B1 + y, a);//E292
            str(0x04B2 + y, a);//E295
            str(0x04B3 + y, a);//E298
        }

        void LabelE29C()
        {
            str(0x00, x);//E29C
            ldam(0x03B8 + y);//E29E
            str(0x02, a);//E2A1
            ldam(0x03AD + y);//E2A3
            str(0x01, a);//E2A6
            txa();//E2A8
            asl();//E2A9
            asl();//E2AA
            pha();//E2AB
            tay();//E2AC
            ldam(0x0499 + x);//E2AD
            asl();//E2B0
            asl();//E2B1
            tax();//E2B2
            ldam(0x01);//E2B3
            c = false;//E2B5
            adc(ram[0xE1FD + x]);//E2B6
            str(0x04AC + y, a);//E2B9
            ldam(0x01);//E2BC
            c = false;//E2BE
            adc(ram[0xE1FF + x]);//E2BF
            str(0x04AE + y, a);//E2C2
            inx();//E2C5
            iny();//E2C6
            ldam(0x02);//E2C7
            c = false;//E2C9
            adc(ram[0xE1FD + x]);//E2CA
            str(0x04AC + y, a);//E2CD
            ldam(0x02);//E2D0
            c = false;//E2D2
            adc(ram[0xE1FF + x]);//E2D3
            str(0x04AE + y, a);//E2D6
            pla();//E2D9
            tay();//E2DA
            ldxm(0x00);//E2DB
        }

        void LabelE2DE()
        {
            ldam(0x071C);//E2DE
            c = false;//E2E1
            adc(0x80);//E2E2
            str(0x02, a);//E2E4
            ldam(0x071A);//E2E6
            adc(0x00);//E2E9
            str(0x01, a);//E2EB
            ldam(0x86 + x);//E2ED
            cmp(a, ram[0x02]);//E2EF
            ldam(0x6D + x);//E2F1
            sbc(ram[0x01]);//E2F3
            if (!c)
                goto LabelE30C;//E2F5
            ldam(0x04AE + y);//E2F7
            if (n) //
                goto LabelE309;//E2FA
            lda(0xFF);//E2FC
            ldxm(0x04AC + y);//E2FE
            if (n) //
                goto LabelE306;//E301
            str(0x04AC + y, a);//E303
            LabelE306:
            str(0x04AE + y, a);//E306
            LabelE309:
            ldxm(0x08);//E309
            return;
            LabelE30C:
            ldam(0x04AC + y);//E30C
            if (!n)
                goto LabelE322;//E30F
            cmp(a, 0xA0);//E311
            if (!c)
                goto LabelE322;//E313
            lda(0x00);//E315
            ldxm(0x04AE + y);//E317
            if (!n)
                goto LabelE31F;//E31A
            str(0x04AE + y, a);//E31C
            LabelE31F:
            str(0x04AC + y, a);//E31F
            LabelE322:
            ldxm(0x08);//E322
        }

        void LabelE325()
        {
            ldx(0x00);//E325
            LabelE327();
        }

        void LabelE327()
        {
            str(0x06, y);//E327
            lda(0x01);//E329
            str(0x07, a);//E32B
            LabelE32D();
        }

        void LabelE32D()
        {
            LabelE32D:
            ldam(0x04AC + y);//E32D
            cmp(a, ram[0x04AC + x]);//E330
            if (c)
                goto LabelE35F;//E333
            cmp(a, ram[0x04AE + x]);//E335
            if (!c)
                goto LabelE34C;//E338
            if (z)
                goto LabelE37E;//E33A
            ldam(0x04AE + y);//E33C
            cmp(a, ram[0x04AC + y]);//E33F
            if (!c)
                goto LabelE37E;//E342
            cmp(a, ram[0x04AC + x]);//E344
            if (c)
                goto LabelE37E;//E347
            ldym(0x06);//E349
            return;
            LabelE34C:
            ldam(0x04AE + x);//E34C
            cmp(a, ram[0x04AC + x]);//E34F
            if (!c)
                goto LabelE37E;//E352
            ldam(0x04AE + y);//E354
            cmp(a, ram[0x04AC + x]);//E357
            if (c)
                goto LabelE37E;//E35A
            ldym(0x06);//E35C
            return;
            LabelE35F:
            cmp(a, ram[0x04AC + x]);//E35F
            if (z)
                goto LabelE37E;//E362
            cmp(a, ram[0x04AE + x]);//E364
            if (!c)
                goto LabelE37E;//E367
            if (z)
                goto LabelE37E;//E369
            cmp(a, ram[0x04AE + y]);//E36B
            if (!c)
                goto LabelE37A;//E36E
            if (z)
                goto LabelE37A;//E370
            ldam(0x04AE + y);//E372
            cmp(a, ram[0x04AC + x]);//E375
            if (c)
                goto LabelE37E;//E378
            LabelE37A:
            c = false;//E37A
            ldym(0x06);//E37B
            return;
            LabelE37E:
            inx();//E37E
            iny();//E37F
            dec(0x07);//E380
            if (!n)
                goto LabelE32D;//E382
            c = true;//E384
            ldym(0x06);//E385
        }

        void LabelE388()
        {
            pha();//E388
            txa();//E389
            c = false;//E38A
            adc(0x01);//E38B
            tax();//E38D
            pla();//E38E
            LabelE3A5(); //E38F
            return;
        }

        void LabelE392()
        {
            txa();//E392
            c = false;//E393
            adc(0x0D);//E394
            tax();//E396
            ldy(0x1B);//E397
            LabelE3A3(); //E399
            return;
        }

        void LabelE39C()
        {
            ldy(0x1A);//E39C
            txa();//E39E
            c = false;//E39F
            adc(0x07);//E3A0
            tax();//E3A2
            LabelE3A3();
        }

        void LabelE3A3()
        {
            lda(0x00);//E3A3
            LabelE3A5();
        }

        void LabelE3A5()
        {
            LabelE3F0();//E3A5
            ldxm(0x08);//E3A8
            cmp(a, 0x00);//E3AA
        }

        void LabelE3E8()
        {
            iny();//E3E8
            LabelE3E9();
        }

        void LabelE3E9()
        {
            lda(0x00);//E3E9
            ldx(0x00);//E3EE
            LabelE3F0();
        }

        void LabelE3EC()
        {
            lda(0x01);//E3EC
            ldx(0x00);//E3EE
            LabelE3F0();
        }

        void LabelE3F0()
        {
            pha();//E3F0
            str(0x04, y);//E3F1
            ldam(0xE3B0 + y);//E3F3
            c = false;//E3F6
            adc(ram[0x86 + x]);//E3F7
            str(0x05, a);//E3F9
            ldam(0x6D + x);//E3FB
            adc(0x00);//E3FD
            and(0x01);//E3FF
            lsr();//E401
            orm(0x05);//E402
            ror();//E404
            lsr();//E405
            lsr();//E406
            lsr();//E407
            Label9BE1();//E408
            ldym(0x04);//E40B
            ldam(0xCE + x);//E40D
            c = false;//E40F
            adc(ram[0xE3CC + y]);//E410
            and(0xF0);//E413
            c = true;//E415
            sbc(0x20);//E416
            str(0x02, a);//E418
            tay();//E41A
            ldam(W(0x06) + y);//E41B
            str(0x03, a);//E41D
            ldym(0x04);//E41F
            pla();//E421
            if (!z)
                goto LabelE429;//E422
            ldam(0xCE + x);//E424
            goto LabelE42B; //E426
            LabelE429:
            ldam(0x86 + x);//E429
            LabelE42B:
            and(0x0F);//E42B
            str(0x04, a);//E42D
            ldam(0x03);//E42F
        }

        void LabelE435()
        {
            str(0x00, y);//E435
            ldam(0x03B9);//E437
            c = false;//E43A
            adc(ram[0xE433 + y]);//E43B
            ldxm(0x039A + y);//E43E
            ldym(0x06E5 + x);//E441
            str(0x02, y);//E444
            LabelE4AE();//E446
            ldam(0x03AE);//E449
            str(0x0203 + y, a);//E44C
            str(0x020B + y, a);//E44F
            str(0x0213 + y, a);//E452
            c = false;//E455
            adc(0x06);//E456
            str(0x0207 + y, a);//E458
            str(0x020F + y, a);//E45B
            str(0x0217 + y, a);//E45E
            lda(0x21);//E461
            str(0x0202 + y, a);//E463
            str(0x020A + y, a);//E466
            str(0x0212 + y, a);//E469
            or(0x40);//E46C
            str(0x0206 + y, a);//E46E
            str(0x020E + y, a);//E471
            str(0x0216 + y, a);//E474
            ldx(0x05);//E477
            LabelE479:
            lda(0xE1);//E479
            str(0x0201 + y, a);//E47B
            iny();//E47E
            iny();//E47F
            iny();//E480
            iny();//E481
            dex();//E482
            if (!n)
                goto LabelE479;//E483
            ldym(0x02);//E485
            ldam(0x00);//E487
            if (!z)
                goto LabelE490;//E489
            lda(0xE0);//E48B
            str(0x0201 + y, a);//E48D
            LabelE490:
            ldx(0x00);//E490
            LabelE492:
            ldam(0x039D);//E492
            c = true;//E495
            sbc(ram[0x0200 + y]);//E496
            cmp(a, 0x64);//E499
            if (!c)
                goto LabelE4A2;//E49B
            lda(0xF8);//E49D
            str(0x0200 + y, a);//E49F
            LabelE4A2:
            iny();//E4A2
            iny();//E4A3
            iny();//E4A4
            iny();//E4A5
            inx();//E4A6
            cmp(x, 0x06);//E4A7
            if (!z)
                goto LabelE492;//E4A9
            ldym(0x00);//E4AB
        }

        void LabelE4AE()
        {
            ldx(0x06);//E4AE
            LabelE4B0:
            str(0x0200 + y, a);//E4B0
            c = false;//E4B3
            adc(0x08);//E4B4
            iny();//E4B6
            iny();//E4B7
            iny();//E4B8
            iny();//E4B9
            dex();//E4BA
            if (!z)
                goto LabelE4B0;//E4BB
            ldym(0x02);//E4BD
        }

        void LabelE4DC()
        {
            ldym(0x06F3 + x);//E4DC
            ldam(0x0747);//E4DF
            if (!z)
                goto LabelE4EC;//E4E2
            ldam(0x2A + x);//E4E4
            and(0x7F);//E4E6
            cmp(a, 0x01);//E4E8
            if (z)
                goto LabelE4F0;//E4EA
            LabelE4EC:
            ldx(0x00);//E4EC
            if (z)
                goto LabelE4F7;//E4EE
            LabelE4F0:
            ldam(0x09);//E4F0
            lsr();//E4F2
            lsr();//E4F3
            and(0x03);//E4F4
            tax();//E4F6
            LabelE4F7:
            ldam(0x03BE);//E4F7
            c = false;//E4FA
            adc(ram[0xE4C4 + x]);//E4FB
            str(0x0200 + y, a);//E4FE
            c = false;//E501
            adc(ram[0xE4CC + x]);//E502
            str(0x0204 + y, a);//E505
            ldam(0x03B3);//E508
            c = false;//E50B
            adc(ram[0xE4C0 + x]);//E50C
            str(0x0203 + y, a);//E50F
            c = false;//E512
            adc(ram[0xE4C8 + x]);//E513
            str(0x0207 + y, a);//E516
            ldam(0xE4D0 + x);//E519
            str(0x0201 + y, a);//E51C
            ldam(0xE4D4 + x);//E51F
            str(0x0205 + y, a);//E522
            ldam(0xE4D8 + x);//E525
            str(0x0202 + y, a);//E528
            str(0x0206 + y, a);//E52B
            ldxm(0x08);//E52E
            ldam(0x03D6);//E530
            and(0xFC);//E533
            if (z)
                return;//E535
            lda(0x00);//E537
            str(0x2A + x, a);//E539
            lda(0xF8);//E53B
            LabelE5C1();//E53D
        }

        void LabelE54B()
        {
            ldym(0x06E5 + x);//E54B
            ldam(0x03AE);//E54E
            str(0x0203 + y, a);//E551
            c = false;//E554
            adc(0x08);//E555
            str(0x0207 + y, a);//E557
            str(0x020B + y, a);//E55A
            c = false;//E55D
            adc(0x0C);//E55E
            str(0x05, a);//E560
            ldam(0xCF + x);//E562
            LabelE5C1();//E564
            adc(0x08);//E567
            str(0x0208 + y, a);//E569
            ldam(0x010D);//E56C
            str(0x02, a);//E56F
            lda(0x01);//E571
            str(0x03, a);//E573
            str(0x04, a);//E575
            str(0x0202 + y, a);//E577
            str(0x0206 + y, a);//E57A
            str(0x020A + y, a);//E57D
            lda(0x7E);//E580
            str(0x0201 + y, a);//E582
            str(0x0209 + y, a);//E585
            lda(0x7F);//E588
            str(0x0205 + y, a);//E58A
            ldam(0x070F);//E58D
            if (z)
                goto LabelE5A7;//E590
            tya();//E592
            c = false;//E593
            adc(0x0C);//E594
            tay();//E596
            ldam(0x010F);//E597
            asl();//E59A
            tax();//E59B
            ldam(0xE541 + x);//E59C
            str(0x00, a);//E59F
            ldam(0xE542 + x);//E5A1
            LabelEBB2();//E5A4
            LabelE5A7:
            ldxm(0x08);//E5A7
            ldym(0x06E5 + x);//E5A9
            ldam(0x03D1);//E5AC
            and(0x0E);//E5AF
            if (z)
                return;//E5B1
            LabelE5B3();
        }

        void LabelE5B3()
        {
            lda(0xF8);//E5B3
            LabelE5B5();
        }

        void LabelE5B5()
        {
            str(0x0214 + y, a);//E5B5
            str(0x0210 + y, a);//E5B8
            LabelE5BB();
        }

        void LabelE5BB()
        {
            str(0x020C + y, a);//E5BB
            LabelE5BE();
        }

        void LabelE5BE()
        {
            str(0x0208 + y, a);//E5BE
            LabelE5C1();
        }

        void LabelE5C1()
        {
            str(0x0204 + y, a);//E5C1
            str(0x0200 + y, a);//E5C4
        }

        void LabelE5C8()
        {
            ldym(0x06E5 + x);//E5C8
            str(0x02, y);//E5CB
            iny();//E5CD
            iny();//E5CE
            iny();//E5CF
            ldam(0x03AE);//E5D0
            LabelE4AE();//E5D3
            ldxm(0x08);//E5D6
            ldam(0xCF + x);//E5D8
            LabelE5BB();//E5DA
            ldym(0x074E);//E5DD
            cmp(y, 0x03);//E5E0
            if (z)
                goto LabelE5E9;//E5E2
            ldym(0x06CC);//E5E4
            if (z)
                goto LabelE5EB;//E5E7
            LabelE5E9:
            lda(0xF8);//E5E9
            LabelE5EB:
            ldym(0x06E5 + x);//E5EB
            str(0x0210 + y, a);//E5EE
            str(0x0214 + y, a);//E5F1
            lda(0x5B);//E5F4
            ldxm(0x0743);//E5F6
            if (z)
                goto LabelE5FD;//E5F9
            lda(0x75);//E5FB
            LabelE5FD:
            ldxm(0x08);//E5FD
            iny();//E5FF
            LabelE5B5();//E600
            lda(0x02);//E603
            iny();//E605
            LabelE5B5();//E606
            inx();//E609
            LabelF1F6();//E60A
            dex();//E60D
            ldym(0x06E5 + x);//E60E
            asl();//E611
            pha();//E612
            if (!c)
                goto LabelE61A;//E613
            lda(0xF8);//E615
            str(0x0200 + y, a);//E617
            LabelE61A:
            pla();//E61A
            asl();//E61B
            pha();//E61C
            if (!c)
                goto LabelE624;//E61D
            lda(0xF8);//E61F
            str(0x0204 + y, a);//E621
            LabelE624:
            pla();//E624
            asl();//E625
            pha();//E626
            if (!c)
                goto LabelE62E;//E627
            lda(0xF8);//E629
            str(0x0208 + y, a);//E62B
            LabelE62E:
            pla();//E62E
            asl();//E62F
            pha();//E630
            if (!c)
                goto LabelE638;//E631
            lda(0xF8);//E633
            str(0x020C + y, a);//E635
            LabelE638:
            pla();//E638
            asl();//E639
            pha();//E63A
            if (!c)
                goto LabelE642;//E63B
            lda(0xF8);//E63D
            str(0x0210 + y, a);//E63F
            LabelE642:
            pla();//E642
            asl();//E643
            if (!c)
                goto LabelE64B;//E644
            lda(0xF8);//E646
            str(0x0214 + y, a);//E648
            LabelE64B:
            ldam(0x03D1);//E64B
            asl();//E64E
            if (!c)
                return;//E64F
            LabelE5B3();//E651
        }

        void LabelE655()
        {
            ldam(0x09);//E655
            lsr();//E657
            if (c)
                goto LabelE65C;//E658
            dec(0xDB + x);//E65A
            LabelE65C:
            ldam(0xDB + x);//E65C
            LabelE5C1();//E65E
            ldam(0x03B3);//E661
            str(0x0203 + y, a);//E664
            c = false;//E667
            adc(0x08);//E668
            str(0x0207 + y, a);//E66A
            lda(0x02);//E66D
            str(0x0202 + y, a);//E66F
            str(0x0206 + y, a);//E672
            lda(0xF7);//E675
            str(0x0201 + y, a);//E677
            lda(0xFB);//E67A
            str(0x0205 + y, a);//E67C
            return; //E67F
        }

        void LabelE686()
        {
            ldym(0x06F3 + x);//E686
            ldam(0x2A + x);//E689
            cmp(a, 0x02);//E68B
            if (c)
            {
                LabelE655();//E68D
                return;
            }
            ldam(0xDB + x);//E68F
            str(0x0200 + y, a);//E691
            c = false;//E694
            adc(0x08);//E695
            str(0x0204 + y, a);//E697
            ldam(0x03B3);//E69A
            str(0x0203 + y, a);//E69D
            str(0x0207 + y, a);//E6A0
            ldam(0x09);//E6A3
            lsr();//E6A5
            and(0x03);//E6A6
            tax();//E6A8
            ldam(0xE682 + x);//E6A9
            iny();//E6AC
            LabelE5C1();//E6AD
            dey();//E6B0
            lda(0x02);//E6B1
            str(0x0202 + y, a);//E6B3
            lda(0x82);//E6B6
            str(0x0206 + y, a);//E6B8
            ldxm(0x08);//E6BB
        }

        void LabelE6D2()
        {
            ldym(0x06EA);//E6D2
            ldam(0x03B9);//E6D5
            c = false;//E6D8
            adc(0x08);//E6D9
            str(0x02, a);//E6DB
            ldam(0x03AE);//E6DD
            str(0x05, a);//E6E0
            ldxm(0x39);//E6E2
            ldam(0xE6CE + x);//E6E4
            orm(0x03CA);//E6E7
            str(0x04, a);//E6EA
            txa();//E6EC
            pha();//E6ED
            asl();//E6EE
            asl();//E6EF
            tax();//E6F0
            lda(0x01);//E6F1
            str(0x07, a);//E6F3
            str(0x03, a);//E6F5
            LabelE6F7:
            ldam(0xE6BE + x);//E6F7
            str(0x00, a);//E6FA
            ldam(0xE6BF + x);//E6FC
            LabelEBB2();//E6FF
            dec(0x07);//E702
            if (!n)
                goto LabelE6F7;//E704
            ldym(0x06EA);//E706
            pla();//E709
            if (z)
                goto LabelE73B;//E70A
            cmp(a, 0x03);//E70C
            if (z)
                goto LabelE73B;//E70E
            str(0x00, a);//E710
            ldam(0x09);//E712
            lsr();//E714
            and(0x03);//E715
            orm(0x03CA);//E717
            str(0x0202 + y, a);//E71A
            str(0x0206 + y, a);//E71D
            ldxm(0x00);//E720
            dex();//E722
            if (z)
                goto LabelE72B;//E723
            str(0x020A + y, a);//E725
            str(0x020E + y, a);//E728
            LabelE72B:
            ldam(0x0206 + y);//E72B
            or(0x40);//E72E
            str(0x0206 + y, a);//E730
            ldam(0x020E + y);//E733
            or(0x40);//E736
            str(0x020E + y, a);//E738
            LabelE73B:
            LabelEB64(); //E73B
            return;
        }

        void LabelE87D()
        {
            ldam(0xCF + x);//E87D
            str(0x02, a);//E87F
            ldam(0x03AE);//E881
            str(0x05, a);//E884
            ldym(0x06E5 + x);//E886
            str(0xEB, y);//E889
            lda(0x00);//E88B
            str(0x0109, a);//E88D
            ldam(0x46 + x);//E890
            str(0x03, a);//E892
            ldam(0x03C5 + x);//E894
            str(0x04, a);//E897
            ldam(0x16 + x);//E899
            cmp(a, 0x0D);//E89B
            if (!z)
                goto LabelE8A9;//E89D
            ldym(0x58 + x);//E89F
            if (n) //
                goto LabelE8A9;//E8A1
            ldym(0x078A + x);//E8A3
            if (z)
                goto LabelE8A9;//E8A6
            return;
            LabelE8A9:
            ldam(0x1E + x);//E8A9
            str(0xED, a);//E8AB
            and(0x1F);//E8AD
            tay();//E8AF
            ldam(0x16 + x);//E8B0
            cmp(a, 0x35);//E8B2
            if (!z)
                goto LabelE8BE;//E8B4
            ldy(0x00);//E8B6
            lda(0x01);//E8B8
            str(0x03, a);//E8BA
            lda(0x15);//E8BC
            LabelE8BE:
            cmp(a, 0x33);//E8BE
            if (!z)
                goto LabelE8D5;//E8C0
            dec(0x02);//E8C2
            lda(0x03);//E8C4
            ldym(0x078A + x);//E8C6
            if (z)
                goto LabelE8CD;//E8C9
            or(0x20);//E8CB
            LabelE8CD:
            str(0x04, a);//E8CD
            ldy(0x00);//E8CF
            str(0xED, y);//E8D1
            lda(0x08);//E8D3
            LabelE8D5:
            cmp(a, 0x32);//E8D5
            if (!z)
                goto LabelE8E1;//E8D7
            ldy(0x03);//E8D9
            ldxm(0x070E);//E8DB
            ldam(0xE878 + x);//E8DE
            LabelE8E1:
            str(0xEF, a);//E8E1
            str(0xEC, y);//E8E3
            ldxm(0x08);//E8E5
            cmp(a, 0x0C);//E8E7
            if (!z)
                goto LabelE8F2;//E8E9
            ldam(0xA0 + x);//E8EB
            if (n) //
                goto LabelE8F2;//E8ED
            inc(0x0109);//E8EF
            LabelE8F2:
            ldam(0x036A);//E8F2
            if (z)
                goto LabelE900;//E8F5
            ldy(0x16);//E8F7
            cmp(a, 0x01);//E8F9
            if (z)
                goto LabelE8FE;//E8FB
            iny();//E8FD
            LabelE8FE:
            str(0xEF, y);//E8FE
            LabelE900:
            ldym(0xEF);//E900
            cmp(y, 0x06);//E902
            if (!z)
                goto LabelE923;//E904
            ldam(0x1E + x);//E906
            cmp(a, 0x02);//E908
            if (!c)
                goto LabelE910;//E90A
            ldx(0x04);//E90C
            str(0xEC, x);//E90E
            LabelE910:
            and(0x20);//E910
            orm(0x0747);//E912
            if (!z)
                goto LabelE923;//E915
            ldam(0x09);//E917
            and(0x08);//E919
            if (!z)
                goto LabelE923;//E91B
            ldam(0x03);//E91D
            eor(0x03);//E91F
            str(0x03, a);//E921
            LabelE923:
            ldam(0xE85B + y);//E923
            orm(0x04);//E926
            str(0x04, a);//E928
            ldam(0xE840 + y);//E92A
            tax();//E92D
            ldym(0xEC);//E92E
            ldam(0x036A);//E930
            if (z)
            {
                LabelE965();//E933
                return;
            }
            cmp(a, 0x01);//E935
            if (!z)
            {
                LabelE94C();//E937
                return;
            }
            ldam(0x0363);//E939
            if (!n)
                goto LabelE940;//E93C
            ldx(0xDE);//E93E
            LabelE940:
            ldam(0xED);//E940
            and(0x20);//E942
            if (z)
                LabelE949();//E944
        }

        void LabelE946()
        {
            str(0x0109, x);//E946
            LabelE949();
        }

        void LabelE949()
        {
            LabelEA4B(); //E949
            return;
        }

        void LabelE94C()
        {
            ldam(0x0363);//E94C
            and(0x01);//E94F
            if (z)
                goto LabelE955;//E951
            ldx(0xE4);//E953
            LabelE955:
            ldam(0xED);//E955
            and(0x20);//E957
            if (z)
            {
                LabelE949();//E959
                return;
            }
            ldam(0x02);//E95B
            c = true;//E95D
            sbc(0x10);//E95E
            str(0x02, a);//E960
            LabelE946(); //E962
            return;
        }

        void LabelE965()
        {
            cmp(x, 0x24);//E965
            if (!z)
                goto LabelE97A;//E967
            cmp(y, 0x05);//E969
            if (!z)
                goto LabelE977;//E96B
            ldx(0x30);//E96D
            lda(0x02);//E96F
            str(0x03, a);//E971
            lda(0x05);//E973
            str(0xEC, a);//E975
            LabelE977:
            goto LabelE9CA; //E977
            LabelE97A:
            cmp(x, 0x90);//E97A
            if (!z)
                goto LabelE990;//E97C
            ldam(0xED);//E97E
            and(0x20);//E980
            if (!z)
                goto LabelE98D;//E982
            ldam(0x078F);//E984
            cmp(a, 0x10);//E987
            if (c)
                goto LabelE98D;//E989
            ldx(0x96);//E98B
            LabelE98D:
            goto LabelEA37; //E98D
            LabelE990:
            ldam(0xEF);//E990
            cmp(a, 0x04);//E992
            if (c)
                goto LabelE9A6;//E994
            cmp(y, 0x02);//E996
            if (!c)
                goto LabelE9A6;//E998
            ldx(0x5A);//E99A
            ldym(0xEF);//E99C
            cmp(y, 0x02);//E99E
            if (!z)
                goto LabelE9A6;//E9A0
            ldx(0x7E);//E9A2
            inc(0x02);//E9A4
            LabelE9A6:
            ldam(0xEC);//E9A6
            cmp(a, 0x04);//E9A8
            if (!z)
                goto LabelE9CA;//E9AA
            ldx(0x72);//E9AC
            inc(0x02);//E9AE
            ldym(0xEF);//E9B0
            cmp(y, 0x02);//E9B2
            if (z)
                goto LabelE9BA;//E9B4
            ldx(0x66);//E9B6
            inc(0x02);//E9B8
            LabelE9BA:
            cmp(y, 0x06);//E9BA
            if (!z)
                goto LabelE9CA;//E9BC
            ldx(0x54);//E9BE
            ldam(0xED);//E9C0
            and(0x20);//E9C2
            if (!z)
                goto LabelE9CA;//E9C4
            ldx(0x8A);//E9C6
            dec(0x02);//E9C8
            LabelE9CA:
            ldym(0x08);//E9CA
            ldam(0xEF);//E9CC
            cmp(a, 0x05);//E9CE
            if (!z)
                goto LabelE9DE;//E9D0
            ldam(0xED);//E9D2
            if (z)
                goto LabelE9FA;//E9D4
            and(0x08);//E9D6
            if (z)
                goto LabelEA37;//E9D8
            ldx(0xB4);//E9DA
            if (!z)
                goto LabelE9FA;//E9DC
            LabelE9DE:
            cmp(x, 0x48);//E9DE
            if (z)
                goto LabelE9FA;//E9E0
            ldam(0x0796 + y);//E9E2
            cmp(a, 0x05);//E9E5
            if (c)
                goto LabelEA37;//E9E7
            cmp(x, 0x3C);//E9E9
            if (!z)
                goto LabelE9FA;//E9EB
            cmp(a, 0x01);//E9ED
            if (z)
                goto LabelEA37;//E9EF
            inc(0x02);//E9F1
            inc(0x02);//E9F3
            inc(0x02);//E9F5
            goto LabelEA29; //E9F7
            LabelE9FA:
            ldam(0xEF);//E9FA
            cmp(a, 0x06);//E9FC
            if (z)
                goto LabelEA37;//E9FE
            cmp(a, 0x08);//EA00
            if (z)
                goto LabelEA37;//EA02
            cmp(a, 0x0C);//EA04
            if (z)
                goto LabelEA37;//EA06
            cmp(a, 0x18);//EA08
            if (c)
                goto LabelEA37;//EA0A
            ldy(0x00);//EA0C
            cmp(a, 0x15);//EA0E
            if (!z)
                goto LabelEA22;//EA10
            iny();//EA12
            ldam(0x075F);//EA13
            cmp(a, 0x07);//EA16
            if (c)
                goto LabelEA37;//EA18
            ldx(0xA2);//EA1A
            lda(0x03);//EA1C
            str(0xEC, a);//EA1E
            if (!z)
                goto LabelEA37;//EA20
            LabelEA22:
            ldam(0x09);//EA22
            and(ram[0xE876 + y]);//EA24
            if (!z)
                goto LabelEA37;//EA27
            LabelEA29:
            ldam(0xED);//EA29
            and(0xA0);//EA2B
            orm(0x0747);//EA2D
            if (!z)
                goto LabelEA37;//EA30
            txa();//EA32
            c = false;//EA33
            adc(0x06);//EA34
            tax();//EA36
            LabelEA37:
            ldam(0xED);//EA37
            and(0x20);//EA39
            if (z)
            {
                LabelEA4B();
                return;//EA3B
            }
            ldam(0xEF);//EA3D
            cmp(a, 0x04);//EA3F
            if (!c)
            {
                LabelEA4B();
                return;//EA41
            }
            ldy(0x01);//EA43
            str(0x0109, y);//EA45
            dey();//EA48
            str(0xEC, y);//EA49
            LabelEA4B();
        }

        void LabelEA4B()
        {
            ldym(0xEB);//EA4B
            LabelEBAA();//EA4D
            LabelEBAA();//EA50
            LabelEBAA();//EA53
            ldxm(0x08);//EA56
            ldym(0x06E5 + x);//EA58
            ldam(0xEF);//EA5B
            cmp(a, 0x08);//EA5D
            if (!z)
                LabelEA64();//EA5F
            LabelEA61();
        }

        void LabelEA61()
        {
            LabelEB64(); //EA61
            return;
        }

        void LabelEA64()
        {
            ldam(0x0109);//EA64
            if (z)
                goto LabelEAA6;//EA67
            ldam(0x0202 + y);//EA69
            or(0x80);//EA6C
            iny();//EA6E
            iny();//EA6F
            LabelE5B5();//EA70
            dey();//EA73
            dey();//EA74
            tya();//EA75
            tax();//EA76
            ldam(0xEF);//EA77
            cmp(a, 0x05);//EA79
            if (z)
                goto LabelEA8A;//EA7B
            cmp(a, 0x11);//EA7D
            if (z)
                goto LabelEA8A;//EA7F
            cmp(a, 0x15);//EA81
            if (c)
                goto LabelEA8A;//EA83
            txa();//EA85
            c = false;//EA86
            adc(0x08);//EA87
            tax();//EA89
            LabelEA8A:
            ldam(0x0201 + x);//EA8A
            pha();//EA8D
            ldam(0x0205 + x);//EA8E
            pha();//EA91
            ldam(0x0211 + y);//EA92
            str(0x0201 + x, a);//EA95
            ldam(0x0215 + y);//EA98
            str(0x0205 + x, a);//EA9B
            pla();//EA9E
            str(0x0215 + y, a);//EA9F
            pla();//EAA2
            str(0x0211 + y, a);//EAA3
            LabelEAA6:
            ldam(0x036A);//EAA6
            if (!z)
            {
                LabelEA61();//EAA9
                return;
            }
            ldam(0xEF);//EAAB
            ldxm(0xEC);//EAAD
            cmp(a, 0x05);//EAAF
            if (!z)
                goto LabelEAB6;//EAB1
            LabelEB64(); //EAB3
            return;
            LabelEAB6:
            cmp(a, 0x07);//EAB6
            if (z)
                goto LabelEAD7;//EAB8
            cmp(a, 0x0D);//EABA
            if (z)
                goto LabelEAD7;//EABC
            cmp(a, 0x0C);//EABE
            if (z)
                goto LabelEAD7;//EAC0
            cmp(a, 0x12);//EAC2
            if (!z)
                goto LabelEACA;//EAC4
            cmp(x, 0x05);//EAC6
            if (!z)
                goto LabelEB12;//EAC8
            LabelEACA:
            cmp(a, 0x15);//EACA
            if (!z)
                goto LabelEAD3;//EACC
            lda(0x42);//EACE
            str(0x0216 + y, a);//EAD0
            LabelEAD3:
            cmp(x, 0x02);//EAD3
            if (!c)
                goto LabelEB12;//EAD5
            LabelEAD7:
            ldam(0x036A);//EAD7
            if (!z)
                goto LabelEB12;//EADA
            ldam(0x0202 + y);//EADC
            and(0xA3);//EADF
            str(0x0202 + y, a);//EAE1
            str(0x020A + y, a);//EAE4
            str(0x0212 + y, a);//EAE7
            or(0x40);//EAEA
            cmp(x, 0x05);//EAEC
            if (!z)
                goto LabelEAF2;//EAEE
            or(0x80);//EAF0
            LabelEAF2:
            str(0x0206 + y, a);//EAF2
            str(0x020E + y, a);//EAF5
            str(0x0216 + y, a);//EAF8
            cmp(x, 0x04);//EAFB
            if (!z)
                goto LabelEB12;//EAFD
            ldam(0x020A + y);//EAFF
            or(0x80);//EB02
            str(0x020A + y, a);//EB04
            str(0x0212 + y, a);//EB07
            or(0x40);//EB0A
            str(0x020E + y, a);//EB0C
            str(0x0216 + y, a);//EB0F
            LabelEB12:
            ldam(0xEF);//EB12
            cmp(a, 0x11);//EB14
            if (!z)
                goto LabelEB4E;//EB16
            ldam(0x0109);//EB18
            if (!z)
                goto LabelEB3E;//EB1B
            ldam(0x0212 + y);//EB1D
            and(0x81);//EB20
            str(0x0212 + y, a);//EB22
            ldam(0x0216 + y);//EB25
            or(0x41);//EB28
            str(0x0216 + y, a);//EB2A
            ldxm(0x078F);//EB2D
            cmp(x, 0x10);//EB30
            if (c)
            {
                LabelEB64();
                return;
            }//EB32
            str(0x020E + y, a);//EB34
            and(0x81);//EB37
            str(0x020A + y, a);//EB39
            if (!c)
            {
                LabelEB64();
                return;
            }//EB3C
            LabelEB3E:
            ldam(0x0202 + y);//EB3E
            and(0x81);//EB41
            str(0x0202 + y, a);//EB43
            ldam(0x0206 + y);//EB46
            or(0x41);//EB49
            str(0x0206 + y, a);//EB4B
            LabelEB4E:
            ldam(0xEF);//EB4E
            cmp(a, 0x18);//EB50
            if (!c)
            {
                LabelEB64();
                return;
            }//EB52
            lda(0x82);//EB54
            str(0x020A + y, a);//EB56
            str(0x0212 + y, a);//EB59
            or(0x40);//EB5C
            str(0x020E + y, a);//EB5E
            str(0x0216 + y, a);//EB61
        }

        void LabelEB64()
        {
            ldxm(0x08);//EB64
            ldam(0x03D1);//EB66
            lsr();//EB69
            lsr();//EB6A
            lsr();//EB6B
            pha();//EB6C
            if (!c)
                goto LabelEB74;//EB6D
            lda(0x04);//EB6F
            LabelEBC1();//EB71
            LabelEB74:
            pla();//EB74
            lsr();//EB75
            pha();//EB76
            if (!c)
                goto LabelEB7E;//EB77
            lda(0x00);//EB79
            LabelEBC1();//EB7B
            LabelEB7E:
            pla();//EB7E
            lsr();//EB7F
            lsr();//EB80
            pha();//EB81
            if (!c)
                goto LabelEB89;//EB82
            lda(0x10);//EB84
            LabelEBB7();//EB86
            LabelEB89:
            pla();//EB89
            lsr();//EB8A
            pha();//EB8B
            if (!c)
                goto LabelEB93;//EB8C
            lda(0x08);//EB8E
            LabelEBB7();//EB90
            LabelEB93:
            pla();//EB93
            lsr();//EB94
            if (!c)
                return;//EB95
            LabelEBB7();//EB97
            ldam(0x16 + x);//EB9A
            cmp(a, 0x0C);//EB9C
            if (z)
                return;//EB9E
            ldam(0xB6 + x);//EBA0
            cmp(a, 0x02);//EBA2
            if (!z)
                return;//EBA4
            LabelC998();//EBA6
        }

        void LabelEBAA()
        {
            ldam(0xE73E + x);//EBAA
            str(0x00, a);//EBAD
            ldam(0xE73F + x);//EBAF
            LabelEBB2();
        }

        void LabelEBB2()
        {
            str(0x01, a);//EBB2
            LabelF282(); //EBB4
            return;
        }

        void LabelEBB7()
        {
            c = false;//EBB7
            adc(ram[0x06E5 + x]);//EBB8
            tay();//EBBB
            lda(0xF8);//EBBC
            LabelE5C1(); //EBBE
            return;
        }

        void LabelEBC1()
        {
            c = false;//EBC1
            adc(ram[0x06E5 + x]);//EBC2
            tay();//EBC5
            LabelEC4A();//EBC6
            str(0x0210 + y, a);//EBC9
        }

        void LabelEBD1()
        {
            ldam(0x03BC);//EBD1
            str(0x02, a);//EBD4
            ldam(0x03B1);//EBD6
            str(0x05, a);//EBD9
            lda(0x03);//EBDB
            str(0x04, a);//EBDD
            lsr();//EBDF
            str(0x03, a);//EBE0
            ldym(0x06EC + x);//EBE2
            ldx(0x00);//EBE5
            LabelEBE7:
            ldam(0xEBCD + x);//EBE7
            str(0x00, a);//EBEA
            ldam(0xEBCE + x);//EBEC
            LabelEBB2();//EBEF
            cmp(x, 0x04);//EBF2
            if (!z)
                goto LabelEBE7;//EBF4
            ldxm(0x08);//EBF6
            ldym(0x06EC + x);//EBF8
            ldam(0x074E);//EBFB
            cmp(a, 0x01);//EBFE
            if (z)
                goto LabelEC0A;//EC00
            lda(0x86);//EC02
            str(0x0201 + y, a);//EC04
            str(0x0205 + y, a);//EC07
            LabelEC0A:
            ldam(0x03E8 + x);//EC0A
            cmp(a, 0xC4);//EC0D
            if (!z)
                goto LabelEC35;//EC0F
            lda(0x87);//EC11
            iny();//EC13
            LabelE5BB();//EC14
            dey();//EC17
            lda(0x03);//EC18
            ldxm(0x074E);//EC1A
            dex();//EC1D
            if (z)
                goto LabelEC21;//EC1E
            lsr();//EC20
            LabelEC21:
            ldxm(0x08);//EC21
            str(0x0202 + y, a);//EC23
            or(0x40);//EC26
            str(0x0206 + y, a);//EC28
            or(0x80);//EC2B
            str(0x020E + y, a);//EC2D
            and(0x83);//EC30
            str(0x020A + y, a);//EC32
            LabelEC35:
            ldam(0x03D4);//EC35
            pha();//EC38
            and(0x04);//EC39
            if (z)
                goto LabelEC45;//EC3B
            lda(0xF8);//EC3D
            str(0x0204 + y, a);//EC3F
            str(0x020C + y, a);//EC42
            LabelEC45:
            pla();//EC45
            and(0x08);//EC46
            if (z)
                return;//EC48
            LabelEC4A();
        }

        void LabelEC46()
        {
            and(0x08);//EC46
            if (z)
                return;//EC48
            LabelEC4A();
        }

        void LabelEC4A()
        {
            lda(0xF8);//EC4A
            str(0x0200 + y, a);//EC4C
            str(0x0208 + y, a);//EC4F
        }

        void LabelEC53()
        {
            lda(0x02);//EC53
            str(0x00, a);//EC55
            lda(0x75);//EC57
            ldym(0x0E);//EC59
            cmp(y, 0x05);//EC5B
            if (z)
                goto LabelEC65;//EC5D
            lda(0x03);//EC5F
            str(0x00, a);//EC61
            lda(0x84);//EC63
            LabelEC65:
            ldym(0x06EC + x);//EC65
            iny();//EC68
            LabelE5BB();//EC69
            ldam(0x09);//EC6C
            asl();//EC6E
            asl();//EC6F
            asl();//EC70
            asl();//EC71
            and(0xC0);//EC72
            orm(0x00);//EC74
            iny();//EC76
            LabelE5BB();//EC77
            dey();//EC7A
            dey();//EC7B
            ldam(0x03BC);//EC7C
            LabelE5C1();//EC7F
            ldam(0x03B1);//EC82
            str(0x0203 + y, a);//EC85
            ldam(0x03F1 + x);//EC88
            c = true;//EC8B
            sbc(ram[0x071C]);//EC8C
            str(0x00, a);//EC8F
            c = true;//EC91
            sbc(ram[0x03B1]);//EC92
            adc(ram[0x00]);//EC95
            adc(0x06);//EC97
            str(0x0207 + y, a);//EC99
            ldam(0x03BD);//EC9C
            str(0x0208 + y, a);//EC9F
            str(0x020C + y, a);//ECA2
            ldam(0x03B2);//ECA5
            str(0x020B + y, a);//ECA8
            ldam(0x00);//ECAB
            c = true;//ECAD
            sbc(ram[0x03B2]);//ECAE
            adc(ram[0x00]);//ECB1
            adc(0x06);//ECB3
            str(0x020F + y, a);//ECB5
            ldam(0x03D4);//ECB8
            LabelEC46();//ECBB
            ldam(0x03D4);//ECBE
            asl();//ECC1
            if (!c)
                goto LabelECC9;//ECC2
            lda(0xF8);//ECC4
            LabelE5C1();//ECC6
            LabelECC9:
            ldam(0x00);//ECC9
            if (!n)
                return;//ECCB
            ldam(0x0203 + y);//ECCD
            cmp(a, ram[0x0207 + y]);//ECD0
            if (!c)
                return;//ECD3
            lda(0xF8);//ECD5
            str(0x0204 + y, a);//ECD7
            str(0x020C + y, a);//ECDA
        }

        void LabelECDE()
        {
            ldym(0x06F1 + x);//ECDE
            ldam(0x03BA);//ECE1
            str(0x0200 + y, a);//ECE4
            ldam(0x03AF);//ECE7
            str(0x0203 + y, a);//ECEA
            LabelECED();
        }

        void LabelECED()
        {
            ldam(0x09);//ECED
            lsr();//ECEF
            lsr();//ECF0
            pha();//ECF1
            and(0x01);//ECF2
            eor(0x64);//ECF4
            str(0x0201 + y, a);//ECF6
            pla();//ECF9
            lsr();//ECFA
            lsr();//ECFB
            lda(0x02);//ECFC
            if (!c)
                goto LabelED02;//ECFE
            or(0xC0);//ED00
            LabelED02:
            str(0x0202 + y, a);//ED02
        }

        void LabelED09()
        {
            ldym(0x06EC + x);//ED09
            ldam(0x24 + x);//ED0C
            inc(0x24 + x);//ED0E
            lsr();//ED10
            and(0x07);//ED11
            cmp(a, 0x03);//ED13
            if (c)
                LabelED61();//ED15
            LabelED17();
        }

        void LabelED17()
        {
            tax();//ED17
            ldam(0xED06 + x);//ED18
            iny();//ED1B
            LabelE5BB();//ED1C
            dey();//ED1F
            ldxm(0x08);//ED20
            ldam(0x03BA);//ED22
            c = true;//ED25
            sbc(0x04);//ED26
            str(0x0200 + y, a);//ED28
            str(0x0208 + y, a);//ED2B
            c = false;//ED2E
            adc(0x08);//ED2F
            str(0x0204 + y, a);//ED31
            str(0x020C + y, a);//ED34
            ldam(0x03AF);//ED37
            c = true;//ED3A
            sbc(0x04);//ED3B
            str(0x0203 + y, a);//ED3D
            str(0x0207 + y, a);//ED40
            c = false;//ED43
            adc(0x08);//ED44
            str(0x020B + y, a);//ED46
            str(0x020F + y, a);//ED49
            lda(0x02);//ED4C
            str(0x0202 + y, a);//ED4E
            lda(0x82);//ED51
            str(0x0206 + y, a);//ED53
            lda(0x42);//ED56
            str(0x020A + y, a);//ED58
            lda(0xC2);//ED5B
            str(0x020E + y, a);//ED5D
        }

        void LabelED61()
        {
            lda(0x00);//ED61
            str(0x24 + x, a);//ED63
        }

        void LabelED66()
        {
            ldym(0x06E5 + x);//ED66
            lda(0x5B);//ED69
            iny();//ED6B
            LabelE5B5();//ED6C
            iny();//ED6F
            lda(0x02);//ED70
            LabelE5B5();//ED72
            dey();//ED75
            dey();//ED76
            ldam(0x03AE);//ED77
            str(0x0203 + y, a);//ED7A
            str(0x020F + y, a);//ED7D
            c = false;//ED80
            adc(0x08);//ED81
            str(0x0207 + y, a);//ED83
            str(0x0213 + y, a);//ED86
            c = false;//ED89
            adc(0x08);//ED8A
            str(0x020B + y, a);//ED8C
            str(0x0217 + y, a);//ED8F
            ldam(0xCF + x);//ED92
            tax();//ED94
            pha();//ED95
            cmp(x, 0x20);//ED96
            if (c)
                goto LabelED9C;//ED98
            lda(0xF8);//ED9A
            LabelED9C:
            LabelE5BE();//ED9C
            pla();//ED9F
            c = false;//EDA0
            adc(0x80);//EDA1
            tax();//EDA3
            cmp(x, 0x20);//EDA4
            if (c)
                goto LabelEDAA;//EDA6
            lda(0xF8);//EDA8
            LabelEDAA:
            str(0x020C + y, a);//EDAA
            str(0x0210 + y, a);//EDAD
            str(0x0214 + y, a);//EDB0
            ldam(0x03D1);//EDB3
            pha();//EDB6
            and(0x08);//EDB7
            if (z)
                goto LabelEDC3;//EDB9
            lda(0xF8);//EDBB
            str(0x0200 + y, a);//EDBD
            str(0x020C + y, a);//EDC0
            LabelEDC3:
            pla();//EDC3
            pha();//EDC4
            and(0x04);//EDC5
            if (z)
                goto LabelEDD1;//EDC7
            lda(0xF8);//EDC9
            str(0x0204 + y, a);//EDCB
            str(0x0210 + y, a);//EDCE
            LabelEDD1:
            pla();//EDD1
            and(0x02);//EDD2
            if (z)
                goto LabelEDDE;//EDD4
            lda(0xF8);//EDD6
            str(0x0208 + y, a);//EDD8
            str(0x0214 + y, a);//EDDB
            LabelEDDE:
            ldxm(0x08);//EDDE
        }

        void LabelEDE1()
        {
            ldym(0xB5);//EDE1
            dey();//EDE3
            if (!z)
                return;//EDE4
            ldam(0x03D3);//EDE6
            and(0x08);//EDE9
            if (!z)
                return;//EDEB
            ldym(0x06EE + x);//EDED
            ldam(0x03B0);//EDF0
            str(0x0203 + y, a);//EDF3
            ldam(0x03BB);//EDF6
            str(0x0200 + y, a);//EDF9
            lda(0x74);//EDFC
            str(0x0201 + y, a);//EDFE
            lda(0x02);//EE01
            str(0x0202 + y, a);//EE03
        }

        void LabelEEE9()
        {
            ldam(0x079E);//EEE9
            if (z)
                goto LabelEEF3;//EEEC
            ldam(0x09);//EEEE
            lsr();//EEF0
            if (c)
                return;//EEF1
            LabelEEF3:
            ldam(0x0E);//EEF3
            cmp(a, 0x0B);//EEF5
            if (z)
            {
                LabelEF40();//EEF7
                return;
            }
            ldam(0x070B);//EEF9
            if (!z)
            {
                LabelEF3A();//EEFC
                return;
            }
            ldym(0x0704);//EEFE
            if (z)
            {
                LabelEF34();//EF01
                return;
            }
            ldam(0x1D);//EF03
            cmp(a, 0x00);//EF05
            if (z)
            {
                LabelEF34();//EF07
                return;
            }
            LabelEF34();//EF09
            ldam(0x09);//EF0C
            and(0x04);//EF0E
            if (!z)
                return;//EF10
            tax();//EF12
            ldym(0x06E4);//EF13
            ldam(0x33);//EF16
            lsr();//EF18
            if (c)
                goto LabelEF1F;//EF19
            iny();//EF1B
            iny();//EF1C
            iny();//EF1D
            iny();//EF1E
            LabelEF1F:
            ldam(0x0754);//EF1F
            if (z)
                goto LabelEF2D;//EF22
            ldam(0x0219 + y);//EF24
            cmp(a, ram[0xEEB5]);//EF27
            if (z)
                return;//EF2A
            inx();//EF2C
            LabelEF2D:
            ldam(0xEEE7 + x);//EF2D
            str(0x0219 + y, a);//EF30
        }

        void LabelEF34()
        {
            LabelEFEC();//EF34
            LabelEF45(); //EF37
            return;
        }

        void LabelEF3A()
        {
            LabelF0B0();//EF3A
            LabelEF45(); //EF3D
            return;
        }

        void LabelEF40()
        {
            ldy(0x0E);//EF40
            ldam(0xEE07 + y);//EF42
            LabelEF45();
        }

        void LabelEF45()
        {
            str(0x06D5, a);//EF45
            lda(0x04);//EF48
            LabelEFBE();//EF4A
            LabelF0E9();//EF4D
            ldam(0x0711);//EF50
            if (z)
                goto LabelEF7A;//EF53
            ldy(0x00);//EF55
            ldam(0x0781);//EF57
            cmp(a, ram[0x0711]);//EF5A
            str(0x0711, y);//EF5D
            if (c)
                goto LabelEF7A;//EF60
            str(0x0711, a);//EF62
            ldy(0x07);//EF65
            ldam(0xEE07 + y);//EF67
            str(0x06D5, a);//EF6A
            ldy(0x04);//EF6D
            ldam(0x57);//EF6F
            orm(0x0C);//EF71
            if (z)
                goto LabelEF76;//EF73
            dey();//EF75
            LabelEF76:
            tya();//EF76
            LabelEFBE();//EF77
            LabelEF7A:
            ldam(0x03D0);//EF7A
            lsr();//EF7D
            lsr();//EF7E
            lsr();//EF7F
            lsr();//EF80
            str(0x00, a);//EF81
            ldx(0x03);//EF83
            ldam(0x06E4);//EF85
            c = false;//EF88
            adc(0x18);//EF89
            tay();//EF8B
            LabelEF8C:
            lda(0xF8);//EF8C
            lsrm(0x00);//EF8E
            if (!c)
                goto LabelEF95;//EF90
            LabelE5C1();//EF92
            LabelEF95:
            tya();//EF95
            c = true;//EF96
            sbc(0x08);//EF97
            tay();//EF99
            dex();//EF9A
            if (!n)
                goto LabelEF8C;//EF9B
        }

        void LabelEFA4()
        {
            ldx(0x05);//EFA4
            LabelEFA6:
            ldam(0xEF9E + x);//EFA6
            str(0x02 + x, a);//EFA9
            dex();//EFAB
            if (!n)
                goto LabelEFA6;//EFAC
            ldx(0xB8);//EFAE
            ldy(0x04);//EFB0
            LabelEFDC();//EFB2
            ldam(0x0226);//EFB5
            or(0x40);//EFB8
            str(0x0222, a);//EFBA
        }

        void LabelEFBE()
        {
            str(0x07, a);//EFBE
            ldam(0x03AD);//EFC0
            str(0x0755, a);//EFC3
            str(0x05, a);//EFC6
            ldam(0x03B8);//EFC8
            str(0x02, a);//EFCB
            ldam(0x33);//EFCD
            str(0x03, a);//EFCF
            ldam(0x03C4);//EFD1
            str(0x04, a);//EFD4
            ldxm(0x06D5);//EFD6
            ldym(0x06E4);//EFD9
            LabelEFDC();
        }

        void LabelEFDC()
        {
            LabelEFDC:
            ldam(0xEE17 + x);//EFDC
            str(0x00, a);//EFDF
            ldam(0xEE18 + x);//EFE1
            LabelEBB2();//EFE4
            dec(0x07);//EFE7
            if (!z)
                goto LabelEFDC;//EFE9
        }

        void LabelEFEC()
        {
            ldam(0x1D);//EFEC
            cmp(a, 0x03);//EFEE
            if (z)
                goto LabelF044;//EFF0
            cmp(a, 0x02);//EFF2
            if (z)
                goto LabelF034;//EFF4
            cmp(a, 0x01);//EFF6
            if (!z)
                goto LabelF00B;//EFF8
            ldam(0x0704);//EFFA
            if (!z)
                goto LabelF050;//EFFD
            ldy(0x06);//EFFF
            ldam(0x0714);//F001
            if (!z)
                goto LabelF028;//F004
            ldy(0x00);//F006
            goto LabelF028; //F008
            return;
            LabelF00B:
            ldy(0x06);//F00B
            ldam(0x0714);//F00D
            if (!z)
                goto LabelF028;//F010
            ldy(0x02);//F012
            ldam(0x57);//F014
            orm(0x0C);//F016
            if (z)
                goto LabelF028;//F018
            ldam(0x0700);//F01A
            cmp(a, 0x09);//F01D
            if (!c)
                goto LabelF03C;//F01F
            ldam(0x45);//F021
            and(ram[0x33]);//F023
            if (!z)
                goto LabelF03C;//F025
            iny();//F027
            LabelF028:
            LabelF091();//F028
            LabelF02B:
            lda(0x00);//F02B
            str(0x070D, a);//F02D
            ldam(0xEE07 + y);//F030
            return;
            LabelF034:
            ldy(0x04);//F034
            LabelF091();//F036
            goto LabelF062; //F039
            LabelF03C:
            ldy(0x04);//F03C
            LabelF091();//F03E
            goto LabelF068; //F041
            LabelF044:
            ldy(0x05);//F044
            ldam(0x9F);//F046
            if (z)
                goto LabelF028;//F048
            LabelF091();//F04A
            goto LabelF06D; //F04D
            LabelF050:
            ldy(0x01);//F050
            LabelF091();//F052
            ldam(0x0782);//F055
            orm(0x070D);//F058
            if (!z)
                goto LabelF068;//F05B
            ldam(0x0A);//F05D
            asl();//F05F
            if (c)
                goto LabelF068;//F060
            LabelF062:
            ldam(0x070D);//F062
            LabelF0D0(); //F065
            return;
            LabelF068:
            lda(0x03);//F068
            goto LabelF06F; //F06A
            LabelF06D:
            lda(0x02);//F06D
            LabelF06F:
            str(0x00, a);//F06F
            LabelF062();//F071
            pha();//F074
            ldam(0x0781);//F075
            if (!z)
                goto LabelF08F;//F078
            ldam(0x070C);//F07A
            str(0x0781, a);//F07D
            ldam(0x070D);//F080
            c = false;//F083
            adc(0x01);//F084
            cmp(a, ram[0x00]);//F086
            if (!c)
                goto LabelF08C;//F088
            lda(0x00);//F08A
            LabelF08C:
            str(0x070D, a);//F08C
            LabelF08F:
            pla();//F08F
        }

        void LabelF062()
        {
            ldam(0x070D);//F062
            LabelF0D0(); //F065
            return;
        }

        void LabelF091()
        {
            ldam(0x0754);//F091
            if (z)
                return;//F094
            tya();//F096
            c = false;//F097
            adc(0x08);//F098
            tay();//F09A
        }

        void LabelF0B0()
        {
            ldym(0x070D);//F0B0
            ldam(0x09);//F0B3
            and(0x03);//F0B5
            if (!z)
                goto LabelF0C6;//F0B7
            iny();//F0B9
            cmp(y, 0x0A);//F0BA
            if (!c)
                goto LabelF0C3;//F0BC
            ldy(0x00);//F0BE
            str(0x070B, y);//F0C0
            LabelF0C3:
            str(0x070D, y);//F0C3
            LabelF0C6:
            ldam(0x0754);//F0C6
            if (!z)
            {
                LabelF0D7();//F0C9
                return;
            }
            ldam(0xF09C + y);//F0CB
            ldy(0x0F);//F0CE
            LabelF0D0();
        }

        void LabelF0D0()
        {
            asl();//F0D0
            asl();//F0D1
            asl();//F0D2
            adc(ram[0xEE07 + y]);//F0D3
        }

        void LabelF0D7()
        {
            tya();//F0D7
            c = false;//F0D8
            adc(0x0A);//F0D9
            tax();//F0DB
            ldy(0x09);//F0DC
            ldam(0xF09C + x);//F0DE
            if (!z)
                goto LabelF0E5;//F0E1
            ldy(0x01);//F0E3
            LabelF0E5:
            ldam(0xEE07 + y);//F0E5
        }

        void LabelF0E9()
        {
            ldym(0x06E4);//F0E9
            ldam(0x0E);//F0EC
            cmp(a, 0x0B);//F0EE
            if (z)
                goto LabelF105;//F0F0
            ldam(0x06D5);//F0F2
            cmp(a, 0x50);//F0F5
            if (z)
                goto LabelF117;//F0F7
            cmp(a, 0xB8);//F0F9
            if (z)
                goto LabelF117;//F0FB
            cmp(a, 0xC0);//F0FD
            if (z)
                goto LabelF117;//F0FF
            cmp(a, 0xC8);//F101
            if (!z)
                return;//F103
            LabelF105:
            ldam(0x0212 + y);//F105
            and(0x3F);//F108
            str(0x0212 + y, a);//F10A
            ldam(0x0216 + y);//F10D
            and(0x3F);//F110
            or(0x40);//F112
            str(0x0216 + y, a);//F114
            LabelF117:
            ldam(0x021A + y);//F117
            and(0x3F);//F11A
            str(0x021A + y, a);//F11C
            ldam(0x021E + y);//F11F
            and(0x3F);//F122
            or(0x40);//F124
            str(0x021E + y, a);//F126
        }

        void LabelF12A()
        {
            ldx(0x00);//F12A
            ldy(0x00);//F12C
            LabelF142(); //F12E
            return;
        }

        void LabelF131()
        {
            ldy(0x01);//F131
            LabelF1A8();//F133
            ldy(0x03);//F136
            LabelF142(); //F138
            return;
        }

        void LabelF13B()
        {
            ldy(0x00);//F13B
            LabelF1A8();//F13D
            ldy(0x02);//F140
            LabelF142();
        }

        void LabelF142()
        {
            LabelF171();//F142
            ldxm(0x08);//F145
        }

        void LabelF148()
        {
            ldy(0x02);//F148
            LabelF1A8();//F14A
            ldy(0x06);//F14D
            LabelF142(); //F14F
            return;
        }

        void LabelF152()
        {
            lda(0x01);//F152
            ldy(0x01);//F154
            LabelF165(); //F156
            return;
        }

        void LabelF159()
        {
            lda(0x09);//F159
            ldy(0x04);//F15B
            LabelF165();//F15D
            inx();//F160
            inx();//F161
            lda(0x09);//F162
            iny();//F164
            LabelF165();
        }

        void LabelF165()
        {
            str(0x00, x);//F165
            c = false;//F167
            adc(ram[0x00]);//F168
            tax();//F16A
            LabelF171();//F16B
            ldxm(0x08);//F16E//F170
        }

        void LabelF171()
        {
            ldam(0xCE + x);//F171
            str(0x03B8 + y, a);//F173
            ldam(0x86 + x);//F176
            c = true;//F178
            sbc(ram[0x071C]);//F179
            str(0x03AD + y, a);//F17C//F17F
        }

        void LabelF180()
        {
            ldx(0x00);//F180
            ldy(0x00);//F182
            LabelF1C0(); //F184
            return;//F184
        }

        void LabelF187()
        {
            ldy(0x00);//F187
            LabelF1A8();//F189
            ldy(0x02);//F18C
            LabelF1C0(); //F18E
            return;//F18E
        }

        void LabelF191()
        {
            ldy(0x01);//F191
            LabelF1A8();//F193
            ldy(0x03);//F196
            LabelF1C0(); //F198
            return;//F198
        }

        void LabelF19B()
        {
            ldy(0x02);//F19B
            LabelF1A8();//F19D
            ldy(0x06);//F1A0
            LabelF1C0(); //F1A2
            return;//F1A2
        }

        void LabelF1A8()
        {
            txa();//F1A8
            c = false;//F1A9
            adc(ram[0xF1A5 + y]);//F1AA
            tax();//F1AD//F1AE
        }

        void LabelF1AF()
        {
            lda(0x01);//F1AF
            ldy(0x01);//F1B1
            LabelF1BA(); //F1B3
            return;//F1B3
        }

        void LabelF1B6()
        {
            lda(0x09);//F1B6
            ldy(0x04);//F1B8
            LabelF1BA();
        }

        void LabelF1BA()
        {
            str(0x00, x);//F1BA
            c = false;//F1BC
            adc(ram[0x00]);//F1BD
            tax();//F1BF
            LabelF1C0();
        }

        void LabelF1C0()
        {
            tya();//F1C0
            pha();//F1C1
            LabelF1D7();//F1C2
            asl();//F1C5
            asl();//F1C6
            asl();//F1C7
            asl();//F1C8
            orm(0x00);//F1C9
            str(0x00, a);//F1CB
            pla();//F1CD
            tay();//F1CE
            ldam(0x00);//F1CF
            str(0x03D0 + y, a);//F1D1
            ldxm(0x08);//F1D4//F1D6
        }

        void LabelF1D7()
        {
            LabelF1F6();//F1D7
            lsr();//F1DA
            lsr();//F1DB
            lsr();//F1DC
            lsr();//F1DD
            str(0x00, a);//F1DE
            LabelF239(); //F1E0
            return;//F1E0
        }

        void LabelF1F6()
        {
            str(0x04, x);//F1F6
            ldy(0x01);//F1F8
            LabelF1FA:
            ldam(0x071C + y);//F1FA
            c = true;//F1FD
            sbc(ram[0x86 + x]);//F1FE
            str(0x07, a);//F200
            ldam(0x071A + y);//F202
            sbc(ram[0x6D + x]);//F205
            ldxm(0xF1F3 + y);//F207
            cmp(a, 0x00);//F20A
            if (n) //
                goto LabelF21E;//F20C
            ldxm(0xF1F4 + y);//F20E
            cmp(a, 0x01);//F211
            if (!n)
                goto LabelF21E;//F213
            lda(0x38);//F215
            str(0x06, a);//F217
            lda(0x08);//F219
            LabelF26D();//F21B
            LabelF21E:
            ldam(0xF1E3 + x);//F21E
            ldxm(0x04);//F221
            cmp(a, 0x00);//F223
            if (!z)
                return;//F225
            dey();//F227
            if (!n)
                goto LabelF1FA;//F228//F22A
        }

        void LabelF239()
        {
            str(0x04, x);//F239
            ldy(0x01);//F23B
            LabelF23D:
            ldam(0xF237 + y);//F23D
            c = true;//F240
            sbc(ram[0xCE + x]);//F241
            str(0x07, a);//F243
            lda(0x01);//F245
            sbc(ram[0xB5 + x]);//F247
            ldxm(0xF234 + y);//F249
            cmp(a, 0x00);//F24C
            if (n) //
                goto LabelF260;//F24E
            ldxm(0xF235 + y);//F250
            cmp(a, 0x01);//F253
            if (!n)
                goto LabelF260;//F255
            lda(0x20);//F257
            str(0x06, a);//F259
            lda(0x04);//F25B
            LabelF26D();//F25D
            LabelF260:
            ldam(0xF22B + x);//F260
            ldxm(0x04);//F263
            cmp(a, 0x00);//F265
            if (!z)
                return;//F267
            dey();//F269
            if (!n)
                goto LabelF23D;//F26A//F26C
        }

        void LabelF26D()
        {
            str(0x05, a);//F26D
            ldam(0x07);//F26F
            cmp(a, ram[0x06]);//F271
            if (c)
                return;//F273
            lsr();//F275
            lsr();//F276
            lsr();//F277
            and(0x07);//F278
            cmp(y, 0x01);//F27A
            if (c)
                goto LabelF280;//F27C
            adc(ram[0x05]);//F27E
            LabelF280:
            tax();//F280//F281
        }

        void LabelF282()
        {
            int p = 0;
            if (y == 0x69)
                p = 1;

            ldam(0x03);//F282
            lsr();//F284
            lsr();//F285
            ldam(0x00);//F286
            if (!c)
                goto LabelF296;//F288
            str(0x0205 + y, a);//F28A
            ldam(0x01);//F28D
            str(0x0201 + y, a);//F28F
            lda(0x40);//F292
            if (!z)
                goto LabelF2A0;//F294
            LabelF296:
            str(0x0201 + y, a);//F296
            ldam(0x01);//F299
            str(0x0205 + y, a);//F29B
            lda(0x00);//F29E
            LabelF2A0:
            orm(0x04);//F2A0
            str(0x0202 + y, a);//F2A2
            str(0x0206 + y, a);//F2A5
            ldam(0x02);//F2A8
            str(0x0200 + y, a);//F2AA
            str(0x0204 + y, a);//F2AD
            ldam(0x05);//F2B0
            str(0x0203 + y, a);//F2B2
            c = false;//F2B5
            adc(0x08);//F2B6
            str(0x0207 + y, a);//F2B8
            ldam(0x02);//F2BB
            c = false;//F2BD
            adc(0x08);//F2BE
            str(0x02, a);//F2C0
            tya();//F2C2
            c = false;//F2C3
            adc(0x08);//F2C4
            tay();//F2C6
            inx();//F2C7
            inx();//F2C8//F2C9
        }

        void LabelF2D0()
        {
            ldam(0x0770);//F2D0
            if (!z)
                goto LabelF2D9;//F2D3
            str(0x4015, a);//F2D5
            return;
            LabelF2D9:
            lda(0xFF);//F2D9
            str(0x4017, a);//F2DB
            lda(0x0F);//F2DE
            str(0x4015, a);//F2E0
            ldam(0x07C6);//F2E3
            if (!z)
                goto LabelF2EE;//F2E6
            ldam(0xFA);//F2E8
            cmp(a, 0x01);//F2EA
            if (!z)
                goto LabelF34B;//F2EC
            LabelF2EE:
            ldam(0x07B2);//F2EE
            if (!z)
                goto LabelF316;//F2F1
            ldam(0xFA);//F2F3
            if (z)
                goto LabelF35D;//F2F5
            str(0x07B2, a);//F2F7
            str(0x07C6, a);//F2FA
            lda(0x00);//F2FD
            str(0x4015, a);//F2FF
            str(0xF1, a);//F302
            str(0xF2, a);//F304
            str(0xF3, a);//F306
            lda(0x0F);//F308
            str(0x4015, a);//F30A
            lda(0x2A);//F30D
            str(0x07BB, a);//F30F
            LabelF312:
            lda(0x44);//F312
            if (!z)
                goto LabelF327;//F314
            LabelF316:
            ldam(0x07BB);//F316
            cmp(a, 0x24);//F319
            if (z)
                goto LabelF325;//F31B
            cmp(a, 0x1E);//F31D
            if (z)
                goto LabelF312;//F31F
            cmp(a, 0x18);//F321
            if (!z)
                goto LabelF32E;//F323
            LabelF325:
            lda(0x64);//F325
            LabelF327:
            ldx(0x84);//F327
            ldy(0x7F);//F329
            LabelF388();//F32B
            LabelF32E:
            dec(0x07BB);//F32E
            if (!z)
                goto LabelF35D;//F331
            lda(0x00);//F333
            str(0x4015, a);//F335
            ldam(0x07B2);//F338
            cmp(a, 0x02);//F33B
            if (!z)
                goto LabelF344;//F33D
            lda(0x00);//F33F
            str(0x07C6, a);//F341
            LabelF344:
            lda(0x00);//F344
            str(0x07B2, a);//F346
            if (z)
                goto LabelF35D;//F349
            LabelF34B:
            LabelF41B();//F34B
            LabelF57C();//F34E
            LabelF667();//F351
            LabelF694();//F354
            lda(0x00);//F357
            str(0xFB, a);//F359
            str(0xFC, a);//F35B
            LabelF35D:
            lda(0x00);//F35D
            str(0xFF, a);//F35F
            str(0xFE, a);//F361
            str(0xFD, a);//F363
            str(0xFA, a);//F365
            ldym(0x07C0);//F367
            ldam(0xF4);//F36A
            and(0x03);//F36C
            if (z)
                goto LabelF377;//F36E
            inc(0x07C0);//F370
            cmp(y, 0x30);//F373
            if (!c)
                goto LabelF37D;//F375
            LabelF377:
            tya();//F377
            if (z)
                goto LabelF37D;//F378
            dec(0x07C0);//F37A
            LabelF37D:
            str(0x4011, y);//F37D
        }

        void LabelF381()
        {
            str(0x4001, y);//F381
            str(0x4000, x);//F384
        }

        void LabelF388()
        {
            LabelF381();//F388
            LabelF38B();
        }

        void LabelF38B()
        {
            ldx(0x00);//F38B
            LabelF38D();
        }

        void LabelF38D()
        {
            tay();//F38D
            ldam(0xFF01 + y);//F38E
            if (z)
                return;//F391
            str(0x4002 + x, a);//F393
            ldam(0xFF00 + y);//F396
            or(0x08);//F399
            str(0x4003 + x, a);//F39B
        }

        void LabelF39F()
        {
            str(0x4004, x);//F39F
            str(0x4005, y);//F3A2
        }

        void LabelF3A6()
        {
            LabelF39F();//F3A6
            LabelF3A9();
        }

        void LabelF3A9()
        {
            ldx(0x04);//F3A9
            if (!z)
                LabelF38D();//F3AB
            LabelF3AD();
        }

        void LabelF3AD()
        {
            ldx(0x08);//F3AD
            if (!z)
                LabelF38D();//F3AF
        }

        void LabelF3BF()
        {
            lda(0x40);//F3BF
            str(0x07BB, a);//F3C1
            lda(0x62);//F3C4
            LabelF38B();//F3C6
            ldx(0x99);//F3C9
            if (!z)
                LabelF3F2();//F3CB
        }

        void LabelF3CD()
        {
            lda(0x26);//F3CD
            if (!z)
                LabelF3D3();//F3CF
        }

        void LabelF3D1()
        {
            lda(0x18);//F3D1
            LabelF3D3();
        }

        void LabelF3D3()
        {
            ldx(0x82);//F3D3
            ldy(0xA7);//F3D5
            LabelF388();//F3D7
            lda(0x28);//F3DA
            str(0x07BB, a);//F3DC
            LabelF3DF();
        }

        void LabelF3DF()
        {
            ldam(0x07BB);//F3DF
            cmp(a, 0x25);//F3E2
            if (!z)
                goto LabelF3EC;//F3E4
            ldx(0x5F);//F3E6
            ldy(0xF6);//F3E8
            if (!z)
                goto LabelF3F4;//F3EA
            LabelF3EC:
            cmp(a, 0x20);//F3EC
            if (!z)
            {
                LabelF419();//F3EE
                return;
            }
            ldx(0x48);//F3F0
            ldy(0xBC);//F3F2
            LabelF3F4:
            LabelF381();//F3F4
            if (!z)
            {
                LabelF419();//F3F7
                return;
            }
            LabelF3F9();
        }

        void LabelF3F2()
        {
            ldy(0xBC);//F3F2
            LabelF381();//F3F4
            if (!z)
                LabelF419();//F3F7
            LabelF3F9();
        }

        void LabelF3F9()
        {
            lda(0x05);//F3F9
            ldy(0x99);//F3FB
            if (!z)
                LabelF403();//F3FD
            LabelF3FF();
        }

        void LabelF3FF()
        {
            lda(0x0A);//F3FF
            ldy(0x93);//F401
            LabelF403();
        }

        void LabelF403()
        {
            ldx(0x9E);//F403
            str(0x07BB, a);//F405
            lda(0x0C);//F408
            LabelF388();//F40A
            LabelF40D();
        }

        void LabelF40D()
        {
            ldam(0x07BB);//F40D
            cmp(a, 0x06);//F410
            if (!z)
                return;//F412
            lda(0xBB);//F414
            str(0x4001, a);//F416
            if (!z)
                LabelF47B();//F419
        }

        void LabelF419()
        {
            if (!z)
            {
                LabelF47B();//F419
                return;
            }
            LabelF41B();
        }

        void LabelF41B()
        {
            ldym(0xFF);//F41B
            if (z)
                goto LabelF43F;//F41D
            str(0xF1, y);//F41F
            if (n) //
            {
                LabelF3CD();//F421
                return;
            }

            lsrm(0xFF);//F423
            if (c)
            {
                LabelF3D1();//F425
                return;
            }

            lsrm(0xFF);//F427
            if (c)
            {
                LabelF3FF();//F429
                return;
            }

            lsrm(0xFF);//F42B
            if (c)
                goto LabelF45B;//F42D

            lsrm(0xFF);//F42F
            if (c)
            {
                LabelF47D();//F431
                return;
            }
            lsrm(0xFF);//F433
            if (c)
            {
                LabelF4B6();//F435
                return;
            }
            lsrm(0xFF);//F437
            if (c)
            {
                LabelF3F9();//F439
                return;
            }
            lsrm(0xFF);//F43B
            if (c)
            {
                LabelF3BF();//F43D
                return;
            }
            LabelF43F:
            ldam(0xF1);//F43F
            if (z)
                return;//F441
            if (n) //
                LabelF3DF();//F443
            lsr();//F445
            if (c)
                LabelF3DF();//F446
            lsr();//F448
            if (c)
                LabelF40D();//F449
            lsr();//F44B
            if (c)
                goto LabelF469;//F44C
            lsr();//F44E
            if (c)
            {
                LabelF48D();//F44F
                return;
            }

            lsr();//F451
            if (c)
            {
                LabelF4BB();//F452
                return;
            }

            lsr();//F454
            if (c)
                LabelF40D();//F455
            lsr();//F457
            if (c)
            {
                LabelF4A2();//F458
                return;
            }
            LabelF45B:
            lda(0x0E);//F45B
            str(0x07BB, a);//F45D
            ldy(0x9C);//F460
            ldx(0x9E);//F462
            lda(0x26);//F464
            LabelF388();//F466
            LabelF469:
            ldym(0x07BB);//F469
            ldam(0xF3B0 + y);//F46C
            str(0x4000, a);//F46F
            cmp(y, 0x06);//F472
            if (!z)
            {
                LabelF47B();//F474
                return;
            }
            lda(0x9E);//F476
            str(0x4002, a);//F478
            LabelF47B();
        }

        void LabelF47B()
        {
            if (!z)
                LabelF4A2();//F47B
        }

        void LabelF47D()
        {
            lda(0x0E);//F47D
            ldy(0xCB);//F47F
            ldx(0x9F);//F481
            str(0x07BB, a);//F483
            lda(0x28);//F486
            LabelF388();//F488
            if (!z)
                LabelF4A2();//F48B
        }

        void LabelF48D()
        {
            ldym(0x07BB);//F48D
            cmp(y, 0x08);//F490
            if (!z)
                goto LabelF49D;//F492
            lda(0xA0);//F494
            str(0x4002, a);//F496
            lda(0x9F);//F499
            if (!z)
                goto LabelF49F;//F49B
            LabelF49D:
            lda(0x90);//F49D
            LabelF49F:
            str(0x4000, a);//F49F
            LabelF4A2();
        }

        void LabelF4A2()
        {
            dec(0x07BB);//F4A2
            if (!z)
                return;//F4A5
            LabelF4A7();
        }

        void LabelF4A7()
        {
            ldx(0x00);//F4A7
            str(0xF1, x);//F4A9
            ldx(0x0E);//F4AB
            str(0x4015, x);//F4AD
            ldx(0x0F);//F4B0
            str(0x4015, x);//F4B2
        }

        void LabelF4B6()
        {
            lda(0x2F);//F4B6
            str(0x07BB, a);//F4B8
            LabelF4BB();
        }

        void LabelF4BB()
        {
            ldam(0x07BB);//F4BB
            lsr();//F4BE
            if (c)
                goto LabelF4D1;//F4BF
            lsr();//F4C1
            if (c)
                goto LabelF4D1;//F4C2
            and(0x02);//F4C4
            if (z)
                goto LabelF4D1;//F4C6
            ldy(0x91);//F4C8
            ldx(0x9A);//F4CA
            lda(0x44);//F4CC
            LabelF388();//F4CE
            LabelF4D1:
            LabelF4A2(); //F4D1
            return;
        }

        void LabelF518()
        {
            lda(0x35);//F518
            ldx(0x8D);//F51A
            if (!z)
                LabelF522();//F51C
            LabelF51E();
        }

        void LabelF51E()
        {
            lda(0x06);//F51E
            ldx(0x98);//F520
            LabelF522();
        }

        void LabelF522()
        {
            str(0x07BD, a);//F522
            ldy(0x7F);//F525
            lda(0x42);//F527
            LabelF3A6();//F529
            LabelF52C();
        }

        void LabelF52C()
        {
            ldam(0x07BD);//F52C
            cmp(a, 0x30);//F52F
            if (!z)
                goto LabelF538;//F531
            lda(0x54);//F533
            str(0x4006, a);//F535
            LabelF538:
            if (!z)
                LabelF568();//F538
            LabelF53A();
        }

        void LabelF53A()
        {
            lda(0x20);//F53A
            str(0x07BD, a);//F53C
            ldy(0x94);//F53F
            lda(0x5E);//F541
            if (!z)
                LabelF550();//F543
            LabelF545();
        }

        void LabelF545()
        {
            ldam(0x07BD);//F545
            cmp(a, 0x18);//F548
            if (!z)
            {
                LabelF568();//F54A
                return;
            }
            ldy(0x93);//F54C
            lda(0x18);//F54E
            LabelF550();
        }

        void LabelF550()
        {
            if (!z)
                LabelF5D1();//F550
            LabelF552();
        }

        void LabelF552()
        {
            lda(0x36);//F552
            str(0x07BD, a);//F554
            LabelF557();
        }

        void LabelF557()
        {
            ldam(0x07BD);//F557
            lsr();//F55A
            if (c)
            {
                LabelF568();//F55B
                return;
            }
            tay();//F55D
            ldam(0xF4D9 + y);//F55E
            ldx(0x5D);//F561
            ldy(0x7F);//F563
            LabelF565();
        }

        void LabelF565()
        {
            LabelF3A6();//F565
            LabelF568();
        }

        void LabelF568()
        {
            dec(0x07BD);//F568
            if (!z)
                return;//F56B
            LabelF56D();
        }

        void LabelF56D()
        {
            ldx(0x00);//F56D
            str(0xF2, x);//F56F
            LabelF571();
        }


        void LabelF571()
        {
            ldx(0x0D);//F571
            str(0x4015, x);//F573
            ldx(0x0F);//F576
            str(0x4015, x);//F578
        }

        void LabelF57C()
        {
            ldam(0xF2);//F57C
            and(0x40);//F57E
            if (!z)
                goto LabelF5E7;//F580
            ldym(0xFE);//F582
            if (z)
                goto LabelF5A6;//F584
            str(0xF2, y);//F586
            if (n) //
                goto LabelF5C8;//F588
            lsrm(0xFE);//F58A
            if (c)
            {
                LabelF518();//F58C
                return;
            }
            lsrm(0xFE);//F58E
            if (c)
                goto LabelF5FC;//F590
            lsrm(0xFE);//F592
            if (c)
                goto LabelF600;//F594
            lsrm(0xFE);//F596
            if (c)
            {
                LabelF53A();//F598
                return;
            }
            lsrm(0xFE);//F59A
            if (c)
            {
                LabelF51E();//F59C
                return;
            }
            lsrm(0xFE);//F59E
            if (c)
            {
                LabelF552();//F5A0
                return;
            }
            lsrm(0xFE);//F5A2
            if (c)
                goto LabelF5E2;//F5A4
            LabelF5A6:
            ldam(0xF2);//F5A6
            if (z)
                return;//F5A8
            if (n) //
                goto LabelF5D3;//F5AA
            lsr();//F5AC
            if (c)
                goto LabelF5C2;//F5AD
            lsr();//F5AF
            if (c)
                goto LabelF60F;//F5B0
            lsr();//F5B2
            if (c)
                goto LabelF60F;//F5B3
            lsr();//F5B5
            if (c)
            {
                LabelF545();//F5B6
                return;
            }
            lsr();//F5B8
            if (c)
                goto LabelF5C2;//F5B9
            lsr();//F5BB
            if (c)
            {
                LabelF557();//F5BC
                return;
            }
            lsr();//F5BE
            if (c)
                goto LabelF5E7;//F5BF
            return;
            LabelF5C2:
            LabelF52C(); //F5C2
            return;
            LabelF5C5:
            LabelF568(); //F5C5
            return;
            LabelF5C8:
            lda(0x38);//F5C8
            str(0x07BD, a);//F5CA
            ldy(0xC4);//F5CD
            lda(0x18);//F5CF
            if (!z)
                goto LabelF5DE;//F5D1
            LabelF5D3:
            ldam(0x07BD);//F5D3
            cmp(a, 0x08);//F5D6
            if (!z)
            {
                LabelF568();//F5D8
                return;
            }
            ldy(0xA4);//F5DA
            lda(0x5A);//F5DC
            LabelF5DE:
            ldx(0x9F);//F5DE
            LabelF5E0:
            if (!z)
            {
                LabelF565();//F5E0
                return;
            }
            LabelF5E2:
            lda(0x30);//F5E2
            str(0x07BD, a);//F5E4
            LabelF5E7:
            ldam(0x07BD);//F5E7
            ldx(0x03);//F5EA
            LabelF5EC:
            lsr();//F5EC
            if (c)
                goto LabelF5C5;//F5ED
            dex();//F5EF
            if (!z)
                goto LabelF5EC;//F5F0
            tay();//F5F2
            ldam(0xF4D3 + y);//F5F3
            ldx(0x82);//F5F6
            ldy(0x7F);//F5F8
            if (!z)
                goto LabelF5E0;//F5FA
            LabelF5FC:
            lda(0x10);//F5FC
            if (!z)
                goto LabelF602;//F5FE
            LabelF600:
            lda(0x20);//F600
            LabelF602:
            str(0x07BD, a);//F602
            lda(0x7F);//F605
            str(0x4005, a);//F607
            lda(0x00);//F60A
            str(0x07BE, a);//F60C
            LabelF60F:
            inc(0x07BE);//F60F
            ldam(0x07BE);//F612
            lsr();//F615
            tay();//F616
            cmp(y, ram[0x07BD]);//F617
            if (z)
                goto LabelF628;//F61A
            lda(0x9D);//F61C
            str(0x4004, a);//F61E
            ldam(0xF4F8 + y);//F621
            LabelF3A9();//F624
            LabelF628:
            LabelF56D(); //F628
            return;
        }

        void LabelF5D1()
        {
            if (!z)
                LabelF5DE();//F5D1
        }

        void LabelF5DE()
        {
            ldx(0x9F);//F5DE
            if (!z)
            {
                LabelF565();//F5E0
                return;
            }
        }

        void LabelF63B()
        {
            lda(0x20);//F63B
            str(0x07BF, a);//F63D
            LabelF640();
        }

        void LabelF640()
        {
            ldam(0x07BF);//F640
            lsr();//F643
            if (!c)
            {
                LabelF658();//F644
                return;
            }
            tay();//F646
            ldxm(0xF62B + y);//F647
            ldam(0xFFEA + y);//F64A
            LabelF64D();
        }

        void LabelF64D()
        {
            str(0x400C, a);//F64D
            str(0x400E, x);//F650
            lda(0x18);//F653
            str(0x400F, a);//F655
            LabelF658();
        }

        void LabelF658()
        {
            dec(0x07BF);//F658
            if (!z)
                return;//F65B
            lda(0xF0);//F65D
            str(0x400C, a);//F65F
            lda(0x00);//F662
            str(0xF3, a);//F664
        }

        void LabelF667()
        {
            ldym(0xFD);//F667
            if (z)
                goto LabelF675;//F669
            str(0xF3, y);//F66B
            lsrm(0xFD);//F66D
            if (c)
            {
                LabelF63B();//F66F
                return;
            }
            lsrm(0xFD);//F671
            if (c)
                goto LabelF680;//F673
            LabelF675:
            ldam(0xF3);//F675
            if (z)
                return;//F677
            lsr();//F679
            if (c)
            {
                LabelF640();//F67A
                return;
            }
            lsr();//F67C
            if (c)
                goto LabelF685;//F67D
            LabelF680:
            lda(0x40);//F680
            str(0x07BF, a);//F682
            LabelF685:
            ldam(0x07BF);//F685
            lsr();//F688
            tay();//F689
            ldx(0x0F);//F68A
            ldam(0xFFC9 + y);//F68C
            if (!z)
            {
                LabelF64D();//F68F
                return;
            }
            LabelF691();
            return;
        }

        void LabelF691()
        {
            LabelF73A(); //F691
        }

        void LabelF694()
        {
            ldam(0xFC);//F694
            if (!z)
                goto LabelF6A4;//F696
            ldam(0xFB);//F698
            if (!z)
                goto LabelF6C8;//F69A
            ldam(0x07B1);//F69C
            orm(0xF4);//F69F
            if (!z)
            {
                LabelF691();//F6A1
                return;
            }
            return;
            LabelF6A4:
            str(0x07B1, a);//F6A4
            cmp(a, 0x01);//F6A7
            if (!z)
                goto LabelF6B1;//F6A9
            LabelF4A7();//F6AB
            LabelF571();//F6AE
            LabelF6B1:
            ldxm(0xF4);//F6B1
            str(0x07C5, x);//F6B3
            ldy(0x00);//F6B6
            str(0x07C4, y);//F6B8
            str(0xF4, y);//F6BB
            cmp(a, 0x40);//F6BD
            if (!z)
                goto LabelF6F1;//F6BF
            ldx(0x08);//F6C1
            str(0x07C4, x);//F6C3
            if (!z)
                goto LabelF6F1;//F6C6
            LabelF6C8:
            cmp(a, 0x04);//F6C8
            if (!z)
                goto LabelF6CF;//F6CA
            LabelF4A7();//F6CC
            LabelF6CF:
            ldy(0x10);//F6CF
            LabelF6D1:
            str(0x07C7, y);//F6D1
            ldy(0x00);//F6D4
            str(0x07B1, y);//F6D6
            str(0xF4, a);//F6D9
            cmp(a, 0x01);//F6DB
            if (!z)
                goto LabelF6ED;//F6DD
            inc(0x07C7);//F6DF
            ldym(0x07C7);//F6E2
            cmp(y, 0x32);//F6E5
            if (!z)
                goto LabelF6F5;//F6E7
            ldy(0x11);//F6E9
            if (!z)
                goto LabelF6D1;//F6EB
            LabelF6ED:
            ldy(0x08);//F6ED
            str(0xF7, y);//F6EF
            LabelF6F1:
            iny();//F6F1
            lsr();//F6F2
            if (!c)
                goto LabelF6F1;//F6F3
            LabelF6F5:
            ldam(0xF90C + y);//F6F5
            tay();//F6F8
            ldam(0xF90D + y);//F6F9
            str(0xF0, a);//F6FC
            ldam(0xF90E + y);//F6FE
            str(0xF5, a);//F701
            ldam(0xF90F + y);//F703
            str(0xF6, a);//F706
            ldam(0xF910 + y);//F708
            str(0xF9, a);//F70B
            ldam(0xF911 + y);//F70D
            str(0xF8, a);//F710
            ldam(0xF912 + y);//F712
            str(0x07B0, a);//F715
            str(0x07C1, a);//F718
            lda(0x01);//F71B
            str(0x07B4, a);//F71D
            str(0x07B6, a);//F720
            str(0x07B9, a);//F723
            str(0x07BA, a);//F726
            lda(0x00);//F729
            str(0xF7, a);//F72B
            str(0x07CA, a);//F72D
            lda(0x0B);//F730
            str(0x4015, a);//F732
            lda(0x0F);//F735
            str(0x4015, a);//F737
            LabelF73A();
        }

        void LabelF6A4()
        {
            str(0x07B1, a);//F6A4
            cmp(a, 0x01);//F6A7
            if (!z)
                goto LabelF6B1;//F6A9
            LabelF4A7();//F6AB
            LabelF571();//F6AE
            LabelF6B1:
            ldxm(0xF4);//F6B1
            str(0x07C5, x);//F6B3
            ldy(0x00);//F6B6
            str(0x07C4, y);//F6B8
            str(0xF4, y);//F6BB
            cmp(a, 0x40);//F6BD
            if (!z)
                goto LabelF6F1;//F6BF
            ldx(0x08);//F6C1
            str(0x07C4, x);//F6C3
            if (!z)
                goto LabelF6F1;//F6C6
            LabelF6C8:
            cmp(a, 0x04);//F6C8
            if (!z)
                goto LabelF6CF;//F6CA
            LabelF4A7();//F6CC
            LabelF6CF:
            ldy(0x10);//F6CF
            LabelF6D1:
            str(0x07C7, y);//F6D1
            ldy(0x00);//F6D4
            str(0x07B1, y);//F6D6
            str(0xF4, a);//F6D9
            cmp(a, 0x01);//F6DB
            if (!z)
                goto LabelF6ED;//F6DD
            inc(0x07C7);//F6DF
            ldym(0x07C7);//F6E2
            cmp(y, 0x32);//F6E5
            if (!z)
                goto LabelF6F5;//F6E7
            ldy(0x11);//F6E9
            if (!z)
                goto LabelF6D1;//F6EB
            LabelF6ED:
            ldy(0x08);//F6ED
            str(0xF7, y);//F6EF
            LabelF6F1:
            iny();//F6F1
            lsr();//F6F2
            if (!c)
                goto LabelF6F1;//F6F3
            LabelF6F5:
            ldam(0xF90C + y);//F6F5
            tay();//F6F8
            ldam(0xF90D + y);//F6F9
            str(0xF0, a);//F6FC
            ldam(0xF90E + y);//F6FE
            str(0xF5, a);//F701
            ldam(0xF90F + y);//F703
            str(0xF6, a);//F706
            ldam(0xF910 + y);//F708
            str(0xF9, a);//F70B
            ldam(0xF911 + y);//F70D
            str(0xF8, a);//F710
            ldam(0xF912 + y);//F712
            str(0x07B0, a);//F715
            str(0x07C1, a);//F718
            lda(0x01);//F71B
            str(0x07B4, a);//F71D
            str(0x07B6, a);//F720
            str(0x07B9, a);//F723
            str(0x07BA, a);//F726
            lda(0x00);//F729
            str(0xF7, a);//F72B
            str(0x07CA, a);//F72D
            lda(0x0B);//F730
            str(0x4015, a);//F732
            lda(0x0F);//F735
            str(0x4015, a);//F737
            LabelF73A();
        }

        void LabelF6D4()
        {
            goto LabelF6D4;
            LabelF6D1:
            str(0x07C7, y);//F6D1
            LabelF6D4:
            ldy(0x00);//F6D4
            str(0x07B1, y);//F6D6
            str(0xF4, a);//F6D9
            cmp(a, 0x01);//F6DB
            if (!z)
                goto LabelF6ED;//F6DD
            inc(0x07C7);//F6DF
            ldym(0x07C7);//F6E2
            cmp(y, 0x32);//F6E5
            if (!z)
                goto LabelF6F5;//F6E7
            ldy(0x11);//F6E9
            if (!z)
                goto LabelF6D1;//F6EB
            LabelF6ED:
            ldy(0x08);//F6ED
            str(0xF7, y);//F6EF
            LabelF6F1:
            iny();//F6F1
            lsr();//F6F2
            if (!c)
                goto LabelF6F1;//F6F3
            LabelF6F5:
            ldam(0xF90C + y);//F6F5
            tay();//F6F8
            ldam(0xF90D + y);//F6F9
            str(0xF0, a);//F6FC
            ldam(0xF90E + y);//F6FE
            str(0xF5, a);//F701
            ldam(0xF90F + y);//F703
            str(0xF6, a);//F706
            ldam(0xF910 + y);//F708
            str(0xF9, a);//F70B
            ldam(0xF911 + y);//F70D
            str(0xF8, a);//F710
            ldam(0xF912 + y);//F712
            str(0x07B0, a);//F715
            str(0x07C1, a);//F718
            lda(0x01);//F71B
            str(0x07B4, a);//F71D
            str(0x07B6, a);//F720
            str(0x07B9, a);//F723
            str(0x07BA, a);//F726
            lda(0x00);//F729
            str(0xF7, a);//F72B
            str(0x07CA, a);//F72D
            lda(0x0B);//F730
            str(0x4015, a);//F732
            lda(0x0F);//F735
            str(0x4015, a);//F737
            LabelF73A();
        }

        void LabelF73A()
        {
            dec(0x07B4);//F73A
            if (!z)
                goto LabelF79E;//F73D
            ldym(0xF7);//F73F
            inc(0xF7);//F741
            ldam(W(0xF5) + y);//F743
            if (z)
                goto LabelF74B;//F745
            if (!n)
                goto LabelF786;//F747
            if (!z)
                goto LabelF77A;//F749
            LabelF74B:
            ldam(0x07B1);//F74B
            cmp(a, 0x40);//F74E
            if (!z)
                goto LabelF757;//F750
            ldam(0x07C5);//F752
            if (!z)
                goto LabelF774;//F755
            LabelF757:
            and(0x04);//F757
            if (!z)
                goto LabelF777;//F759
            ldam(0xF4);//F75B
            and(0x5F);//F75D
            if (!z)
                goto LabelF774;//F75F
            lda(0x00);//F761
            str(0xF4, a);//F763
            str(0x07B1, a);//F765
            str(0x4008, a);//F768
            lda(0x90);//F76B
            str(0x4000, a);//F76D
            str(0x4004, a);//F770
            return;
            LabelF774:
            LabelF6D4(); //F774
            return;
            LabelF777:
            LabelF6A4(); //F777
            return;
            LabelF77A:
            LabelF8CB();//F77A
            str(0x07B3, a);//F77D
            ldym(0xF7);//F780
            inc(0xF7);//F782
            ldam(W(0xF5) + y);//F784
            LabelF786:
            ldxm(0xF2);//F786
            if (!z)
                goto LabelF798;//F788
            LabelF78A:
            LabelF3A9();//F78A
            if (z)
                goto LabelF792;//F78D
            LabelF8D8();//F78F
            LabelF792:
            str(0x07B5, a);//F792
            LabelF39F();//F795
            LabelF798:
            ldam(0x07B3);//F798
            str(0x07B4, a);//F79B
            LabelF79E:
            ldam(0xF2);//F79E
            if (!z)
                goto LabelF7BC;//F7A0
            ldam(0x07B1);//F7A2
            and(0x91);//F7A5
            if (!z)
                goto LabelF7BC;//F7A7
            ldym(0x07B5);//F7A9
            if (z)
                goto LabelF7B1;//F7AC
            dec(0x07B5);//F7AE
            LabelF7B1:
            LabelF8F4();//F7B1
            str(0x4004, a);//F7B4
            ldx(0x7F);//F7B7
            str(0x4005, x);//F7B9
            LabelF7BC:
            ldym(0xF8);//F7BC
            if (z)
                goto LabelF81A;//F7BE
            dec(0x07B6);//F7C0
            if (!z)
                goto LabelF7F7;//F7C3
            LabelF7C5:
            ldym(0xF8);//F7C5
            inc(0xF8);//F7C7
            ldam(W(0xF5) + y);//F7C9
            if (!z)
                goto LabelF7DC;//F7CB
            lda(0x83);//F7CD
            str(0x4000, a);//F7CF
            lda(0x94);//F7D2
            str(0x4001, a);//F7D4
            str(0x07CA, a);//F7D7
            if (!z)
                goto LabelF7C5;//F7DA
            LabelF7DC:
            LabelF8C5();//F7DC
            str(0x07B6, a);//F7DF
            ldym(0xF1);//F7E2
            if (!z)
                goto LabelF81A;//F7E4
            txa();//F7E6
            and(0x3E);//F7E7
            LabelF38B();//F7E9
            if (z)
                goto LabelF7F1;//F7EC
            LabelF8D8();//F7EE
            LabelF7F1:
            str(0x07B7, a);//F7F1
            LabelF381();//F7F4
            LabelF7F7:
            ldam(0xF1);//F7F7
            if (!z)
                goto LabelF81A;//F7F9
            ldam(0x07B1);//F7FB
            and(0x91);//F7FE
            if (!z)
                goto LabelF810;//F800
            ldym(0x07B7);//F802
            if (z)
                goto LabelF80A;//F805
            dec(0x07B7);//F807
            LabelF80A:
            LabelF8F4();//F80A
            str(0x4000, a);//F80D
            LabelF810:
            ldam(0x07CA);//F810
            if (!z)
                goto LabelF817;//F813
            lda(0x7F);//F815
            LabelF817:
            str(0x4001, a);//F817
            LabelF81A:
            ldam(0xF9);//F81A
            dec(0x07B9);//F81C
            if (!z)
                goto LabelF86D;//F81F
            ldym(0xF9);//F821
            inc(0xF9);//F823
            ldam(W(0xF5) + y);//F825
            if (z)
                goto LabelF86A;//F827
            if (!n)
                goto LabelF83E;//F829
            LabelF8CB();//F82B
            str(0x07B8, a);//F82E
            lda(0x1F);//F831
            str(0x4008, a);//F833
            ldym(0xF9);//F836
            inc(0xF9);//F838
            ldam(W(0xF5) + y);//F83A
            if (z)
                goto LabelF86A;//F83C
            LabelF83E:
            LabelF3AD();//F83E
            ldxm(0x07B8);//F841
            str(0x07B9, x);//F844
            ldam(0x07B1);//F847
            and(0x6E);//F84A
            if (!z)
                goto LabelF854;//F84C
            ldam(0xF4);//F84E
            and(0x0A);//F850
            if (z)
                goto LabelF86D;//F852
            LabelF854:
            txa();//F854
            cmp(a, 0x12);//F855
            if (c)
                goto LabelF868;//F857
            ldam(0x07B1);//F859
            and(0x08);//F85C
            if (z)
                goto LabelF864;//F85E
            lda(0x0F);//F860
            if (!z)
                goto LabelF86A;//F862
            LabelF864:
            lda(0x1F);//F864
            if (!z)
                goto LabelF86A;//F866
            LabelF868:
            lda(0xFF);//F868
            LabelF86A:
            str(0x4008, a);//F86A
            LabelF86D:
            ldam(0xF4);//F86D
            and(0xF3);//F86F
            if (z)
                return;//F871
            dec(0x07BA);//F873
            if (!z)
                return;//F876
            LabelF878:
            ldym(0x07B0);//F878
            inc(0x07B0);//F87B
            ldam(W(0xF5) + y);//F87E
            if (!z)
                goto LabelF88A;//F880
            ldam(0x07C1);//F882
            str(0x07B0, a);//F885
            if (!z)
                goto LabelF878;//F888
            LabelF88A:
            LabelF8C5();//F88A
            str(0x07BA, a);//F88D
            txa();//F890
            and(0x3E);//F891
            if (z)
                goto LabelF8B9;//F893
            cmp(a, 0x30);//F895
            if (z)
                goto LabelF8B1;//F897
            cmp(a, 0x20);//F899
            if (z)
                goto LabelF8A9;//F89B
            and(0x10);//F89D
            if (z)
                goto LabelF8B9;//F89F
            lda(0x1C);//F8A1
            ldx(0x03);//F8A3
            ldy(0x18);//F8A5
            if (!z)
                goto LabelF8BB;//F8A7
            LabelF8A9:
            lda(0x1C);//F8A9
            ldx(0x0C);//F8AB
            ldy(0x18);//F8AD
            if (!z)
                goto LabelF8BB;//F8AF
            LabelF8B1:
            lda(0x1C);//F8B1
            ldx(0x03);//F8B3
            ldy(0x58);//F8B5
            if (!z)
                goto LabelF8BB;//F8B7
            LabelF8B9:
            lda(0x10);//F8B9
            LabelF8BB:
            str(0x400C, a);//F8BB
            str(0x400E, x);//F8BE
            str(0x400F, y);//F8C1
        }

        void LabelF8C5()
        {
            tax();//F8C5
            ror();//F8C6
            txa();//F8C7
            rol();//F8C8
            rol();//F8C9
            rol();//F8CA
            LabelF8CB();
        }

        void LabelF8CB()
        {
            and(0x07);//F8CB
            c = false;//F8CD
            adc(ram[0xF0]);//F8CE
            adc(ram[0x07C4]);//F8D0
            tay();//F8D3
            ldam(0xFF66 + y);//F8D4
        }

        void LabelF8D8()
        {
            ldam(0x07B1);//F8D8
            and(0x08);//F8DB
            if (z)
                goto LabelF8E3;//F8DD
            lda(0x04);//F8DF
            if (!z)
                goto LabelF8EF;//F8E1
            LabelF8E3:
            ldam(0xF4);//F8E3
            and(0x7D);//F8E5
            if (z)
                goto LabelF8ED;//F8E7
            lda(0x08);//F8E9
            if (!z)
                goto LabelF8EF;//F8EB
            LabelF8ED:
            lda(0x28);//F8ED
            LabelF8EF:
            ldx(0x82);//F8EF
            ldy(0x7F);//F8F1
        }

        void LabelF8F4()
        {
            ldam(0x07B1);//F8F4
            and(0x08);//F8F7
            if (z)
                goto LabelF8FF;//F8F9
            ldam(0xFF96 + y);//F8FB
            return;
            LabelF8FF:
            ldam(0xF4);//F8FF
            and(0x7D);//F901
            if (z)
                goto LabelF909;//F903
            ldam(0xFF9A + y);//F905
            return;
            LabelF909:
            ldam(0xFFA2 + y);//F909
        }


    }
}
