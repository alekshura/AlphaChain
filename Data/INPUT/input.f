	program input
      implicit double precision (a-h,o-z)
	double precision M_th,M_exp,lgTa_th,lgTa_exp

      open(1,file="../input.dat")
      open(3,status="old",file="chain.dat")
	open(4,status="old",file="obr_4_10k.dat")
	open(5,file="MaxN.in")

    
      READ (1,*) MAX,IOPT,REPS,EOUT,IGO,H2,HH,NEPS,NSIG,iyuk
      READ (1,*) ILEVP,ILEVN,FACC,NMAXP,NMAXN,ILS,ICHOI
      READ (1,*) IVAREN
      READ (1,*) IDAN


	read(3,*) IZstart,INstart,IZend,INend

c
c	 read data from file


	IIZ = IZstart
	IIN = INstart

	do while(IIZ.ge.IZend)


	 do while (.NOT.EOF(4))

	  read(4,*) IZ,IN,bb2,bb3,bb4,bb5,bb6,bb7,bb8,
     *          Edef,Esh,M_th,M_exp,Qa_th,
     *          Qa_exp,Sn_th,Sn_exp,Sp_th,Sp_exp,Ta_th,Ta_exp,
	*          lgTa_th,lgTa_exp

	  if (IIZ.eq.IZ.and.IIN.eq.IN) then
	    beta2=bb2
	    beta3=bb3
	    beta4=bb4
	    beta5=bb5
	    beta6=bb6
	    beta7=bb7
	    beta8=bb8
	  endif
	
    	 enddo

     	 rewind(4)
	write(1,10) IIZ,IIN,beta2,beta3,beta4,beta5
     &	           ,beta6,beta7,beta8,0.d0,0.d0


	IIZ = IIZ - 2
	IIN = IIN - 2

	

	enddo

      
 
	close(4)
      
	write(1,'(4i2)') 0,0,0,0

 10   format(2i4,10f6.3)
      end