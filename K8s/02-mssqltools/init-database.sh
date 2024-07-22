#!/bin/bash
echo "Aguarde até a execução do DER"
echo "Aguardando serviço sqlserver ficar pronto."
sleep 5
echo "Executando der"

/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P SqlServer2019! -d master -i /tmp/tech-challenge-mer.sql
exit_code=$?

for (( i = 1; i <= 10; i++ ))
do 
    sleep 5
    echo "Executando der :: Nova tentativa :: " $i
    /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P SqlServer2019! -d master -i /tmp/tech-challenge-mer.sql
    exit_code=$?
    if [ $? -eq 0 ]; then
        break
    fi
done
echo "DER executado :: Successfully" 
