using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MarioBTXNA
{
    public partial class Game1 : Game
    {
        //Helper Functions
        void lda(byte value)
        {
            a = value;
            setZN(a);
        }

        void ldam(int addr)
        {
            a = ram[addr];
            setZN(a);

            if (addr == 0x2002)
                CheckVBlank();
            else if (addr == 0x2007)
                a = ReadPPUData();
            else if (addr == 0x4016 || addr == 0x4017)
                a = joyread(y);
        }

        void ldx(byte value)
        {
            x = value;
            setZN(x);
        }

        void ldxm(int addr)
        {
            x = ram[addr];
            setZN(x);
        }

        void ldy(byte value)
        {
            y = value;
            setZN(y);
        }

        void ldym(int addr)
        {
            y = ram[addr];
            setZN(y);
        }

        void str(int addr, int value)
        {
            if (addr == 0x2006)
                setPPUAddress(a);
            else if (addr == 0x2005)
                WritePPUScroll(a);
            else if (addr == 0x2007)
            {
                WritePPUData(a);
            }

            ram[addr] = (byte)value;
        }

        void inc(int addr)
        {
            int v = ram[addr];
            v++;
            ram[addr] = (byte)v;
            setZN(v);
        }

        void dec(int addr)
        {
            int v = ram[addr];
            v--;
            ram[addr] = (byte)v;
            setZN(v);
        }

        void adc(int b)
        {
            int temp = a + b + (c ? 1 : 0);
            setZN((byte)temp);
            c = temp > 255;
            a = (byte)temp;
        }

        void sbc(int b)
        {
            uint temp = (uint)(a - b - (c ? 0 : 1));
            setZN((byte)temp);
            c = temp < 256;
            a = (byte)temp;
        }

        void cmp(byte b, byte d)
        {
            byte res = (byte)(b - d);
            c = b >= d;
            setZN(res);
        }

        void dex()
        {
            sbyte res = (sbyte)(x - 1);
            x = (byte)res;
            setZN(res);
        }

        void dey()
        {
            sbyte res = (sbyte)(y - 1);
            y = (byte)res;
            setZN(res);
        }

        void inx()
        {
            sbyte res = (sbyte)(x + 1);
            x = (byte)res;
            setZN(res);
        }

        void iny()
        {
            sbyte res = (sbyte)(y + 1);
            y = (byte)res;
            setZN(res);
        }

        void or(byte value)
        {
            a |= value;
            setZN(a);
        }

        void orm(int addr)
        {
            byte value = ram[addr];
            a |= value;
            setZN(a);
            ram[addr] = value;
        }

        void and(byte value)
        {
            a &= value;
            setZN(a);
        }

        void txa()
        {
            a = x;
            setZN(a);
        }

        void tya()
        {
            a = y;
            setZN(a);
        }

        void tax()
        {
            x = a;
            setZN(x);
        }

        void tay()
        {
            y = a;
            setZN(y);
        }

        void txs()
        {
            s = x;
        }

        void tsx()
        {
            x = (byte)s;
        }

        void asl()
        {
            c = (a & (1 << 7)) > 0 ? true : false;
            a = (byte)((a << 1) & 0xfe);
            setZN(a);
        }

        void aslm(int addr)
        {
            byte v = ram[addr];
            c = (v & (1 << 7)) > 0 ? true : false;
            v = (byte)((v << 1) & 0xfe);
            setZN(v);
            ram[addr] = v;
        }

        void lsr()
        {
            c = (a & (1 << 0)) > 0 ? true : false;
            a = (byte)((a >> 1) & 0x7f);
            setZN(a);
        }

        void lsrm(int addr)
        {
            byte v = ram[addr];
            c = (v & (1 << 0)) > 0 ? true : false;
            v = (byte)((v >> 1) & 0x7f);
            setZN(v);
            ram[addr] = v;
        }

        void eor(byte c)
        {
            a ^= c;
            setZN(a);
        }

        void eorm(int addr)
        {
            byte c = ram[addr];
            a ^= c;
            setZN(a);
        }

        void ror()
        {
            bool bit0 = (a & (1 << 0)) > 0 ? true : false;
            a >>= 1;
            if (c)
                a |= (1 << 7);
            c = bit0;
            setZN(a);
        }

        void rorm(int addr)
        {
            int value = ram[addr];
            bool bit0 = (value & (1 << 0)) > 0 ? true : false;
            value >>= 1;
            if (c)
                value |= (1 << 7);
            c = bit0;
            setZN((byte)value);
            ram[addr] = (byte)value;
        }

        void rol()
        {
            bool bit7 = (a & (1 << 7)) > 0 ? true : false;
            a <<= 1;
            if (c)
                a |= (1 << 0);
            c = bit7;
            setZN(a);
        }

        void rolm(int addr)
        {
            int value = ram[addr];
            bool bit7 = (value & (1 << 7)) > 0 ? true : false;
            value <<= 1;
            if (c)
                value |= (1 << 0);
            c = bit7;
            setZN((byte)value);
            ram[addr] = (byte)value;
        }

        void bit(int value)
        {
            n = value < 0;
            z = (a & value) == 0;
        }

        void pha()
        {
            ram[0x100 | s] = a;
            s--;
        }

        void pla()
        {
            s++;
            a = ram[0x100 | s];
            setZN(a);
        }

        void setZN(int value)
        {
            z = value == 0;
            n = (value & 0x80) > 0;
        }

        int W(int addr)
        {
            return (ram[addr]) | ram[addr + 1] << 8;
        }

        void CheckVBlank()
        {
            cycles++;
            if (cycles % 2 == 0)
            {
                a = 0xc0;
                n = true;
                ram[0x2002] = a;
            }
            else
            {
                a = 0;
                ram[0x2002] = a;
                n = false;
            }
        }

        void setPPUAddress(byte value)
        {
            if (!writeToggle)
                ppuAddr = value << 8;
            else
                ppuAddr |= value;
            writeToggle = !writeToggle;
        }

        void WritePPUScroll(byte value)
        {
            if (!writeToggle)
                ppuScrollX = value;
            writeToggle = !writeToggle;
        }

        void WritePPUData(byte value)
        {
            vram[ppuAddr] = value;

            if (ppuAddr == 0x3f10 || ppuAddr == 0x3f14 || ppuAddr == 0x3f18 || ppuAddr == 0x3f1c)
                vram[ppuAddr - 0x10] = value;

            if ((ram[0x2000] & 4) > 0)
                ppuAddr += 32;
            else
                ppuAddr++;
        }

        byte ReadPPUData()
        {
            byte value = vram[ppuAddr];

            if ((ram[0x2000] & 4) > 0)
                ppuAddr += 32;
            else
                ppuAddr++;
            return value;
        }

        byte joyread(int count)
        {
            byte value = 0;
            if (Game1.GetJoyState()[count - 1] == true)
                value = 0x41;
            else
                value = 0x40;
            return value;
        }
    }
}
